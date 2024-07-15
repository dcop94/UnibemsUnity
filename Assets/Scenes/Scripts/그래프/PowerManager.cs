using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }

    public float CurrentPower { get; private set; }

    private float minPower = 50f;
    private float maxPower = 200f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GeneratePower()
    {
        // 랜덤한 전력량 생성 (minPower 이상, maxPower 미만)
        CurrentPower = Random.Range(minPower, maxPower);
    }
}
