using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public RectTransform needleTransform; // 바늘
    public Text percentageText; // 퍼센트에 의한 각도 (text)
    
    void Start()
    {
        // 바늘의 피벗을 한쪽 끝으로 설정 (아래쪽 가운데)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // 바늘의 앵커를 설정
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

    }

    // RectTransform의 앵커를 설정하는 함수
    void SetAnchors(RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
    }

    // 0 ~ 100 퍼센트를 입력으로 받아 바늘의 각도를 조정
    public void UpdateNeedle(float percentage)
    {
        // 입력 값을 0~100 퍼센트로 치환
        percentage = Mathf.Clamp(percentage, 0, 100);

        // 퍼센트에 따라 각도 계산 (0~100 퍼센트 -> -90도 ~ 90도)
        float angle = Mathf.Lerp(90f, -90f, percentage / 100f);

        // 바늘의 회전 적용
        needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    
    void Update()
    {
        float testPercentage = Mathf.PingPong(Time.time * 10, 100);
        UpdateNeedle(testPercentage);
    }
    
}
