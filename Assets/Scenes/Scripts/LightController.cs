using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Text numberText;
    public Slider slider;
    public string uniqueID;  // 고유한 ID 설정

    private int currentValue;

    void Start()
    {
        // 저장된 슬라이더 값을 불러오기
        currentValue = PlayerPrefs.GetInt(uniqueID, 0);
        UpdateText();

        // 슬라이더 값 설정
        slider.value = currentValue;
        slider.maxValue = 100;
        slider.minValue = 0;

        // 슬라이더 값이 변경될 때 호출될 메소드 연결
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        currentValue = Mathf.RoundToInt(value);
        UpdateText();
        SaveValue();
    }

    void UpdateText()
    {
        numberText.text = currentValue.ToString();
    }

    void SaveValue()
    {
        PlayerPrefs.SetInt(uniqueID, currentValue);
        PlayerPrefs.Save();
    }
}
