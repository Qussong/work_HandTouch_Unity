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
    [Tooltip("서버로부터 전달받은 좌표")]
    [ReadOnly][SerializeField] Vector2 clickPos = new Vector2();
    [Tooltip("서버로부터 전달받은 좌표값 화면에 출력해주는 텍스트 객체")]
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
        if (null == coordinateDisplay) Debug.Log("좌표 디스플레이 객체가 설정되지 않았습니다.");
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
            Debug.Log("TCP 서버 접속 시도...");
            // TCP 객체 생성
            client = new TcpClient(serverIP, int.Parse(port));
            // 네트워크 스트림 가져오기
            stream = client?.GetStream();
            isRunnig = true;
            Debug.Log("TCP 서버 접속 완료");

            // 코루틴 실행 (Vector2 데이터 받음)
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
            // 스트림에서 읽을 수 있는 데이터가 존재하는지 확인
            if (stream.DataAvailable)
            {
                int readSize = stream.Read(buffer);

                // 바이트 배열 float 으로 변환
                float x = BitConverter.ToSingle(buffer, 0);
                float y = BitConverter.ToSingle(buffer, 4);
                // 좌표 변환
                // 전송츨 영점 위치 : 좌상단
                // unity 영점 위치 : 좌하단
                clickPos = new Vector2(x, 1080 - y);

                // Debug
                Debug.Log($"Receive Data - X : {clickPos.x}, Y : {clickPos.y}");
                DisplayCoordinate(clickPos);

                // 클릭 이벤트 발생
                ClickEventGen.ClickAt(clickPos);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnAppplicationQuit()
    {
        // 1. 코루틴 정지
        isRunnig = false;
        // 2. 스트림 닫기
        if (null != stream) stream.Close();
        // 3. 클라이언트 닫기
        if (null != client) client.Close();
    }

    private void DisplayCoordinate(Vector2 pos)
    {
        if (null == coordinateDisplay) return;
        coordinateDisplay.text = $"X : {pos.x} Y : {pos.y} (Unity ver)";
    }
}
