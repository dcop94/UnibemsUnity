using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;

public class Ventil : MonoBehaviour
{
    // DataManger 객체 생성 : 유니티 기본 Class 상속받을 함수인 경우 new 키워드 X -> 게임 오브젝트에 컴포넌트로 추가하여 인스턴스를 생성 
    //public DataManager dataManager;

    // Sensor 스크립트를 참조하기 위한 변수 선언
    // public Sensor sensorScript;

    // 오브젝트마다 고유한 위치ID를 설정할 수 있도록 public 변수 추가
    public int ventilID;
    public int ventControlID;

    // data 담을 list 생성 
    private List<DataManager.VENTIL> ventilDataList;
    private List<DataManager.VentilControl> ventControlList;

    void Start()
    {
        // VentilControl 스크립트 참조
        // ventilControlScript = GetComponent<VentilControl>();

        InvokeRepeating("GenerateVentilData", 1.0f, 1.0f);
    }

    void GenerateVentilData()
    {
        // data 담을 list 초기화  
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

        // 수정 필요
        VentilControlData.m_vc_id = ventControlID;

        // 댐퍼 제어 값 (%)
        float controlInput = 50;
        float result = 0;

        GetDataInitalize(ref VentilControlData.m_dvalue, ref VentilData.m_wind, ref VentilData.m_power);
        GetControlDataBaseOnInput(ref VentilControlData.m_dvalue, ref VentilData.m_wind, ref VentilData.m_power, ref controlInput, ref result);

        // 리스트에 초기화 한 데이터값 추가
        ventilDataList.Add(VentilData);
        ventControlList.Add(VentilControlData);

        // 정상인 경우
        if (DataManager.Instance != null)
        {
            DataManager.Instance.VentilData(ventilDataList);
            DataManager.Instance.VentControlData(ventControlList);
        }

        // dataManager 변수 초기화 안된 경우
        else
        {
            Debug.LogError("dataManager 초기화 안됨!");
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
        // 상승정도
        result = controlInput - dvalue;

        dvalue += result;
        v_wind += Mathf.Round(result * 0.4f * 10.0f) / 10.0f;
        v_power += Mathf.Round(result * 0.02f * 10.0f) / 10.0f;
    }
}
