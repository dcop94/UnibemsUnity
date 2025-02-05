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

        // 실험 센서 온도값 불러오기
        float sensorTemp = dataManager.sensor.m_temp; // 수정된 부분

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
            Debug.LogError("dataManager 초기화 안됨!");
        }

        // HAVC 데이터 업데이트 로그 출력
        //Debug.Log($"HAVC Generated: Temp={HAVCData.m_temp}, Power={HAVCData.m_power} based on Sensor Temp={sensorTemp}");
    }
}





















// 여기









/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;
using static Sensor;


public class HAVC : MonoBehaviour
{
    // DataManger 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성 
    //public DataManager dataManager;

    // Sensor 스크립트를 참조하기 위한 변수 선언
    public Sensor sensorScript;

    // HAVC 형 data 담을 list 생성 
    private List<DataManager.HAVC> havcDataList;

    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int havcID;

    void Start()
    {
        InvokeRepeating("GenerateHAVCData", 1.0f, 1.0f);
    }

    void GenerateHAVCData()
    {
        // HAVC형 data 담을 list 초기화  
        havcDataList = new List<DataManager.HAVC>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        // 에이컨 설정 온도 , 상태 초기화
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
        // 운행 O
        if (status == 1)
        {
            // Sensor 스크립트의 sensorDataList를 불러오기
            List<DataManager.SENSOR> sensorDataList = sensorScript.GetSensorDataList();

            if (sensorDataList != null && sensorDataList.Count > 0)
            {
                // 마지막 데이터를 사용
                float sensorTemp = sensorDataList[sensorDataList.Count - 1].m_temp;

                // 설정온도가 낮출 경우
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

        // 운행 X
        if (status == 0)
        {
            return 0.0f;
        }

        // 단순 오류 표시(status 1, 0 아닌 경우)
        return 20000000.0f;
    }
}
 
*/

/*
 // DataManger 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성 
    //public DataManager dataManager;

    // Sensor 스크립트를 참조하기 위한 변수 선언
    public Sensor sensorScript;

    // HAVC 형 data 담을 list 생성 
    private List<DataManager.HAVC> havcDataList;

    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int havcID;

    void Start()
    {
        InvokeRepeating("GenerateHAVCData", 1.0f, 1.0f);
    }

    void GenerateHAVCData()
    {
        // HAVC형 data 담을 list 초기화  
        havcDataList = new List<DataManager.HAVC>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        // 에이컨 설정 온도 , 상태 초기화
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
        // 운행 O
        if (status == 1)
        {
            // Sensor 스크립트의 sensorDataList를 불러오기
            List<DataManager.SENSOR> sensorDataList = sensorScript.GetSensorDataList();

            if (sensorDataList != null && sensorDataList.Count > 0)
            {
                // 마지막 데이터를 사용
                float sensorTemp = sensorDataList[sensorDataList.Count - 1].m_temp;

                // 설정온도가 낮출 경우
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

        // 운행 X
        if (status == 0)
        {
            return 0.0f;
        }

        // 단순 오류 표시(status 1, 0 아닌 경우)
        return 20000000.0f;
    } 
 
 */

