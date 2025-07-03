using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class TCP_Receiver : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Server IP")]
    string serverIP;
    [SerializeField]
    [Tooltip("Port Number")]
    string port;
    [Tooltip("�����κ��� ���޹��� ��ǥ")]
    [ReadOnly][SerializeField] Vector2 clickPos = new Vector2();
    [Tooltip("�����κ��� ���޹��� ��ǥ�� ȭ�鿡 ������ִ� �ؽ�Ʈ ��ü")]
    [SerializeField] TMP_Text coordinateDisplay;    // optional

    TcpClient client;       //
    NetworkStream stream;   //
    bool isRunnig;          //
    bool isDebug;

    private void Awake()
    {
        if (null == serverIP) Debug.LogError("Server IP not set.");
        if (null == port) Debug.LogError("Port not set.");
        isDebug = false;

        // optional
        if (null == coordinateDisplay) Debug.Log("��ǥ ���÷��� ��ü�� �������� �ʾҽ��ϴ�.");
    }

    void Start()
    {
        //StartClient();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(false == isDebug)
            {
                coordinateDisplay.color = new Color(1, 1, 1, 1);
                isDebug = true;
            }
            else
            {
                coordinateDisplay.color = new Color(1, 1, 1, 0);
                isDebug = false;
            }
        }
    }

    public void StartClient()
    {
        try
        {
            Debug.Log("TCP ���� ���� �õ�...");
            // TCP ��ü ����
            client = new TcpClient(serverIP, int.Parse(port));
            // ��Ʈ��ũ ��Ʈ�� ��������
            stream = client?.GetStream();
            isRunnig = true;
            Debug.Log("TCP ���� ���� �Ϸ�");

            // �ڷ�ƾ ���� (Vector2 ������ ����)
            StartCoroutine(ReceiveData());
        }
        catch (Exception e)
        {
            Debug.LogError($"Error : {e.Message}");
        }
    }

    private IEnumerator ReceiveData()
    {
        byte[] buffer = new byte[1024]; // 1KB

        while (isRunnig)
        {
            // ��Ʈ������ ���� �� �ִ� �����Ͱ� �����ϴ��� Ȯ��
            if (stream.DataAvailable)
            {
                int readSize = stream.Read(buffer);

                // ����Ʈ �迭 float ���� ��ȯ
                float x = BitConverter.ToSingle(buffer, 0);
                float y = BitConverter.ToSingle(buffer, 4);
                // ��ǥ ��ȯ
                // ������ ���� ��ġ : �»��
                // unity ���� ��ġ : ���ϴ�
                clickPos = new Vector2(x, 1080 - y);

                // Debug
                Debug.Log($"Receive Data - X : {clickPos.x}, Y : {clickPos.y}");
                DisplayCoordinate(clickPos);

                // Ŭ�� �̺�Ʈ �߻�
                ClickEventGen.ClickAt(clickPos);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnAppplicationQuit()
    {
        // 1. �ڷ�ƾ ����
        isRunnig = false;
        // 2. ��Ʈ�� �ݱ�
        if (null != stream) stream.Close();
        // 3. Ŭ���̾�Ʈ �ݱ�
        if (null != client) client.Close();
    }

    private void DisplayCoordinate(Vector2 pos)
    {
        if (null == coordinateDisplay) return;
        coordinateDisplay.text = $"X : {pos.x} Y : {pos.y} (Unity ver)";
    }
}
