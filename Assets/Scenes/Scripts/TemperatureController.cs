using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    public Text currentTemperature; // ����µ�
    public Text temperatureText; // �����µ�
    public Button UpButton;
    public Button DownButton;

    public string objectID; // ������Ʈ ���� ID

    private int currentTemp;
    private int temp;
    private int minTemperature = 18; // �ּ� �µ�
    private int maxTemperature = 30; // �ִ� �µ�

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

        // ��ư �̺�Ʈ ���
        UpButton.onClick.AddListener(UPTemperature);
        DownButton.onClick.AddListener(DownTemperature);

        // �ؽ�Ʈ �ʱ�ȭ
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
        // ���� �µ��� PlayerPrefs�� ����
        string savedTemperatureKey = "SavedTemperature_" + objectID;
        PlayerPrefs.SetInt(savedTemperatureKey, temp);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ�� �� ���� �µ� ����
        SaveTemperature();
    }
}
