using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI; // for Text


public class DataManagerRead : MonoBehaviour
{
    private NetworkStream networkStream;
    private DataManager dataManager;


    // UI Text ������Ʈ
    public Text avgTempText;
    public Text avgHumidityText;
    public Text avgCo2Text;
    public Text totalPowerText;
    public Text sensor2Text, sensor3Text;
    public Text ventil2Text, ventil3Text;
    public Text havc1Text, havc2Text, havc3Text, havc4Text;
    public Text sensorTempText, sensorHumidityText, sensorCo2Text;
    public Text ventilWindText, ventilPowerText;
    public Text ev1Text, es1Text;


    void Start()
    {
        //// DataManager �ν��Ͻ� ã�� : DataManager �ν��Ͻ��� ã�Ƽ� ��Ʈ��ũ ��Ʈ���� �����ϱ� ����
        dataManager = FindObjectOfType<DataManager>();


        if (dataManager != null)
        {      
            // DataManager���� ��Ʈ��ũ ��Ʈ�� ��������
            networkStream = dataManager.GetNetworkStream();
            Debug.Log("get networkstream");

            if (networkStream != null)
            {
                Debug.Log("NetworkStream retrieved successfully.");
                // �������� �����͸� �б� ����
                // 2�� �Ŀ� StartReceiving �޼��带 ȣ���ϰ�, �� ���ķ� �� 1�ʸ��� ȣ��
                Debug.Log("Invoke Repeating.");
                InvokeRepeating("ReceiveData", 2f, 3f);
            }
            else
            {
                Debug.LogError("Network stream is null.");
            }
        }

        else
        {
            Debug.LogError("DataManager instance not found.");
        }



    }



    // �������� �����͸� �о�� 
    private void ReceiveData()
    {
        Debug.Log("ReceiveData");
        //  networkStream ��ȿ�ϰ� �б� ������ ��Ȳ
        if (/*networkStream != null &&*/ networkStream.CanRead)
        {
            try
            {
                // ������ ���� ������ ������ �غ�
                byte[] bytes = new byte[1024]; // 1024 : Max buffer size

                Debug.Log("��");
                // Read() - ������ ���� ������ ����
                int bytesRead = networkStream.Read(bytes, 0, bytes.Length);
                Debug.Log("��");

                if (bytesRead > 0)
                {
                    // C#������ String Ÿ������ �ؽ�Ʈ ������ ǥ��
                    string serverToClientData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    //Debug.Log("���ŵ� ������: " + serverToClientData);


                    // �����͸� �Ľ�
                    string[] parsedData = serverToClientData.Split(';');

                    Debug.Log("parsedData[0]: " + parsedData[0]);


                    // Text ������Ʈ�� null���� Ȯ��
                    //CheckTextComponents();

                    /* Ȯ�ο�
                    if (parsedData.Length >= 14)
                    {
                        avgSensorDataText.text = parsedData[0];
                        totalPowerText.text = parsedData[1];
                        havc1Text.text = parsedData[2];
                        havc2Text.text = parsedData[3];
                        havc3Text.text = parsedData[4];
                        havc4Text.text = parsedData[5];
                        sensor1Text.text = parsedData[6];
                        sensor2Text.text = parsedData[7];
                        sensor3Text.text = parsedData[8];
                        ventil1Text.text = parsedData[9];
                        ventil2Text.text = parsedData[10];
                        ventil3Text.text = parsedData[11];
                        ev1Text.text = parsedData[12];
                        es1Text.text = parsedData[13];
                    }
                    */
                    Debug.Log("parsedData.Lenght start");
                    // ���� ���� �����Ͽ� UI�� ǥ��
                    if (parsedData.Length >= 14)
                    {
                        Debug.Log("�Ľ� ����");
                        // ��� ���� �����͸� �Ľ��Ͽ� UI�� ����
                        ParseAverageSensorData(parsedData[0]);

                        // �ٸ� ������ �Ľ��Ͽ� UI�� ����
                        totalPowerText.text = ExtractSingleValue(parsedData[1]);
                        havc1Text.text = ExtractSingleValue(parsedData[2]);
                        havc2Text.text = ExtractSingleValue(parsedData[3]);
                        havc3Text.text = ExtractSingleValue(parsedData[4]);
                        havc4Text.text = ExtractSingleValue(parsedData[5]);

                        ParseSensorValues(parsedData[6]);
                        sensor2Text.text = ExtractSingleValue(parsedData[7]);
                        sensor3Text.text = ExtractSingleValue(parsedData[8]);
                        ParseVentilatorValues(parsedData[9]);
                        ventil2Text.text = ExtractSingleValue(parsedData[10]);
                        ventil3Text.text = ExtractSingleValue(parsedData[11]);
                        ev1Text.text = ExtractSingleValue(parsedData[12]);
                        es1Text.text = ExtractSingleValue(parsedData[13]);
                        Debug.Log("�Ľ� ��");
                    }

                    else
                    {
                        Debug.Log("���ŵ� ������ ���� ����");
                    }


                }
            }
            catch (Exception e)
            {
                Debug.LogError("�����͸� �д� �� ���� �߻�: " + e.Message);
            }
        }
        // ��������
        else
        {
            Debug.Log("NetworkStream Get failed.");
        }
    }

