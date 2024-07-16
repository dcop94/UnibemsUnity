using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public Text otherText; // ������ text
    public Text myText; // ��Ÿ�� text

    void Start()
    {
        // myText�� null�� �ƴϸ鼭 �ڽ��� Text ������Ʈ�� ��쿡�� �۾��� ����
        if (myText != null && myText.GetType() == typeof(Text))
        {
            if (otherText != null)
            {
                myText.text = otherText.text;
            }
            else
            {
                Debug.LogError("Other Text Component is not assigned!");
            }
        }
        else
        {
            Debug.LogError("My Text Component is not assigned or is not of type Text!");
        }
    }
}
