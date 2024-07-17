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
    //데이터매니저의 인스턴스를 담는 전역변수(static 변수이지만 이해하기 쉽게 전역변수라고 하겠다).
    //이 게임 내에서 게임매니저 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것이다.
    //보안을 위해 private으로.
    private static DataManager instance = null;

    private TcpClient socketConnection; // 서버와의 연결을 관리하는 변수
    private NetworkStream networkStream; // 데이터를 송수신하는 string 변수 : 버퍼 역할
    private const string serverIp = "127.0.0.1"; // 나의 IP 주소
    private const string serverIp2 = "192.168.1.235"; // 동환님 IP 주소
    private const int serverPort = 7777; // 서버 포트 번호

    // 엘리베이터
    public struct EV
    {
        public int m_id;
        public float m_power;
    }

    // 에스컬레이터
    public struct ES
    {
        public int m_id;
        public float m_power;
    }

    // 냉난방기
    public struct HAVC
    {
        public int m_id;
        public float m_power;
        public float m_set_temp;
        public int m_status;
    }

    // 계측센서
    public struct SENSOR
    {
        public int m_id;
        public float m_temp;
        public float m_humidity;
        public float m_co2;
    }

    // 환기설비
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


    // 인스턴스 선언
    public HAVC havc;
    public EV ev;
    public ES es;
    public VENTIL ventil;
    public SENSOR sensor;


    // snsorDataList를 담는 리스트 생성 - 통신을 위한 List
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
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
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
        // 인스턴스 초기화
        sensor = new SENSOR();
        havc = new HAVC();

        forSocketSensorDataList = new List<List<DataManager.SENSOR>>();
        //forSocketSensorDataList = new List<List<SENSOR>>();
        forSocketHAVCDataList = new List<List<DataManager.HAVC>>();
        forSocketVentilDataList = new List<List<DataManager.VENTIL>>();
        forSocketEVDataList = new List<List<DataManager.EV>>();
        forSocketESDataList = new List<List<DataManager.ES>>();
        forSocketVentControlDataList = new List<List<VentilControl>>();

        // 소켓 생성 + 서버에 연결요청
        ConnectToServer();


    }

    private void ConnectToServer()
    {
        // 소켓 인스턴스 생성
        socketConnection = new TcpClient();
        try
        {
            // 서버에 연결 요청
            //socketConnection.Connect(serverIp, serverPort);
            socketConnection.Connect(serverIp2, serverPort);

            if (socketConnection.Connected)
            {
                // TCP 연결을 통해 NetworkStream 얻기
                networkStream = socketConnection.GetStream();
                Debug.Log("서버와 연결 성공!");
            }
            else
            {
                Debug.LogError("서버와 연결 실패");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("서버와 연결 중 예외 발생: " + e.Message);
        }
    }

    void SendDataToServer(string message)
    {
        // 메시지 전송전 서버 연결 확인용 
        if (socketConnection == null || socketConnection.Connected == false)
        {
            Debug.LogError("서버에 연결되지 않았습니다.");
        }

        // C#에서는 String 타입으로 텍스트 데이터 표현 
        // message 문자열을 바이트 배열로 변환 - C++ 서버와 통신때문에 사용
        byte[] messageBytes = Encoding.ASCII.GetBytes(message);

        // Write() - 메시지 전송
        networkStream.Write(messageBytes, 0, messageBytes.Length);
    }

    // 어플리케이션 종료 전 소켓 및 스트림 (기본 함수)
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

    // DataMagerRead 연결용
    public NetworkStream GetNetworkStream()
    {
        return networkStream;
    }

    public void Sensor1Data(List<DataManager.SENSOR> dataList)
    {
        forSocketSensorDataList.Add(dataList);

        foreach (DataManager.SENSOR sensorData in dataList)
        {
            // 서버 콘솔 확인용 (위치, 온도 습도, 이산화탄소 농도 순서)
            string sensorDataString = $"{sensorData.m_id},{sensorData.m_temp},{sensorData.m_humidity},{sensorData.m_co2}Sensor /";
            SendDataToServer(sensorDataString);
        }
    }

    public void HAVCData(List<DataManager.HAVC> dataList)
    {
        forSocketHAVCDataList.Add(dataList);

        foreach (DataManager.HAVC havcData in dataList)
        {
            // 위치, 설정온도, 전력 순서
            string havcDataString = $"{havcData.m_id},{havcData.m_power},{havcData.m_set_temp},{havcData.m_status}HAVC /";
            SendDataToServer(havcDataString);
        }
    }

    public void VentilData(List<DataManager.VENTIL> dataList)
    {
        forSocketVentilDataList.Add(dataList);

        foreach (DataManager.VENTIL ventilData in dataList)
        {
            // 위치, 풍량 , 전력량 순서
            string ventilDataString = $"{ventilData.m_id},{ventilData.m_wind},{ventilData.m_power}VENTIL /";
            SendDataToServer(ventilDataString);
        }
    }
    public void EvData(List<DataManager.EV> dataList)
    {
        forSocketEVDataList.Add(dataList);

        foreach (DataManager.EV evData in dataList)
        {
            // 위치, 전력량 순서
            string evlDataString = $"{evData.m_id},{evData.m_power}EV /";
            SendDataToServer(evlDataString);
        }
    }

    public void EsData(List<DataManager.ES> dataList)
    {
        forSocketESDataList.Add(dataList);

        foreach (DataManager.ES esData in dataList)
        {
            // 바람, 전력량 순서
            string eslDataString = $"{esData.m_id},{esData.m_power}ES /";
            SendDataToServer(eslDataString);
        }
    }

    public void VentControlData(List<DataManager.VentilControl> dataList)
    {
        forSocketVentControlDataList.Add(dataList);

        foreach (DataManager.VentilControl ventilControlData in dataList)
        {
            // 위치, 풍량 , 전력량 순서
            string ventilDataControlString = $"{ventilControlData.m_vc_id},{ventilControlData.m_d_id},{ventilControlData.m_dvalue}VC /";
            SendDataToServer(ventilDataControlString);
        }
    }

}
