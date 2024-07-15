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
        // ������ ���·� ���� (minPower �̻�, maxPower �̸�)
        CurrentPower = Random.Range(minPower, maxPower);
    }
}
