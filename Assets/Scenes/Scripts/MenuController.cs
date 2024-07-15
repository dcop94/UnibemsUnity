using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button firstMenuButton;
    public GameObject secondMenuPanel;
    public Button secondMenuButton;
    public GameObject thirdMenuPanel;
    
    void Start()
    {
        // 패널 기본 비활성화
        secondMenuPanel.SetActive(false);
        thirdMenuPanel.SetActive(false);

        firstMenuButton.onClick.AddListener(ToggleSecondMenu);
        secondMenuButton.onClick.AddListener(ToggleThirdMenu);
    }

    void ToggleSecondMenu()
    {
        // 패널 토글
        secondMenuPanel.SetActive(!secondMenuPanel.activeSelf);

        // 부모 패널의 레이아웃을 강제로 업데이트
        LayoutRebuilder.ForceRebuildLayoutImmediate(secondMenuPanel.GetComponent<RectTransform>());
    }

    void ToggleThirdMenu()
    {
        // 패널 토글
        thirdMenuPanel.SetActive(!thirdMenuPanel.activeSelf);

        // 부모 패널의 레이아웃을 강제로 업데이트
        LayoutRebuilder.ForceRebuildLayoutImmediate(thirdMenuPanel.GetComponent<RectTransform>());
    }
}
