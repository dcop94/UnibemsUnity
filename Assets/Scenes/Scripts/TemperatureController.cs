using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    public Text currentTemperature; // 현재온도
    public Text temperatureText; // 설정온도
    public Button UpButton;
    public Button DownButton;

    public string objectID; // 오브젝트 고유 ID

    private int currentTemp;
    private int temp;
    private int minTemperature = 18; // 최소 온도
    private int maxTemperature = 30; // 최대 온도

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

        // 버튼 이벤트 등록
        UpButton.onClick.AddListener(UPTemperature);
        DownButton.onClick.AddListener(DownTemperature);

        // 텍스트 초기화
        UpdateTemperatureText();
    }

    void UPTemperature()
    {
        if (temp < maxTemperature)
        {
            temp++;
            UpdateTemperatureText();
            SaveTemperature();
        }
    }

    void DownTemperature()
    {
        if (temp > minTemperature)
        {
            temp--;
            UpdateTemperatureText();
            SaveTemperature();
        }
    }

    void UpdateTemperatureText()
    {
        temperatureText.text = temp.ToString();
    }

    void SaveTemperature()
    {
        // 설정 온도를 PlayerPrefs에 저장
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        PlayerPrefs.SetInt(savedTemperatureKey, temp);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 설정 온도 저장
        SaveTemperature();
    }
}
