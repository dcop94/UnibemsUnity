using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    public Text currentTemperature; // ����µ�
    public Text temperatureText; // �����µ�
    public Text airConditionerStatusText; // ������ ���� �ؽ�Ʈ
    public Button UpButton;
    public Button DownButton;
    public Toggle airConditionerToggle; // ������ On/Off ���

    public string objectID; // ������Ʈ ���� ID

    private int currentTemp;
    private int temp;
    private int minTemperature = 18; // �ּ� �µ�
    private int maxTemperature = 30; // �ִ� �µ�

    private bool isAirConditionerOn; // ������ ����

    void Start()
    {
        // �ؽ�Ʈ�� ���� int���� ����
        currentTemp = int.Parse(currentTemperature.text);

        // PlayerPrefs���� ���� �µ��� �ҷ�����
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        if (PlayerPrefs.HasKey(savedTemperatureKey))
        {
            temp = PlayerPrefs.GetInt(savedTemperatureKey);
        }
        else
        {
            // ���� �µ��� ����Ǿ� ���� ������ ���� �µ��� �ʱ�ȭ
            temp = currentTemp;
        }

        // PlayerPrefs���� ������ ���¸� �ҷ�����
        string savedAirConditionerKey = "SavedAirConditioner_" + objectID;
        if (PlayerPrefs.HasKey(savedAirConditionerKey))
        {
            isAirConditionerOn = PlayerPrefs.GetInt(savedAirConditionerKey) == 1;
        }
        else
        {
            isAirConditionerOn = false;
        }

        // ��ư �̺�Ʈ ���
        UpButton.onClick.AddListener(UPTemperature);
        DownButton.onClick.AddListener(DownTemperature);
        airConditionerToggle.onValueChanged.AddListener(OnAirConditionerToggleChanged);

        // ��� ���� �ʱ�ȭ
        airConditionerToggle.isOn = isAirConditionerOn;

        // �ؽ�Ʈ �ʱ�ȭ
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
        // ���� �µ��� PlayerPrefs�� ����
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        PlayerPrefs.SetInt(savedTemperatureKey, temp);
        PlayerPrefs.Save();
    }

    void SaveAirConditionerState()
    {
        // ������ ���¸� PlayerPrefs�� ����
        string savedAirConditionerKey = "SavedAirConditioner_" + objectID;
        PlayerPrefs.SetInt(savedAirConditionerKey, isAirConditionerOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ�� �� ���� �µ� �� ������ ���� ����
        SaveTemperature();
        SaveAirConditionerState();
    }
}
