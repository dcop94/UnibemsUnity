using UnityEngine;
using UnityEngine.UI;

public class DamperController : MonoBehaviour
{
    public Text currentOpenness; // ���� ������
    public Text opennessText; // ���� ������
    public Button UpButton;
    public Button DownButton;

    public string objectID; // ������Ʈ ���� ID

    private int currentOpen;
    private int openness;
    private int minOpenness = 0; // �ּ� ������
    private int maxOpenness = 100; // �ִ� ������

    void Start()
    {
        // �ؽ�Ʈ�� ���� int�� ����
        currentOpen = int.Parse(currentOpenness.text);

        // PlayerPrefs���� ���� �ۼ�Ʈ �ҷ�����
        string savedOpennessKey = "SavedOpenness_" + objectID;
        if(PlayerPrefs.HasKey(savedOpennessKey))
        {
            openness = PlayerPrefs.GetInt(savedOpennessKey);
        }
        else
        {
            // ���� �������� ����Ǿ� ���� ������ ���� �µ��� �ʱ�ȭ
            openness = currentOpen;
        }

        // ��ư �̺�Ʈ ���
        UpButton.onClick.AddListener(UPOpenness);
        DownButton.onClick.AddListener(DownOpenness);

        // �ؽ�Ʈ �ʱ�ȭ
        UpdateOpennessText();
    }

    void UPOpenness()
    {
        if (openness < maxOpenness)
        {
            openness += 10;
            UpdateOpennessText();
            SaveOpenness();
        }
    }

    void DownOpenness()
    {
        if (openness> minOpenness)
        {
            openness -= 10;
            UpdateOpennessText();
            SaveOpenness();
        }
    }

    void UpdateOpennessText()
    {
        opennessText.text = openness.ToString();
    }

    void SaveOpenness()
    {
        // ���� �������� PlayerPrefs�� ����
        string savedOpennessKey = "SavedOpenness_" + objectID;
        PlayerPrefs.SetInt(savedOpennessKey, openness);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ�� �� ���� ������ ����
        SaveOpenness();
    }
}
