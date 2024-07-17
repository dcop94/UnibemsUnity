using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Text numberText;
    public Slider slider;
    public string uniqueID;  // ������ ID ����

    private int currentValue;

    void Start()
    {
        // ����� �����̴� ���� �ҷ�����
        currentValue = PlayerPrefs.GetInt(uniqueID, 0);
        UpdateText();

        // �����̴� �� ����
        slider.value = currentValue;
        slider.maxValue = 100;
        slider.minValue = 0;

        // �����̴� ���� ����� �� ȣ��� �޼ҵ� ����
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
