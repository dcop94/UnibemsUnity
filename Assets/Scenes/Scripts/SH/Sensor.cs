using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    // DataManager ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ����
    //public DataManager dataManager;
    // SENSOR �� data ���� list ����
    private List<DataManager.SENSOR> sensorDataList;
    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int sensorID;


    void Start()
    {
        Debug.Log("Sensor InvokeRepeat");
        // 1���Ŀ� ù ȣ��, 1�ʸ��� GenerateSensorData �޼��带 ȣ���Ͽ� ������ ����
        InvokeRepeating("GenerateSensorData", 1.0f, 1.0f);
    }
    void GenerateSensorData()
    {
        // SENSOR �� data ���� list �ʱ�ȭ
        sensorDataList = new List<DataManager.SENSOR>();

        // ���� �µ� ���� �ʱ�ȭ
        float currentTemperature = 22.0f;

        // ���� �ð��� ���� ���� �µ� ������ �ʱ�ȭ
        currentTemperature = currentTemperature + GetTempRandomVariationBasedOnTime();

        DataManager.SENSOR sensorData = new DataManager.SENSOR
        {
            m_id = sensorID // �� ������Ʈ�� ���� ID ����
        };

        // ���� �ð��� ���� ���� ����, CO2 �� �ʱ�ȭ
        sensorData.m_temp = currentTemperature;
        sensorData.m_co2 = 700 + Mathf.Round(Mathf.Round(GetCO2BasedOnTime()));
        sensorData.m_humidity = 45 + Mathf.Round(Mathf.Round(GetHumidityBasedOnTime()));

        // ����Ʈ�� �ʱ�ȭ �� �����Ͱ� �߰�
        sensorDataList.Add(sensorData);

        Debug.Log("SENSOR");

        // ������ ���
        //if (DataManager.Instance != null)
        //{
            DataManager.Instance.Sensor1Data(sensorDataList);
        //}
        // dataManager ���� �ʱ�ȭ �ȵ� ���
        //else
        //{
          //  Debug.LogError("dataManager �ʱ�ȭ �ȵ�!");
        //}

        if (!FlagSet.sensor_on)
        {
            FlagSet.sensor_on = true;
        }
    }

    float GetTempRandomVariationBasedOnTime()
    {
        // ���� �ð��� �������� �µ��� ����
        float hour = System.DateTime.Now.Hour;

        // ����
        if (hour >= 9 && hour < 13)
        {
            return Mathf.Round(Random.Range(3.0f, 4.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 13 && hour < 18)
        // ����
        {
            return Mathf.Round(Random.Range(4.0f, 6.0f) * 10.0f) / 10.0f;
        }
        // ����
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(3.0f, 4.0f) * 10.0f) / 10.0f;
        }
        // ����
        else
        {
            return Mathf.Round(Random.Range(-1.0f, 1.0f) * 10.0f) / 10.0f;
        }
    }

    float GetCO2BasedOnTime()
    {
        float hour = System.DateTime.Now.Hour;

        // ����
        if (hour >= 9 && hour < 13)
        {
            return Mathf.Round(Random.Range(100.0f, 300.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 13 && hour < 18)
        // ����
        {
            return Mathf.Round(Random.Range(300.0f, 500.0f) * 10.0f) / 10.0f;
        }
        // ����
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(100.0f, 300.0f) * 10.0f) / 10.0f;
        }
        // ����
        else
        {
            return Mathf.Round(Random.Range(-100.0f, 100.0f) * 10.0f) / 10.0f;
        }

    }

    float GetHumidityBasedOnTime()
    {
        float hour = System.DateTime.Now.Hour;

        // ����
        if (hour >= 9 && hour < 12)
        {
            return Mathf.Round(Random.Range(5.0f, 15.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 12 && hour < 18)
        // ����
        {
            return Mathf.Round(Random.Range(15.0f, 25.0f) * 10.0f) / 10.0f;
        }
        // ����
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(5.0f, 15.0f) * 10.0f) / 10.0f;
        }
        // ����
        else
        {
            return Mathf.Round(Random.Range(-5.0f, 5.0f) * 10.0f) / 10.0f;
        }

    }

    public List<DataManager.SENSOR> GetSensorDataList()
    {
        return sensorDataList;
    }
}
