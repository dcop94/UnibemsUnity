using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    public Text powerText;  // ���·��� ǥ���� �ؽ�Ʈ UI ��ü

    private float currentPower = 0f;  // ���� ���·�
    private float minPower = 50f;  // �ּ� ���·�
    private float maxPower = 200f;  // �ִ� ���·�

    void Start()
    {
        // 1�ʸ��� UpdatePower �Լ��� ȣ��
        InvokeRepeating("UpdatePower", 1f, 1f);
    }

    void UpdatePower()
    {
        // ������ ���·� ���� (minPower �̻�, maxPower �̸�)
        currentPower = Random.Range(minPower, maxPower);

        // ���·��� ���ڷ� ǥ�� (�Ҽ��� ��° �ڸ�����)
        powerText.text = currentPower.ToString("F2");
    }
}
