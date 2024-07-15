using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public RectTransform needleTransform; // �ٴ�
    public Text percentageText; // �ۼ�Ʈ�� ���� ���� (text)
    
    void Start()
    {
        // �ٴ��� �ǹ��� ���� ������ ���� (�Ʒ��� ���)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // �ٴ��� ��Ŀ�� ����
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

    }

    // RectTransform�� ��Ŀ�� �����ϴ� �Լ�
    void SetAnchors(RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
    }

    // 0 ~ 100 �ۼ�Ʈ�� �Է����� �޾� �ٴ��� ������ ����
    public void UpdateNeedle(float percentage)
    {
        // �Է� ���� 0~100 �ۼ�Ʈ�� ġȯ
        percentage = Mathf.Clamp(percentage, 0, 100);

        // �ۼ�Ʈ�� ���� ���� ��� (0~100 �ۼ�Ʈ -> -90�� ~ 90��)
        float angle = Mathf.Lerp(90f, -90f, percentage / 100f);

        // �ٴ��� ȸ�� ����
        needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    
    void Update()
    {
        float testPercentage = Mathf.PingPong(Time.time * 10, 100);
        UpdateNeedle(testPercentage);
    }
    
}
