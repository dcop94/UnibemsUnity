using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText; // ��ư�� �ؽ�Ʈ ������Ʈ
    public Color normalColor = Color.white; // �⺻ �ؽ�Ʈ ����
    public Color highlightedColor = Color.green; // ���� �ؽ�Ʈ ����

    private void Start()
    {
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = highlightedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }
}
