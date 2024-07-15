using UnityEngine;
using UnityEngine.UI;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject infoPanel; // �г� ������Ʈ

    void Start()
    {
        // ������ �� �г��� ��Ȱ��ȭ
        infoPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        // �г� Ȱ��ȭ
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        // �г� ��Ȱ��ȭ
        infoPanel.SetActive(false);
    }
}
