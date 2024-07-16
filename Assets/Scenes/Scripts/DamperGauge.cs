using UnityEngine;
using UnityEngine.UI;

public class DamperGauge : MonoBehaviour
{
    public RectTransform needleTransform; // �ٴ�
    public Text opennessText; // �������� ǥ���ϴ� Text

    private float minAngle = 90f; // 0%�� ���� ����
    private float maxAngle = -90f; // 100%�� ���� ����

    void Start()
    {
        // �ٴ��� �ǹ��� ���� ������ ���� (�Ʒ��� ���)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // �ٴ��� ��Ŀ�� ����
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

        // �ʱ� ������ ����
        UpdateNeedleFromText();
    }

    // RectTransform�� ��Ŀ�� �����ϴ� �Լ�
    void SetAnchors(RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
    }

    // �ؽ�Ʈ�� ������ ��ȯ�Ͽ� �ٴ��� ������ ����
    public void UpdateNeedleFromText()
    {
        // �ؽ�Ʈ���� ������ ���� ������ �Ǽ��� ��ȯ
        if (float.TryParse(opennessText.text, out float openness))
        {
            // �Է� ���� 0~100% ������ ����
            openness = Mathf.Clamp(openness, 0f, 100f);

            // �������� 0~100%�� ��ȯ�Ͽ� ���� ���
            float normalizedOpenness = openness / 100f;
            float angle = Mathf.Lerp(minAngle, maxAngle, normalizedOpenness);

            // �ٴ��� ȸ�� ����
            needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogError("Invalid openness input.");
        }
    }
}
