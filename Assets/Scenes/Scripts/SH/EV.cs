using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EV : MonoBehaviour
{
    // DataManger ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ���� 
    //public DataManager dataManager;

    // EV �� data ���� list ���� 
    private List<DataManager.EV> evDataList;

    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int evID;
    void Start()
    {
        InvokeRepeating("GenerateEVData", 1.0f, 1.0f);
    }

    void GenerateEVData()
    {
        // EV�� data ���� list �ʱ�ȭ  
        evDataList = new List<DataManager.EV>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        DataManager.EV EVData = new DataManager.EV();

        EVData.m_id = evID;

        EVData.m_power = GetDataBasedOnTime();

        evDataList.Add(EVData);

        // ������ ���
        if (DataManager.Instance != null)
        {
            DataManager.Instance.EvData(evDataList);
        }

        // dataManager ���� �ʱ�ȭ �ȵ� ���
        else
        {
            Debug.LogError("dataManager �ʱ�ȭ �ȵ�!");
        }

    }

    float GetDataBasedOnTime()
    {
        // ���� �ð��� �������� ������ ����
        float hour = System.DateTime.Now.Hour;

        if (hour >= 8 && hour < 11)
        {
            return Mathf.Round(Random.Range(2.0f, 2.5f) * 10.0f) / 10.0f;
        }

        else if (hour >= 10 && hour < 20)
        {
            return Mathf.Round(Random.Range(3.0f, 5.0f) * 10.0f) / 10.0f;
        }

        else if (hour >= 20 && hour < 23)
        {
            return Mathf.Round(Random.Range(2.0f, 3.0f) * 10.0f) / 10.0f;
        }

        else
        {
            return Mathf.Round(Random.Range(0.1f, 0.2f) * 10.0f) / 10.0f;
        }
    }
}
