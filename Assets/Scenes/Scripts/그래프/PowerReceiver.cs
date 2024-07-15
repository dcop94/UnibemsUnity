using UnityEngine;
using UnityEngine.UI;

public class PowerReceiver : MonoBehaviour
{
    public Text powerText;  // ���·��� ǥ���� �ؽ�Ʈ UI ��ü

    void Start()
    {
        // 1�ʸ��� UpdatePowerText �Լ��� ȣ��
        InvokeRepeating("UpdatePowerText", 1f, 1f);
    }

    void UpdatePowerText()
    {
        if (PowerManager.Instance == null)
        {
            Debug.LogError("PowerManager instance is not found!");
            return;
        }

        // ���·��� ���ڷ� ǥ�� (�Ҽ��� ��° �ڸ�����)
        powerText.text = PowerManager.Instance.CurrentPower.ToString("F2");
    }
}
