using UnityEngine;
using UnityEngine.UI;

public class PowerReceiver : MonoBehaviour
{
    public Text powerText;  // 전력량을 표시할 텍스트 UI 객체

    void Start()
    {
        // 1초마다 UpdatePowerText 함수를 호출
        InvokeRepeating("UpdatePowerText", 1f, 1f);
    }

    void UpdatePowerText()
    {
        if (PowerManager.Instance == null)
        {
            Debug.LogError("PowerManager instance is not found!");
            return;
        }

        // 전력량을 숫자로 표시 (소수점 둘째 자리까지)
        powerText.text = PowerManager.Instance.CurrentPower.ToString("F2");
    }
}
