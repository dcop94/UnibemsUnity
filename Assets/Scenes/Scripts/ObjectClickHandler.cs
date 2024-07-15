using UnityEngine;
using UnityEngine.UI;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject infoPanel; // 패널 오브젝트

    void Start()
    {
        // 시작할 때 패널을 비활성화
        infoPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        // 패널 활성화
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        // 패널 비활성화
        infoPanel.SetActive(false);
    }
}
