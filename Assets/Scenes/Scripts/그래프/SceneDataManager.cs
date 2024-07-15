using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public static SceneDataManager Instance;

    private int[] powerDataPoints = new int[9]; // 아홉 씬의 데이터를 저장할 배열

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 객체가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 특정 씬의 데이터를 업데이트하는 함수
    public void UpdateSceneData(int sceneIndex, int powerData)
    {
        if (sceneIndex >= 0 && sceneIndex < powerDataPoints.Length)
        {
            powerDataPoints[sceneIndex] = powerData;
        }
    }

    // 모든 씬의 데이터를 반환하는 함수
    public int[] GetPowerDataPoints()
    {
        return powerDataPoints;
    }
}
