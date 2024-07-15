using UnityEngine;

public class PowerUpdater : MonoBehaviour
{
    void Start()
    {
        // 1초마다 UpdatePower 함수를 호출
        InvokeRepeating("UpdatePower", 1f, 1f);
    }

    void UpdatePower()
    {
        if (PowerManager.Instance != null)
        {
            //PowerManager.Instance.UpdatePower();
        }
    }
}
