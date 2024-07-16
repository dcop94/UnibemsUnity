using UnityEngine;
using UnityEngine.UI;

public class DamperController : MonoBehaviour
{
    public Text currentOpenness; // 현재 개폐율
    public Text opennessText; // 설정 개폐율
    public Button UpButton;
    public Button DownButton;

    public string objectID; // 오브젝트 고유 ID

    private int currentOpen;
    private int openness;
    private int minOpenness = 0; // 최소 개폐율
    private int maxOpenness = 100; // 최대 개폐율

    void Start()
    {
        // 텍스트의 값을 int로 변경
        currentOpen = int.Parse(currentOpenness.text);

        // PlayerPrefs에서 설정 퍼센트 불러오기
        string savedOpennessKey = "SavedOpenness_" + objectID;
        if(PlayerPrefs.HasKey(savedOpennessKey))
        {
            openness = PlayerPrefs.GetInt(savedOpennessKey);
        }
        else
        {
            // 설정 개폐율이 저장되어 있지 않으면 현재 온도로 초기화
            openness = currentOpen;
        }

        // 버튼 이벤트 등록
        UpButton.onClick.AddListener(UPOpenness);
        DownButton.onClick.AddListener(DownOpenness);

        // 텍스트 초기화
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
        // 설정 개폐율을 PlayerPrefs에 저장
        string savedOpennessKey = "SavedOpenness_" + objectID;
        PlayerPrefs.SetInt(savedOpennessKey, openness);
        PlayerPrefs.Save();
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 설정 개폐율 저장
        SaveOpenness();
    }
}
