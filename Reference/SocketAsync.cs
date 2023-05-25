using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        // 서버 IP 주소와 포트
        string serverIP = "127.0.0.1";
        int serverPort = 9090;

        // 서버에 전송할 데이터
        string message = "Hello, server!";

        // 비동기 연결 설정
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.BeginConnect(serverIP, serverPort, ConnectCallback, clientSocket);

        // 연결 완료 대기
        connectDone.WaitOne();

        // 데이터 전송
        byte[] data = Encoding.ASCII.GetBytes(message);
        clientSocket.BeginSend(data, 0, data.Length, 0, SendCallback, clientSocket);

        // 전송 완료 대기
        sendDone.WaitOne();

        // 서버 응답 수신
        byte[] buffer = new byte[1024];
        clientSocket.BeginReceive(buffer, 0, buffer.Length, 0, ReceiveCallback, clientSocket);

        // 응답 완료 대기
        receiveDone.WaitOne();

        // 연결 종료
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }

    static ManualResetEvent connectDone = new ManualResetEvent(false);
    static ManualResetEvent sendDone = new ManualResetEvent(false);
    static ManualResetEvent receiveDone = new ManualResetEvent(false);

    static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            clientSocket.EndConnect(ar);
            Console.WriteLine("서버에 연결되었습니다.");
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("연결 중 오류 발생: " + e.ToString());
        }
    }

    static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            int bytesSent = clientSocket.EndSend(ar);
            Console.WriteLine("데이터 전송 완료. 전송된 바이트 수: " + bytesSent);
            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("데이터 전송 중 오류 발생: " + e.ToString());
        }
    }

    static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            int bytesRead = clientSocket.EndReceive(ar);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("서버 응답: " + response);
            receiveDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("데이터 수신 중 오류 발생: " + e.ToString());
        }
    }
}
