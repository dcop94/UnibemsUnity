using UnityEngine;
using UnityEngine.UI;

public class DamperGauge : MonoBehaviour
{
    public RectTransform needleTransform; // 바늘
    public Text opennessText; // 개폐율을 표시하는 Text

    private float minAngle = 90f; // 0%일 때의 각도
    private float maxAngle = -90f; // 100%일 때의 각도

    void Start()
    {
        // 바늘의 피벗을 한쪽 끝으로 설정 (아래쪽 가운데)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // 바늘의 앵커를 설정
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

        // 초기 개폐율 설정
        UpdateNeedleFromText();
    }

    // RectTransform의 앵커를 설정하는 함수
    void SetAnchors(RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
    }

    // 텍스트를 정수로 변환하여 바늘의 각도를 조정
    public void UpdateNeedleFromText()
    {
        // 텍스트에서 개폐율 값을 가져와 실수로 변환
        if (float.TryParse(opennessText.text, out float openness))
        {
            // 입력 값을 0~100% 범위로 제한
            openness = Mathf.Clamp(openness, 0f, 100f);

            // 개폐율을 0~100%로 변환하여 각도 계산
            float normalizedOpenness = openness / 100f;
            float angle = Mathf.Lerp(minAngle, maxAngle, normalizedOpenness);

            // 바늘의 회전 적용
            needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogError("Invalid openness input.");
        }
    }
}
