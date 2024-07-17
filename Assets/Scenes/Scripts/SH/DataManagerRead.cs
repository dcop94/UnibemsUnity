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


    // UI Text 컴포넌트
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
        //// DataManager 인스턴스 찾기 : DataManager 인스턴스를 찾아서 네트워크 스트림에 접근하기 위함
        dataManager = FindObjectOfType<DataManager>();


        if (dataManager != null)
        {      
            // DataManager에서 네트워크 스트림 가져오기
            networkStream = dataManager.GetNetworkStream();
            Debug.Log("get networkstream");

            if (networkStream != null)
            {
                Debug.Log("NetworkStream retrieved successfully.");
                // 서버에서 데이터를 읽기 시작
                // 2초 후에 StartReceiving 메서드를 호출하고, 그 이후로 매 1초마다 호출
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



    // 서버에서 데이터를 읽어옴 
    private void ReceiveData()
    {
        Debug.Log("ReceiveData");
        //  networkStream 유효하고 읽기 가능한 상황
        if (/*networkStream != null &&*/ networkStream.CanRead)
        {
            try
            {
                // 서버로 부터 수신할 데이터 준비
                byte[] bytes = new byte[1024]; // 1024 : Max buffer size

                Debug.Log("전");
                // Read() - 서버로 부터 데이터 수신
                int bytesRead = networkStream.Read(bytes, 0, bytes.Length);
                Debug.Log("후");

                if (bytesRead > 0)
                {
                    // C#에서는 String 타입으로 텍스트 데이터 표현
                    string serverToClientData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    //Debug.Log("수신된 데이터: " + serverToClientData);


                    // 데이터를 파싱
                    string[] parsedData = serverToClientData.Split(';');

                    Debug.Log("parsedData[0]: " + parsedData[0]);


                    // Text 컴포넌트가 null인지 확인
                    //CheckTextComponents();

                    /* 확인용
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
                    // 숫자 값만 추출하여 UI에 표시
                    if (parsedData.Length >= 14)
                    {
                        Debug.Log("파싱 시작");
                        // 평균 센서 데이터를 파싱하여 UI에 설정
                        ParseAverageSensorData(parsedData[0]);

                        // 다른 값들을 파싱하여 UI에 설정
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
                        Debug.Log("파싱 끝");
                    }

                    else
                    {
                        Debug.Log("수신된 데이터 형식 오류");
                    }


                }
            }
            catch (Exception e)
            {
                Debug.LogError("데이터를 읽는 중 오류 발생: " + e.Message);
            }
        }
        // 삭제예정
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
        // 센서 데이터를 ','로 분리하여 개별 키-값 쌍으로 나눕니다.
        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            // 각 키-값 쌍을 ':'로 분리하여 키와 값을 나눕니다.
            string[] keyValue = part.Split(':');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // 키에 따라 해당 Text 컴포넌트에 값을 설정합니다.
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
        // 통풍기 데이터를 ','로 분리하여 개별 키-값 쌍으로 나눕니다.
        string[] parts = data.Split(',');

        foreach (string part in parts)
        {
            // 각 키-값 쌍을 ':'로 분리하여 키와 값을 나눕니다.
            string[] keyValue = part.Split(':');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // 키에 따라 해당 Text 컴포넌트에 값을 설정합니다.
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
        // 데이터를 ':'로 분리하여 키와 값을 나눕니다.
        string[] keyValue = data.Split(':');
        // 형식이 올바르면 값을 반환하고, 그렇지 않으면 "0"을 반환합니다.
        return keyValue.Length == 2 ? keyValue[1].Trim() : "0";
    }

    void OnDestroy()
    {
        // 오브젝트가 파괴될 때 반복 호출을 취소
        CancelInvoke("ReceiveData");
    }


}

