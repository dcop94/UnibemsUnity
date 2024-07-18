using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    // DataManager 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성
    //public DataManager dataManager;
    // SENSOR 형 data 담을 list 생성
    private List<DataManager.SENSOR> sensorDataList;
    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int sensorID;


    void Start()
    {
        Debug.Log("Sensor InvokeRepeat");
        // 1초후에 첫 호출, 1초마다 GenerateSensorData 메서드를 호출하여 데이터 생성
        InvokeRepeating("GenerateSensorData", 1.0f, 1.0f);
    }
    void GenerateSensorData()
    {
        // SENSOR 형 data 담을 list 초기화
        sensorDataList = new List<DataManager.SENSOR>();

        // 현재 온도 변수 초기화
        float currentTemperature = 22.0f;

        // 현재 시간에 따른 센서 온도 데이터 초기화
        currentTemperature = currentTemperature + GetTempRandomVariationBasedOnTime();

        DataManager.SENSOR sensorData = new DataManager.SENSOR
        {
            m_id = sensorID // 각 오브젝트의 고유 ID 설정
        };

        // 현재 시간에 따른 센서 습도, CO2 농도 초기화
        sensorData.m_temp = currentTemperature;
        sensorData.m_co2 = 700 + Mathf.Round(Mathf.Round(GetCO2BasedOnTime()));
        sensorData.m_humidity = 45 + Mathf.Round(Mathf.Round(GetHumidityBasedOnTime()));

        // 리스트에 초기화 한 데이터값 추가
        sensorDataList.Add(sensorData);

        Debug.Log("SENSOR");

        // 정상인 경우
        //if (DataManager.Instance != null)
        //{
            DataManager.Instance.Sensor1Data(sensorDataList);
        //}
        // dataManager 변수 초기화 안된 경우
        //else
        //{
          //  Debug.LogError("dataManager 초기화 안됨!");
        //}

        if (!FlagSet.sensor_on)
        {
            FlagSet.sensor_on = true;
        }
    }

    float GetTempRandomVariationBasedOnTime()
    {
        // 현재 시간을 기준으로 온도를 생성
        float hour = System.DateTime.Now.Hour;

        // 오전
        if (hour >= 9 && hour < 13)
        {
            return Mathf.Round(Random.Range(3.0f, 4.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 13 && hour < 18)
        // 오후
        {
            return Mathf.Round(Random.Range(4.0f, 6.0f) * 10.0f) / 10.0f;
        }
        // 저녁
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(3.0f, 4.0f) * 10.0f) / 10.0f;
        }
        // 새벽
        else
        {
            return Mathf.Round(Random.Range(-1.0f, 1.0f) * 10.0f) / 10.0f;
        }
    }

    float GetCO2BasedOnTime()
    {
        float hour = System.DateTime.Now.Hour;

        // 오전
        if (hour >= 9 && hour < 13)
        {
            return Mathf.Round(Random.Range(100.0f, 300.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 13 && hour < 18)
        // 오후
        {
            return Mathf.Round(Random.Range(300.0f, 500.0f) * 10.0f) / 10.0f;
        }
        // 저녁
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(100.0f, 300.0f) * 10.0f) / 10.0f;
        }
        // 새벽
        else
        {
            return Mathf.Round(Random.Range(-100.0f, 100.0f) * 10.0f) / 10.0f;
        }

    }

    float GetHumidityBasedOnTime()
    {
        float hour = System.DateTime.Now.Hour;

        // 오전
        if (hour >= 9 && hour < 12)
        {
            return Mathf.Round(Random.Range(5.0f, 15.0f) * 10.0f) / 10.0f;
        }
        else if (hour >= 12 && hour < 18)
        // 오후
        {
            return Mathf.Round(Random.Range(15.0f, 25.0f) * 10.0f) / 10.0f;
        }
        // 저녁
        else if (hour >= 18 && hour < 24)

        {
            return Mathf.Round(Random.Range(5.0f, 15.0f) * 10.0f) / 10.0f;
        }
        // 새벽
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
