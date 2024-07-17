using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Text statusText;
    private bool isOn = false;

    void Start()
    {
        UpdateText();
    }

    // 버튼을 클릭했을 때
    public void OnButtonClick()
    {
        isOn = !isOn;
        UpdateText();
    }

    // on, off 텍스트 출력
    private void UpdateText()
    {
        if (isOn)
        {
            statusText.text = "On";
        }
        else
        {
            statusText.text = "Off";
        }
    }
}
