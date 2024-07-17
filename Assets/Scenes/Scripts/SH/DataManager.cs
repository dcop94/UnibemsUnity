using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEditor.VersionControl;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    //�����͸Ŵ����� �ν��Ͻ��� ��� ��������(static ���������� �����ϱ� ���� ����������� �ϰڴ�).
    //�� ���� ������ ���ӸŴ��� �ν��Ͻ��� �� instance�� ��� �༮�� �����ϰ� �� ���̴�.
    //������ ���� private����.
    private static DataManager instance = null;

    private TcpClient socketConnection; // �������� ������ �����ϴ� ����
    private NetworkStream networkStream; // �����͸� �ۼ����ϴ� string ���� : ���� ����
    private const string serverIp = "127.0.0.1"; // ���� IP �ּ�
    private const string serverIp2 = "192.168.1.235"; // ��ȯ�� IP �ּ�
    private const int serverPort = 7777; // ���� ��Ʈ ��ȣ

    // ����������
    public struct EV
    {
        public int m_id;
        public float m_power;
    }

    // �����÷�����
    public struct ES
    {
        public int m_id;
        public float m_power;
    }

    // �ó����
    public struct HAVC
    {
        public int m_id;
        public float m_power;
        public float m_set_temp;
        public int m_status;
    }

    // ��������
    public struct SENSOR
    {
        public int m_id;
        public float m_temp;
        public float m_humidity;
        public float m_co2;
    }

    // ȯ�⼳��
    public struct VENTIL
    {
        public int m_id;
        public float m_wind;
        public float m_power;
    }

    public struct VentilControl
    {
        public int m_vc_id;
        public int m_d_id;
        public float m_dvalue;
    }


    // �ν��Ͻ� ����
    public HAVC havc;
    public EV ev;
    public ES es;
    public VENTIL ventil;
    public SENSOR sensor;


    // snsorDataList�� ��� ����Ʈ ���� - ����� ���� List
    private List<List<DataManager.SENSOR>> forSocketSensorDataList;
    //private List<List<SENSOR>> forSocketSensorDataList;
    private List<List<DataManager.HAVC>> forSocketHAVCDataList;
    private List<List<DataManager.VENTIL>> forSocketVentilDataList;
    private List<List<DataManager.EV>> forSocketEVDataList;
    private List<List<DataManager.ES>> forSocketESDataList;
    private List<List<DataManager.VentilControl>> forSocketVentControlDataList;

    private void Awake()
    {
        Debug.Log("DataManager Created");
        if(instance == null)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }
    }

    
    public static DataManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }



    void Start()
    {
        // �ν��Ͻ� �ʱ�ȭ
        sensor = new SENSOR();
        havc = new HAVC();

        forSocketSensorDataList = new List<List<DataManager.SENSOR>>();
        //forSocketSensorDataList = new List<List<SENSOR>>();
        forSocketHAVCDataList = new List<List<DataManager.HAVC>>();
        forSocketVentilDataList = new List<List<DataManager.VENTIL>>();
        forSocketEVDataList = new List<List<DataManager.EV>>();
        forSocketESDataList = new List<List<DataManager.ES>>();
        forSocketVentControlDataList = new List<List<VentilControl>>();

        // ���� ���� + ������ �����û
        ConnectToServer();


    }

    private void ConnectToServer()
    {
        // ���� �ν��Ͻ� ����
        socketConnection = new TcpClient();
        try
        {
            // ������ ���� ��û
            //socketConnection.Connect(serverIp, serverPort);
            socketConnection.Connect(serverIp2, serverPort);

            if (socketConnection.Connected)
            {
                // TCP ������ ���� NetworkStream ���
                networkStream = socketConnection.GetStream();
                Debug.Log("������ ���� ����!");
            }
            else
            {
                Debug.LogError("������ ���� ����");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("������ ���� �� ���� �߻�: " + e.Message);
        }
    }

    void SendDataToServer(string message)
    {
        // �޽��� ������ ���� ���� Ȯ�ο� 
        if (socketConnection == null || socketConnection.Connected == false)
        {
            Debug.LogError("������ ������� �ʾҽ��ϴ�.");
        }

        // C#������ String Ÿ������ �ؽ�Ʈ ������ ǥ�� 
        // message ���ڿ��� ����Ʈ �迭�� ��ȯ - C++ ������ ��Ŷ����� ���
        byte[] messageBytes = Encoding.ASCII.GetBytes(message);

        // Write() - �޽��� ����
        networkStream.Write(messageBytes, 0, messageBytes.Length);
    }

    // ���ø����̼� ���� �� ���� �� ��Ʈ�� (�⺻ �Լ�)
    void OnApplicationQuit()
    {
        if (networkStream != null)
        {
            networkStream.Close();
        }

        if (socketConnection != null)
        {
            socketConnection.Close();
        }
    }

    // DataMagerRead �����
    public NetworkStream GetNetworkStream()
    {
        return networkStream;
    }

    public void Sensor1Data(List<DataManager.SENSOR> dataList)
    {
        forSocketSensorDataList.Add(dataList);

        foreach (DataManager.SENSOR sensorData in dataList)
        {
            // ���� �ܼ� Ȯ�ο� (��ġ, �µ� ����, �̻�ȭź�� �� ����)
            string sensorDataString = $"{sensorData.m_id},{sensorData.m_temp},{sensorData.m_humidity},{sensorData.m_co2}Sensor /";
            SendDataToServer(sensorDataString);
        }
    }

    public void HAVCData(List<DataManager.HAVC> dataList)
    {
        forSocketHAVCDataList.Add(dataList);

        foreach (DataManager.HAVC havcData in dataList)
        {
            // ��ġ, �����µ�, ���� ����
            string havcDataString = $"{havcData.m_id},{havcData.m_power},{havcData.m_set_temp},{havcData.m_status}HAVC /";
            SendDataToServer(havcDataString);
        }
    }

    public void VentilData(List<DataManager.VENTIL> dataList)
    {
        forSocketVentilDataList.Add(dataList);

        foreach (DataManager.VENTIL ventilData in dataList)
        {
            // ��ġ, ǳ�� , ���·� ����
            string ventilDataString = $"{ventilData.m_id},{ventilData.m_wind},{ventilData.m_power}VENTIL /";
            SendDataToServer(ventilDataString);
        }
    }
    public void EvData(List<DataManager.EV> dataList)
    {
        forSocketEVDataList.Add(dataList);

        foreach (DataManager.EV evData in dataList)
        {
            // ��ġ, ���·� ����
            string evlDataString = $"{evData.m_id},{evData.m_power}EV /";
            SendDataToServer(evlDataString);
        }
    }

    public void EsData(List<DataManager.ES> dataList)
    {
        forSocketESDataList.Add(dataList);

        foreach (DataManager.ES esData in dataList)
        {
            // �ٶ�, ���·� ����
            string eslDataString = $"{esData.m_id},{esData.m_power}ES /";
            SendDataToServer(eslDataString);
        }
    }

    public void VentControlData(List<DataManager.VentilControl> dataList)
    {
        forSocketVentControlDataList.Add(dataList);

        foreach (DataManager.VentilControl ventilControlData in dataList)
        {
            // ��ġ, ǳ�� , ���·� ����
            string ventilDataControlString = $"{ventilControlData.m_vc_id},{ventilControlData.m_d_id},{ventilControlData.m_dvalue}VC /";
            SendDataToServer(ventilDataControlString);
        }
    }

}
