using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;

public class Ventil : MonoBehaviour
{
    // DataManger ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ���� 
    //public DataManager dataManager;

    // Sensor ��ũ��Ʈ�� �����ϱ� ���� ���� ����
    // public Sensor sensorScript;

    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int ventilID;
    public int ventControlID;

    // data ���� list ���� 
    private List<DataManager.VENTIL> ventilDataList;
    private List<DataManager.VentilControl> ventControlList;

    void Start()
    {
        // VentilControl ��ũ��Ʈ ����
        // ventilControlScript = GetComponent<VentilControl>();

        InvokeRepeating("GenerateVentilData", 1.0f, 1.0f);
    }

    void GenerateVentilData()
    {
        // data ���� list �ʱ�ȭ  
        ventilDataList = new List<DataManager.VENTIL>();
        ventControlList = new List<DataManager.VentilControl>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        DataManager.VENTIL VentilData = new DataManager.VENTIL
        {
            m_id = ventilID
        };

        DataManager.VentilControl VentilControlData = new DataManager.VentilControl();

        VentilControlData.m_d_id = VentilData.m_id;

        // ���� �ʿ�
        VentilControlData.m_vc_id = ventControlID;

        // ���� ���� �� (%)
        float controlInput = 50;
        float result = 0;

        GetDataInitalize(ref VentilControlData.m_dvalue, ref VentilData.m_wind, ref VentilData.m_power);
        GetControlDataBaseOnInput(ref VentilControlData.m_dvalue, ref VentilData.m_wind, ref VentilData.m_power, ref controlInput, ref result);

        // ����Ʈ�� �ʱ�ȭ �� �����Ͱ� �߰�
        ventilDataList.Add(VentilData);
        ventControlList.Add(VentilControlData);

        // ������ ���
        if (DataManager.Instance != null)
        {
            DataManager.Instance.VentilData(ventilDataList);
            DataManager.Instance.VentControlData(ventControlList);
        }

        // dataManager ���� �ʱ�ȭ �ȵ� ���
        else
        {
            Debug.LogError("dataManager �ʱ�ȭ �ȵ�!");
        }

    }

    void GetDataInitalize(ref float dvalue, ref float v_wind, ref float v_power)
    {
        dvalue = 50;
        v_wind = Mathf.Round(Random.Range(13.0f, 17.0f) * 10.0f) / 10.0f;
        v_power = Mathf.Round(Random.Range(0.7f, 0.9f) * 10.0f) / 10.0f;

    }

    void GetControlDataBaseOnInput(ref float dvalue, ref float v_wind, ref float v_power, ref float controlInput, ref float result)
    {
        // �������
        result = controlInput - dvalue;

        dvalue += result;
        v_wind += Mathf.Round(result * 0.4f * 10.0f) / 10.0f;
        v_power += Mathf.Round(result * 0.02f * 10.0f) / 10.0f;
    }
}
