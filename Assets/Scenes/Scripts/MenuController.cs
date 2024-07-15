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
        // �г� �⺻ ��Ȱ��ȭ
        secondMenuPanel.SetActive(false);
        thirdMenuPanel.SetActive(false);

        firstMenuButton.onClick.AddListener(ToggleSecondMenu);
        secondMenuButton.onClick.AddListener(ToggleThirdMenu);
    }

    void ToggleSecondMenu()
    {
        // �г� ���
        secondMenuPanel.SetActive(!secondMenuPanel.activeSelf);

        // �θ� �г��� ���̾ƿ��� ������ ������Ʈ
        LayoutRebuilder.ForceRebuildLayoutImmediate(secondMenuPanel.GetComponent<RectTransform>());
    }

    void ToggleThirdMenu()
    {
        // �г� ���
        thirdMenuPanel.SetActive(!thirdMenuPanel.activeSelf);

        // �θ� �г��� ���̾ƿ��� ������ ������Ʈ
        LayoutRebuilder.ForceRebuildLayoutImmediate(thirdMenuPanel.GetComponent<RectTransform>());
    }
}
