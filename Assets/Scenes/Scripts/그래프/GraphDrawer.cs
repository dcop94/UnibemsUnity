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
        //int[] dataPoints = SceneDataManager.Instance.GetPowerDataPoints(); // SceneDataManager를 통해 데이터를 가져옴
        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30 };
        DrawGraph(valueList); // 그래프를 그림

        //CreateCircle(new Vector2(400, 400));
    }

    // 전달받은 데이터를 기반으로 그래프를 그리는 함수
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


        //ClearGraph(); // 기존 그래프를 지움

        //float graphHeight = graphContainer.sizeDelta.y; // 그래프 컨테이너의 높이
        //float graphWidth = graphContainer.sizeDelta.x; // 그래프 컨테이너의 너비
        //float yMax = 100f; // 예시로 y축 최대값을 100으로 설정
        //float xSpacing = graphWidth / (dataPoints.Length + 1); // 점 간의 x축 간격

        //// 각 데이터 포인트를 그래프 상에 점으로 표현
        //for (int i = 0; i < dataPoints.Length; i++)
        //{
        //    float xPosition = xSpacing * (i + 1); // 점의 x 위치 계산
        //    float yPosition = (dataPoints[i] / yMax) * graphHeight; // 점의 y 위치 계산
        //    CreateCircle(new Vector2(xPosition, yPosition)); // 점 생성
        //}
    }

    // 기존 그래프의 점 오브젝트들을 모두 삭제하는 함수
    private void ClearGraph()
    {
        foreach (GameObject circleObject in circleObjects)
        {
            Destroy(circleObject); // 기존 그래프 점 오브젝트 파괴
        }
        circleObjects.Clear(); // 리스트 초기화
    }

    // 주어진 위치에 새로운 점 오브젝트를 생성하는 함수
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image)); // 새로운 점 오브젝트 생성
        gameObject.transform.SetParent(graphContainer, false); // 그래프 컨테이너의 자식으로 설정
        gameObject.GetComponent<Image>().sprite = circleSprite; // 점 오브젝트에 이미지 설정
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransform 컴포넌트 참조
        rectTransform.anchoredPosition = anchoredPosition; // 점의 위치 설정
        rectTransform.sizeDelta = new Vector2(11, 11); // 점의 크기 설정
        rectTransform.anchorMin = new Vector2(0, 0); // 앵커 설정
        rectTransform.anchorMax = new Vector2(0, 0);
        //circleObjects.Add(gameObject); // 리스트에 점 오브젝트 추가
    }
}
