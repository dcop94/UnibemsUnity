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
    //public Text sensor2Text, sensor3Text;
    //public Text ventil2Text, ventil3Text;
    //public Text havc1Text, havc2Text, havc3Text, havc4Text;
    public Text havcSetTempText, havcPowerText;
    public Text havc2SetTempText, havc2PowerText;
    public Text havc3SetTempText, havc3PowerText;
    public Text havc4SetTempText, havc4PowerText;
    public Text sensorTempText, sensorHumidityText, sensorCo2Text;
    public Text sensor2TempText, sensor2HumidityText, sensor2Co2Text;
    public Text sensor3TempText, sensor3HumidityText, sensor3Co2Text;
    public Text ventilWindText, ventilPowerText;
    public Text ventil2WindText, ventil2PowerText;
    public Text ventil3WindText, ventil3PowerText;
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

                // ������ ������ ������ �Ϸ� �ƴ���,
                if (DataManager.b_send == true)
                {
                    int bytesRead = networkStream.Read(bytes, 0, bytes.Length);
                    Debug.Log("��");
                    DataManager.b_send = false;

                    if (bytesRead > 0)
                    {
                        // C#������ String Ÿ������ �ؽ�Ʈ ������ ǥ��
                        string serverToClientData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        //Debug.Log("���ŵ� ������: " + serverToClientData);


                        // �����͸� �Ľ�
                        string[] parsedData = serverToClientData.Split(';');

                        Debug.Log("parsedData[0]: " + parsedData[0]);
                        Debug.Log("parsedData[1]: " + parsedData[1]);
                        Debug.Log("parsedData[2]: " + parsedData[2]);
                        Debug.Log("parsedData[3]: " + parsedData[3]);
                        Debug.Log("parsedData[4]: " + parsedData[4]);
                        Debug.Log("parsedData[5]: " + parsedData[5]);
                        Debug.Log("parsedData[6]: " + parsedData[6]);
                        Debug.Log("parsedData[7]: " + parsedData[7]);
                        Debug.Log("parsedData[8]: " + parsedData[8]);
                        Debug.Log("parsedData[9]: " + parsedData[9]);
                        Debug.Log("parsedData[10]: " + parsedData[10]);
                        Debug.Log("parsedData[11]: " + parsedData[11]);
                        Debug.Log("parsedData[12]: " + parsedData[12]);
                        Debug.Log("parsedData[13]: " + parsedData[13]);
                        

                        // Text ������Ʈ�� null���� Ȯ��
                        //CheckTextComponents();

                        // HAVC ���� ������ ����
                        Debug.Log("parsedData.Lenght start");
                        // ���� ���� �����Ͽ� UI�� ǥ��
                        if (parsedData.Length >= 14)
                        {
                            Debug.Log("�Ľ� ����");
                            // ��� ���� �����͸� �Ľ��Ͽ� UI�� ����
                            ParseAverageSensorData(parsedData[0]);

                            // �ٸ� ������ �Ľ��Ͽ� UI�� ����
                            totalPowerText.text = ExtractSingleValue(parsedData[1]);
                            ParseHAVCValues(parsedData[2]);
                            ParseHAVCValues(parsedData[3]);
                            ParseHAVCValues(parsedData[4]);
                            ParseHAVCValues(parsedData[5]);
                            //havc1Text.text = ExtractSingleValue(parsedData[2]);
                            //havc2Text.text = ExtractSingleValue(parsedData[3]);
                            //havc3Text.text = ExtractSingleValue(parsedData[4]);
                            //havc4Text.text = ExtractSingleValue(parsedData[5]);

                            ParseSensorValues(parsedData[6]);
                            ParseSensorValues(parsedData[7]);
                            ParseSensorValues(parsedData[8]);
                            //sensor2Text.text = ExtractSingleValue(parsedData[7]);
                            //sensor3Text.text = ExtractSingleValue(parsedData[8]);
                            ParseVentilatorValues(parsedData[9]);
                            ParseVentilatorValues(parsedData[10]);
                            ParseVentilatorValues(parsedData[11]);
                            //ventil2Text.text = ExtractSingleValue(parsedData[10]);
                            //ventil3Text.text = ExtractSingleValue(parsedData[11]);
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

                
            }
            catch (Exception e)
            {
                Debug.LogError("�����͸� �д� �� ���� �߻�: " + e.Message);
            }
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

                else if (key.Equals("s2_Temp"))
                {
                    sensor2TempText.text = value;
                }

                else if (key.Equals("s2_Humidity"))
                {
                    sensor2HumidityText.text = value;
                }
                else if (key.Equals("s2_Co2"))
                {
                    sensor2Co2Text.text = value;
                }

                else if (key.Equals("s3_Temp"))
                {
                    sensor3TempText.text = value;
                }

                else if (key.Equals("s3_Humidity"))
                {
                    sensor3HumidityText.text = value;
                }
                else if (key.Equals("s3_Co2"))
                {
                    sensor3Co2Text.text = value;
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

                else if (key.Equals("v2_Wind"))
                {
                    ventil2WindText.text = value;
                }
                else if (key.Equals("v2_Power"))
                {
                    ventil2PowerText.text = value;
                }

                else if (key.Equals("v3_Wind"))
                {
                    ventil3WindText.text = value;
                }
                else if (key.Equals("v3_Power"))
                {
                    ventil3PowerText.text = value;
                }
            }
        }
    }

    private void ParseHAVCValues(string data)
    {
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
                if (key.Equals("h1_Set_Temp"))
                {
                    havcSetTempText.text = value;
                }
                else if (key.Equals("h1_Power"))
                {
                    havcPowerText.text = value;
                }

                else if (key.Equals("h2_Set_Temp"))
                {
                    havc2SetTempText.text = value;
                }
                else if (key.Equals("h2_Power"))
                {
                    havc2PowerText.text = value;
                }

                else if (key.Equals("h3_Set_Temp"))
                {
                    havc3SetTempText.text = value;
                }
                else if (key.Equals("h3_Power"))
                {
                    havc3PowerText.text = value;
                }

                else if (key.Equals("h4_Set_Temp"))
                {
                    havc4SetTempText.text = value;
                }
                else if (key.Equals("h4_Power"))
                {
                    havc4PowerText.text = value;
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

