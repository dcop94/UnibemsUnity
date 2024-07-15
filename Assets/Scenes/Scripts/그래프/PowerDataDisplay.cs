using UnityEngine;
using UnityEngine.UI;

public class PowerDataDisplay : MonoBehaviour
{
    public Text powerDataText; // ���·��� ǥ���� �ؽ�Ʈ UI
    public int sceneIndex; // �� ���� �ε���

    private void Start()
    {
        // �ӽ÷� �ʱ� �����͸� ���� (���� �����ʹ� �ܺο��� �޾ƾ� ��)
        int initialData = Random.Range(0, 100);
        UpdatePowerData(initialData);
    }

    // ���·��� ������Ʈ�ϴ� �Լ�
    public void UpdatePowerData(int powerData)
    {
        powerDataText.text = powerData.ToString(); // ���·��� �ؽ�Ʈ�� ��ȯ�Ͽ� ǥ��
        SceneDataManager.Instance.UpdateSceneData(sceneIndex, powerData); // �߾� ������ �����ڷ� ������ ����
    }
}
