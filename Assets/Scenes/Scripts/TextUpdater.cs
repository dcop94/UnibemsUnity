using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public Text otherText; // 가져올 text
    public Text myText; // 나타낼 text

    void Start()
    {
        // myText가 null이 아니면서 자신의 Text 컴포넌트일 경우에만 작업을 진행
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
