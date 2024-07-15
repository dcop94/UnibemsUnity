using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public static SceneDataManager Instance;

    private int[] powerDataPoints = new int[9]; // ��ȩ ���� �����͸� ������ �迭

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ��ü�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ư�� ���� �����͸� ������Ʈ�ϴ� �Լ�
    public void UpdateSceneData(int sceneIndex, int powerData)
    {
        if (sceneIndex >= 0 && sceneIndex < powerDataPoints.Length)
        {
            powerDataPoints[sceneIndex] = powerData;
        }
    }

    // ��� ���� �����͸� ��ȯ�ϴ� �Լ�
    public int[] GetPowerDataPoints()
    {
        return powerDataPoints;
    }
}
