using UnityEngine;

public class PowerUpdater : MonoBehaviour
{
    void Start()
    {
        // 1�ʸ��� UpdatePower �Լ��� ȣ��
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
