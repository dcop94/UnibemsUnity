using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public RectTransform needleTransform; // �ٴ�
    public Text temperatureText; // �µ��� ǥ���ϴ� Text

    private int minTemperature = 18; // �ּ� �µ�
    private int maxTemperature = 30; // �ִ� �µ�
    private float minAngle = 90f; // 18���� ���� ����
    private float maxAngle = -90f; // 30���� ���� ����

    void Start()
    {
        // �ٴ��� �ǹ��� ���� ������ ���� (�Ʒ��� ���)
        needleTransform.pivot = new Vector2(0.5f, 0f);

        // �ٴ��� ��Ŀ�� ����
        SetAnchors(needleTransform, new Vector2(0.5f, 0.5f));

        // �ʱ� �µ� ����
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
        // �ؽ�Ʈ���� �µ� ���� ������ ������ ��ȯ
        if (int.TryParse(temperatureText.text, out int temperature))
        {
            // �Է� ���� 18~30 ������ ����
            temperature = Mathf.Clamp(temperature, minTemperature, maxTemperature);

            // �µ��� ���� ���� ���
            float normalizedTemperature = (float)(temperature - minTemperature) / (maxTemperature - minTemperature);
            float angle = Mathf.Lerp(minAngle, maxAngle, normalizedTemperature);

            // �ٴ��� ȸ�� ����
            needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogError("Invalid temperature input.");
        }
    }
}
