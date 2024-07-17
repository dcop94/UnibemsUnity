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

    // ��ư�� Ŭ������ ��
    public void OnButtonClick()
    {
        isOn = !isOn;
        UpdateText();
    }

    // on, off �ؽ�Ʈ ���
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
