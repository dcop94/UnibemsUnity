using UnityEngine;
using UnityEngine.UI;

public class PowerDataDisplay : MonoBehaviour
{
    public Text powerDataText; // 전력량을 표시할 텍스트 UI
    public int sceneIndex; // 각 씬의 인덱스

    private void Start()
    {
        // 임시로 초기 데이터를 설정 (실제 데이터는 외부에서 받아야 함)
        int initialData = Random.Range(0, 100);
        UpdatePowerData(initialData);
    }

    // 전력량을 업데이트하는 함수
    public void UpdatePowerData(int powerData)
    {
        powerDataText.text = powerData.ToString(); // 전력량을 텍스트로 변환하여 표시
        SceneDataManager.Instance.UpdateSceneData(sceneIndex, powerData); // 중앙 데이터 관리자로 데이터 전송
    }
}
