using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TCP_Server : MonoBehaviour
{
    [SerializeField]
    string ipAddress;
    [SerializeField]
    int port;

    TcpListener server;
    bool isRunning;
    TcpClient connectedClient;
    NetworkStream stream;

    private void Awake()
    {
        if (null == ipAddress) Debug.LogError("IP �ּҰ� �������� �ʾҽ��ϴ�.");
    }

    private void Start()
    {
        //StartServer();
    }

    public void StartServer()
    {
        server = new TcpListener(IPAddress.Parse(ipAddress), port);
        server.Start();
        isRunning = true;
        Debug.Log($"TCP ������ ��Ʈ {port}���� ���� ��...");

        // �񵿱������� Ŭ���� ���� ����
        server.BeginAcceptTcpClient(OnClientConnected, null);
    }

    private void OnClientConnected(IAsyncResult result)
    {
        connectedClient = server.EndAcceptTcpClient(result);
        Debug.Log("Ŭ���̾�Ʈ �����!");

        stream = connectedClient.GetStream();

        server.BeginAcceptTcpClient(OnClientConnected, null);
    }

    public void SendData(Vector2 position)
    {
        if (null == connectedClient || null == stream)
        {
            Debug.LogError("Ŭ���̾�Ʈ�� ������� �ʾҽ��ϴ�.");
            return;
        }

        byte[] data = new byte[8];
        BitConverter.GetBytes(position.x).CopyTo(data, 0);
        BitConverter.GetBytes(position.y).CopyTo(data, 4);

        stream.Write(data, 0, data.Length);
        Debug.Log($"������ ���۵� : {position}");
    }

    private void OnDestroy()
    {
        if(isRunning)
        {
            if (null != stream) stream.Close();
            if (null != connectedClient) connectedClient.Close();
            server.Stop();
            Debug.Log("TCP ���� �����");
        }
    }

}