    private void ParseAverageSensorData(string data)
    {
        // Split the average sensor data by ',' to get individual key-value pairs
        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            // Split each key-value pair by ':' to separate the key and value
            string[] keyValue = part.Split(':');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // Assign the value to the corresponding Text component based on the key
                if (key.Equals("Avg_Temp"))
                {
                    avgTempText.text = value;
                }
                else if (key.Equals("Avg_Humidity"))
                {
                    avgHumidityText.text = value;
                }
                else if (key.Equals("Avg_Co2"))
                {
                    avgCo2Text.text = value;
                }
            }
        }
    }

    private void ParseSensorValues(string data)
    {
        // ���� �����͸� ','�� �и��Ͽ� ���� Ű-�� ������ �����ϴ�.
        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            // �� Ű-�� ���� ':'�� �и��Ͽ� Ű�� ���� �����ϴ�.
            string[] keyValue = part.Split(':');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // Ű�� ���� �ش� Text ������Ʈ�� ���� �����մϴ�.
                if (key.Equals("s1_Temp"))
                {
                    sensorTempText.text = value;
                }
                else if (key.Equals("s1_Humidity"))
                {
                    sensorHumidityText.text = value;
                }
                else if (key.Equals("s1_Co2"))
                {
                    sensorCo2Text.text = value;
                }
            }
        }
    }

    private void ParseVentilatorValues(string data)
    {
        // ��ǳ�� �����͸� ','�� �и��Ͽ� ���� Ű-�� ������ �����ϴ�.
        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            // �� Ű-�� ���� ':'�� �и��Ͽ� Ű�� ���� �����ϴ�.
            string[] keyValue = part.Split(':');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // Ű�� ���� �ش� Text ������Ʈ�� ���� �����մϴ�.
                if (key.Equals("v1_Wind"))
                {
                    ventilWindText.text = value;
                }
                else if (key.Equals("v1_Power"))
                {
                    ventilPowerText.text = value;
                }
            }
        }
    }

    private string ExtractSingleValue(string data)
    {
        // �����͸� ':'�� �и��Ͽ� Ű�� ���� �����ϴ�.
        string[] keyValue = data.Split(':');
        // ������ �ùٸ��� ���� ��ȯ�ϰ�, �׷��� ������ "0"�� ��ȯ�մϴ�.
        return keyValue.Length == 2 ? keyValue[1].Trim() : "0";
    }

    void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �ݺ� ȣ���� ���
        CancelInvoke("ReceiveData");
    }


}

