using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliker : MonoBehaviour
{
    [SerializeField]
    Vector2 vec = new Vector2();
    [SerializeField]
    TCP_Server server;
    [SerializeField]
    TCP_Receiver client;

    private void Awake()
    {
        if (null == server) Debug.LogError("TCP Server �� �������� �ʾҽ��ϴ�.");
        if (null == client) Debug.LogError("TCP Client �� �������� �ʾҽ��ϴ�.");
    }

    private void Start()
    {
        StartCoroutine(StartTCP());
    }

    private IEnumerator StartTCP()
    {
        server.StartServer();

        yield return new WaitForSeconds(2f);

        client.StartClient();
    }

    void Update()
    {
        // TCP Server -> TCP Client ��ǥ ������ ����
        SendPosToClient();
    }

    private void SendPosToClient()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            server.SendData(vec);
        }
    }

    public void ClickOutput()
    {
        Debug.Log("Hello");
    }
}
