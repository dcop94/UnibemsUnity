using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES : MonoBehaviour
{
    // DataManger 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성 
    public DataManager dataManager;

    // EV 형 data 담을 list 생성 
    private List<DataManager.ES> esDataList;

    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int esID;

    void Start()
    {
        InvokeRepeating("GenerateESData", 1.0f, 1.0f);
    }

    void GenerateESData()
    {
        // EV형 data 담을 list 초기화  
        esDataList = new List<DataManager.ES>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        DataManager.ES ESData = new DataManager.ES();

        ESData.m_id = esID;

        ESData.m_power = GetDataBasedOnTime();

        esDataList.Add(ESData);

        // 정상인 경우
        if (dataManager != null)
        {
            dataManager.EsData(esDataList);
        }

        // dataManager 변수 초기화 안된 경우
        else
        {
            Debug.LogError("dataManager 초기화 안됨!");
        }

    }

    float GetDataBasedOnTime()
    {
        // 현재 시간을 기준으로 데이터를 생성
        float hour = System.DateTime.Now.Hour;

        if (hour >= 8 && hour < 11)
        {
            return Mathf.Round(Random.Range(1.0f, 1.5f) * 10.0f) / 10.0f;
        }

        else if (hour >= 10 && hour < 20)
        {
            return Mathf.Round(Random.Range(2.0f, 3.0f) * 10.0f) / 10.0f;
        }

        else if (hour >= 20 && hour < 23)
        {
            return Mathf.Round(Random.Range(1.5f, 2.0f) * 10.0f) / 10.0f;
        }

        else
        {
            return Mathf.Round(Random.Range(0.1f, 0.2f) * 10.0f) / 10.0f;
        }
    }
}
