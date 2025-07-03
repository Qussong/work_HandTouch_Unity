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
        if (null == ipAddress) Debug.LogError("IP 주소가 설정되지 않았습니다.");
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
        Debug.Log($"TCP 서버가 포트 {port}에서 실행 중...");

        // 비동기적으로 클라의 연결 수락
        server.BeginAcceptTcpClient(OnClientConnected, null);
    }

    private void OnClientConnected(IAsyncResult result)
    {
        connectedClient = server.EndAcceptTcpClient(result);
        Debug.Log("클라이언트 연결됨!");

        stream = connectedClient.GetStream();

        server.BeginAcceptTcpClient(OnClientConnected, null);
    }

    public void SendData(Vector2 position)
    {
        if (null == connectedClient || null == stream)
        {
            Debug.LogError("클라이언트가 연결되지 않았습니다.");
            return;
        }

        byte[] data = new byte[8];
        BitConverter.GetBytes(position.x).CopyTo(data, 0);
        BitConverter.GetBytes(position.y).CopyTo(data, 4);

        stream.Write(data, 0, data.Length);
        Debug.Log($"데이터 전송됨 : {position}");
    }

    private void OnDestroy()
    {
        if(isRunning)
        {
            if (null != stream) stream.Close();
            if (null != connectedClient) connectedClient.Close();
            server.Stop();
            Debug.Log("TCP 서버 종료됨");
        }
    }

}
