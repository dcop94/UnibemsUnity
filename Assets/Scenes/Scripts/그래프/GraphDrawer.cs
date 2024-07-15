using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawer : MonoBehaviour
{
    public RectTransform graphContainer;
    public Sprite circleSprite;

    private List<GameObject> circleObjects = new List<GameObject>();

    void Start()
    {
        //int[] dataPoints = SceneDataManager.Instance.GetPowerDataPoints(); // SceneDataManager�� ���� �����͸� ������
        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30 };
        DrawGraph(valueList); // �׷����� �׸�

        //CreateCircle(new Vector2(400, 400));
    }

    // ���޹��� �����͸� ������� �׷����� �׸��� �Լ�
    private void DrawGraph(List<int> dataPoints)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximun = 100f;
        float xSize = 50f;

        for(int i = 0; i < dataPoints.Count; i++)
        {
            float xPos = i * xSize;
            float yPos = (dataPoints[i] / yMaximun) * graphHeight;
            CreateCircle(new Vector2(xPos, yPos));
        }


        //ClearGraph(); // ���� �׷����� ����

        //float graphHeight = graphContainer.sizeDelta.y; // �׷��� �����̳��� ����
        //float graphWidth = graphContainer.sizeDelta.x; // �׷��� �����̳��� �ʺ�
        //float yMax = 100f; // ���÷� y�� �ִ밪�� 100���� ����
        //float xSpacing = graphWidth / (dataPoints.Length + 1); // �� ���� x�� ����

        //// �� ������ ����Ʈ�� �׷��� �� ������ ǥ��
        //for (int i = 0; i < dataPoints.Length; i++)
        //{
        //    float xPosition = xSpacing * (i + 1); // ���� x ��ġ ���
        //    float yPosition = (dataPoints[i] / yMax) * graphHeight; // ���� y ��ġ ���
        //    CreateCircle(new Vector2(xPosition, yPosition)); // �� ����
        //}
    }

    // ���� �׷����� �� ������Ʈ���� ��� �����ϴ� �Լ�
    private void ClearGraph()
    {
        foreach (GameObject circleObject in circleObjects)
        {
            Destroy(circleObject); // ���� �׷��� �� ������Ʈ �ı�
        }
        circleObjects.Clear(); // ����Ʈ �ʱ�ȭ
    }

    // �־��� ��ġ�� ���ο� �� ������Ʈ�� �����ϴ� �Լ�
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image)); // ���ο� �� ������Ʈ ����
        gameObject.transform.SetParent(graphContainer, false); // �׷��� �����̳��� �ڽ����� ����
        gameObject.GetComponent<Image>().sprite = circleSprite; // �� ������Ʈ�� �̹��� ����
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransform ������Ʈ ����
        rectTransform.anchoredPosition = anchoredPosition; // ���� ��ġ ����
        rectTransform.sizeDelta = new Vector2(11, 11); // ���� ũ�� ����
        rectTransform.anchorMin = new Vector2(0, 0); // ��Ŀ ����
        rectTransform.anchorMax = new Vector2(0, 0);
        //circleObjects.Add(gameObject); // ����Ʈ�� �� ������Ʈ �߰�
    }
}
