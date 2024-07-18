using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAVC : MonoBehaviour
{
    public DataManager dataManager;
    private List<DataManager.HAVC> havcDataList;
    public int havcID;

    void Start()
    {
        InvokeRepeating("GenerateHAVCData", 1.0f, 1.0f);
    }

    void GenerateHAVCData()
    {
        havcDataList = new List<DataManager.HAVC>();
        

        if (!FlagSet.sensor_on)
        {
            return;
        }

        // ���� ���� �µ��� �ҷ�����
        float sensorTemp = dataManager.sensor.m_temp; // ������ �κ�

        DataManager.HAVC HAVCData = new DataManager.HAVC();

        HAVCData.m_status = 1;
        HAVCData.m_set_temp = 24.0f;
        HAVCData.m_id = havcID;

        if (sensorTemp < 26.0f)
        {
           
            float havcPower = Mathf.Round(Random.Range(0.2f, 0.3f) * 10.0f) / 10.0f;
            HAVCData.m_power = havcPower;
        }
        else if (26.0f < sensorTemp && sensorTemp < 28.0f)
        {
            
            float havcPower = Mathf.Round(Random.Range(0.3f, 0.4f) * 10.0f) / 10.0f;
            HAVCData.m_power = havcPower;
        }
        else if (sensorTemp >= 28.0f)
        {
            
            float havcPower = Mathf.Round(Random.Range(0.4f, 0.5f) * 10.0f) / 10.0f;
            HAVCData.m_power = havcPower;
        }

        havcDataList.Add(HAVCData);

        if (dataManager != null)
        {
            dataManager.HAVCData(havcDataList);
        }
        else
        {
            Debug.LogError("dataManager �ʱ�ȭ �ȵ�!");
        }

        // HAVC ������ ������Ʈ �α� ���
        //Debug.Log($"HAVC Generated: Temp={HAVCData.m_temp}, Power={HAVCData.m_power} based on Sensor Temp={sensorTemp}");
    }
}





















// ����









/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;
using static Sensor;


public class HAVC : MonoBehaviour
{
    // DataManger ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ���� 
    //public DataManager dataManager;

    // Sensor ��ũ��Ʈ�� �����ϱ� ���� ���� ����
    public Sensor sensorScript;

    // HAVC �� data ���� list ���� 
    private List<DataManager.HAVC> havcDataList;

    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int havcID;

    void Start()
    {
        InvokeRepeating("GenerateHAVCData", 1.0f, 1.0f);
    }

    void GenerateHAVCData()
    {
        // HAVC�� data ���� list �ʱ�ȭ  
        havcDataList = new List<DataManager.HAVC>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        // ������ ���� �µ� , ���� �ʱ�ȭ
        float h_set_temp = 20;
        int h_status = 1;

        DataManager.HAVC HAVCData = new DataManager.HAVC();

        HAVCData.m_id = havcID;
        HAVCData.m_set_temp = h_set_temp;
        HAVCData.m_status = h_status;
        HAVCData.m_power = GetSetPower(h_status, h_set_temp);

    }

    float GetSetPower(int status, float setTemp)
    {
        // ���� O
        if (status == 1)
        {
            // Sensor ��ũ��Ʈ�� sensorDataList�� �ҷ�����
            List<DataManager.SENSOR> sensorDataList = sensorScript.GetSensorDataList();

            if (sensorDataList != null && sensorDataList.Count > 0)
            {
                // ������ �����͸� ���
                float sensorTemp = sensorDataList[sensorDataList.Count - 1].m_temp;

                // �����µ��� ���� ���
                if (setTemp < sensorTemp)
                {
                    return Mathf.Round(Random.Range(0.06f, 0.08f) * 100.0f) / 100.0f;
                }

                else
                {
                    return Mathf.Round(Random.Range(0.02f, 0.04f) * 100.0f) / 100.0f;
                }
            }
        }

        // ���� X
        if (status == 0)
        {
            return 0.0f;
        }

        // �ܼ� ���� ǥ��(status 1, 0 �ƴ� ���)
        return 20000000.0f;
    }
}
 
*/

/*
 // DataManger ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ���� 
    //public DataManager dataManager;

    // Sensor ��ũ��Ʈ�� �����ϱ� ���� ���� ����
    public Sensor sensorScript;

    // HAVC �� data ���� list ���� 
    private List<DataManager.HAVC> havcDataList;

    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int havcID;

    void Start()
    {
        InvokeRepeating("GenerateHAVCData", 1.0f, 1.0f);
    }

    void GenerateHAVCData()
    {
        // HAVC�� data ���� list �ʱ�ȭ  
        havcDataList = new List<DataManager.HAVC>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        // ������ ���� �µ� , ���� �ʱ�ȭ
        float h_set_temp = 20;
        int h_status = 1;

        DataManager.HAVC HAVCData = new DataManager.HAVC();

        HAVCData.m_id = havcID;
        HAVCData.m_set_temp = h_set_temp;
        HAVCData.m_status = h_status;
        HAVCData.m_power = GetSetPower(h_status, h_set_temp);

    }

    float GetSetPower(int status, float setTemp)
    {
        // ���� O
        if (status == 1)
        {
            // Sensor ��ũ��Ʈ�� sensorDataList�� �ҷ�����
            List<DataManager.SENSOR> sensorDataList = sensorScript.GetSensorDataList();

            if (sensorDataList != null && sensorDataList.Count > 0)
            {
                // ������ �����͸� ���
                float sensorTemp = sensorDataList[sensorDataList.Count - 1].m_temp;

                // �����µ��� ���� ���
                if (setTemp < sensorTemp)
                {
                    return Mathf.Round(Random.Range(0.06f, 0.08f) * 100.0f) / 100.0f;
                }

                else
                {
                    return Mathf.Round(Random.Range(0.02f, 0.04f) * 100.0f) / 100.0f;
                }
            }
        }

        // ���� X
        if (status == 0)
        {
            return 0.0f;
        }

        // �ܼ� ���� ǥ��(status 1, 0 �ƴ� ���)
        return 20000000.0f;
    } 
 
 */

