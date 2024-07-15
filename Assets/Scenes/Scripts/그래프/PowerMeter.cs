using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    public Text powerText;  // 전력량을 표시할 텍스트 UI 객체

    private float currentPower = 0f;  // 현재 전력량
    private float minPower = 50f;  // 최소 전력량
    private float maxPower = 200f;  // 최대 전력량

    void Start()
    {
        // 1초마다 UpdatePower 함수를 호출
        InvokeRepeating("UpdatePower", 1f, 1f);
    }

    void UpdatePower()
    {
        // 랜덤한 전력량 생성 (minPower 이상, maxPower 미만)
        currentPower = Random.Range(minPower, maxPower);

        // 전력량을 숫자로 표시 (소수점 둘째 자리까지)
        powerText.text = currentPower.ToString("F2");
    }
}
