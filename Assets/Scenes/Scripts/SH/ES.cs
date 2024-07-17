using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES : MonoBehaviour
{
    // DataManger ��ü ���� : ����Ƽ �⺻ Class ��ӹ��� �Լ��� ��� new Ű���� X -> ���� ������Ʈ�� ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ���� 
    public DataManager dataManager;

    // EV �� data ���� list ���� 
    private List<DataManager.ES> esDataList;

    // ������Ʈ���� ������ ��ġID�� ������ �� �ֵ��� public ���� �߰�
    public int esID;

    void Start()
    {
        InvokeRepeating("GenerateESData", 1.0f, 1.0f);
    }

    void GenerateESData()
    {
        // EV�� data ���� list �ʱ�ȭ  
        esDataList = new List<DataManager.ES>();

        if (!FlagSet.sensor_on)
        {
            return;
        }

        DataManager.ES ESData = new DataManager.ES();

        ESData.m_id = esID;

        ESData.m_power = GetDataBasedOnTime();

        esDataList.Add(ESData);

        // ������ ���
        if (dataManager != null)
        {
            dataManager.EsData(esDataList);
        }

        // dataManager ���� �ʱ�ȭ �ȵ� ���
        else
        {
            Debug.LogError("dataManager �ʱ�ȭ �ȵ�!");
        }

    }

    float GetDataBasedOnTime()
    {
        // ���� �ð��� �������� �����͸� ����
        float hour = System.DateTime.Now.Hour;

        if (hour >= 8 && hour < 11)
        {
            return Mathf.Round(Random.Range(1.0f, 1.5f) * 10.0f) / 10.0f;
        }

        else if (hour >= 10 && hour < 20)
        {
            return Mathf.Round(Random.Range(2.0f, 3.0f) * 10.0f) / 10.0f;
        }

        else if (hour >= 20 && hour < 23)
        {
            return Mathf.Round(Random.Range(1.5f, 2.0f) * 10.0f) / 10.0f;
        }

        else
        {
            return Mathf.Round(Random.Range(0.1f, 0.2f) * 10.0f) / 10.0f;
        }
    }
}
