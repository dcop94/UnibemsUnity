using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    public Text currentTemperature; // 현재온도
    public Text temperatureText; // 설정온도
    public Text airConditionerStatusText; // 에어컨 상태 텍스트
    public Button UpButton;
    public Button DownButton;
    public Toggle airConditionerToggle; // 에어컨 On/Off 토글

    public string objectID; // 오브젝트 고유 ID

    private int currentTemp;
    private int temp;
    private int minTemperature = 18; // 최소 온도
    private int maxTemperature = 30; // 최대 온도

    private bool isAirConditionerOn; // 에어컨 상태

    void Start()
    {
        // 텍스트의 값을 int으로 변경
        currentTemp = int.Parse(currentTemperature.text);

        // PlayerPrefs에서 설정 온도를 불러오기
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        if (PlayerPrefs.HasKey(savedTemperatureKey))
        {
            temp = PlayerPrefs.GetInt(savedTemperatureKey);
        }
        else
        {
            // 설정 온도가 저장되어 있지 않으면 현재 온도로 초기화
            temp = currentTemp;
        }

        // PlayerPrefs에서 에어컨 상태를 불러오기
        string savedAirConditionerKey = "SavedAirConditioner_" + objectID;
        if (PlayerPrefs.HasKey(savedAirConditionerKey))
        {
            isAirConditionerOn = PlayerPrefs.GetInt(savedAirConditionerKey) == 1;
        }
        else
        {
            isAirConditionerOn = false;
        }

        // 버튼 이벤트 등록
        UpButton.onClick.AddListener(UPTemperature);
        DownButton.onClick.AddListener(DownTemperature);
        airConditionerToggle.onValueChanged.AddListener(OnAirConditionerToggleChanged);

        // 토글 상태 초기화
        airConditionerToggle.isOn = isAirConditionerOn;

        // 텍스트 초기화
        UpdateAirConditionerStatusText();
        UpdateTemperatureText();
    }

    void UPTemperature()
    {
        if (temp < maxTemperature && isAirConditionerOn)
        {
            temp++;
            UpdateTemperatureText();
            SaveTemperature();
        }
    }

    void DownTemperature()
    {
        if (temp > minTemperature && isAirConditionerOn)
        {
            temp--;
            UpdateTemperatureText();
            SaveTemperature();
        }
    }

    void OnAirConditionerToggleChanged(bool isOn)
    {
        isAirConditionerOn = isOn;
        UpdateAirConditionerStatusText();
        UpdateTemperatureText();
        SaveAirConditionerState();
    }

    void UpdateAirConditionerStatusText()
    {
        if (isAirConditionerOn)
        {
            airConditionerStatusText.text = "On";
            temperatureText.gameObject.SetActive(true);
        }
        else
        {
            airConditionerStatusText.text = "Off";
            temperatureText.gameObject.SetActive(false);
        }
    }

    void UpdateTemperatureText()
    {
        if (isAirConditionerOn)
        {
            temperatureText.text = temp.ToString();
        }
    }

    void SaveTemperature()
    {
        // 설정 온도를 PlayerPrefs에 저장
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        PlayerPrefs.SetInt(savedTemperatureKey, temp);
        PlayerPrefs.Save();
    }

    void SaveAirConditionerState()
    {
        // 에어컨 상태를 PlayerPrefs에 저장
        string savedAirConditionerKey = "SavedAirConditioner_" + objectID;
        PlayerPrefs.SetInt(savedAirConditionerKey, isAirConditionerOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 설정 온도 및 에어컨 상태 저장
        SaveTemperature();
        SaveAirConditionerState();
    }
}
