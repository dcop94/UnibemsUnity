using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText; // 버튼의 텍스트 컴포넌트
    public Color normalColor = Color.white; // 기본 텍스트 색상
    public Color highlightedColor = Color.green; // 강조 텍스트 색상

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
