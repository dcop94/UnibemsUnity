using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public RectTransform needleTransform; // 바늘
    public Text temperatureText; // 온도를 표시하는 Text

    private int minTemperature = 18; // 최소 온도
    private int maxTemperature = 30; // 최대 온도
    private float minAngle = 90f; // 18도일 때의 각도
    private float maxAngle = -90f; // 30도일 때의 각도

    void Start()
    {
        // 바늘의 피벗을 한쪽 끝으로 설정 (아래쪽 가운데)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // 바늘의 앵커를 설정
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

        // 초기 온도 설정
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
        // 텍스트에서 온도 값을 가져와 정수로 변환
        if (int.TryParse(temperatureText.text, out int temperature))
        {
            // 입력 값을 18~30 범위로 제한
            temperature = Mathf.Clamp(temperature, minTemperature, maxTemperature);

            // 온도에 따른 각도 계산
            float normalizedTemperature = (float)(temperature - minTemperature) / (maxTemperature - minTemperature);
            float angle = Mathf.Lerp(minAngle, maxAngle, normalizedTemperature);

            // 바늘의 회전 적용
            needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogError("Invalid temperature input.");
        }
    }
}
