using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EV : MonoBehaviour
{
    // DataManger 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성 
    //public DataManager dataManager;

    // EV 형 data 담을 list 생성 
    private List<DataManager.EV> evDataList;

    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int evID;
    void Start()
    {
        InvokeRepeating("GenerateEVData", 1.0f, 1.0f);
    }

    void GenerateEVData()
    {
        // EV형 data 담을 list 초기화  
        evDataList = new List<DataManager.EV>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        DataManager.EV EVData = new DataManager.EV();

        EVData.m_id = evID;

        EVData.m_power = GetDataBasedOnTime();

        evDataList.Add(EVData);

        // 정상인 경우
        if (DataManager.Instance != null)
        {
            DataManager.Instance.EvData(evDataList);
        }

        // dataManager 변수 초기화 안된 경우
        else
        {
            Debug.LogError("dataManager 초기화 안됨!");
        }

    }

    float GetDataBasedOnTime()
    {
        // 현재 시간을 기준으로 데이터 생성
        float hour = System.DateTime.Now.Hour;

        if (hour >= 8 && hour < 11)
        {
            return Mathf.Round(Random.Range(2.0f, 2.5f) * 10.0f) / 10.0f;
        }

        else if (hour >= 10 && hour < 20)
        {
            return Mathf.Round(Random.Range(3.0f, 5.0f) * 10.0f) / 10.0f;
        }

        else if (hour >= 20 && hour < 23)
        {
            return Mathf.Round(Random.Range(2.0f, 3.0f) * 10.0f) / 10.0f;
        }

        else
        {
            return Mathf.Round(Random.Range(0.1f, 0.2f) * 10.0f) / 10.0f;
        }
    }
}
