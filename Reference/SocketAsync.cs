using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        // ���� IP �ּҿ� ��Ʈ
        string serverIP = "127.0.0.1";
        int serverPort = 9090;

        // ������ ������ ������
        string message = "Hello, server!";

        // �񵿱� ���� ����
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.BeginConnect(serverIP, serverPort, ConnectCallback, clientSocket);

        // ���� �Ϸ� ���
        connectDone.WaitOne();

        // ������ ����
        byte[] data = Encoding.ASCII.GetBytes(message);
        clientSocket.BeginSend(data, 0, data.Length, 0, SendCallback, clientSocket);

        // ���� �Ϸ� ���
        sendDone.WaitOne();

        // ���� ���� ����
        byte[] buffer = new byte[1024];
        clientSocket.BeginReceive(buffer, 0, buffer.Length, 0, ReceiveCallback, clientSocket);

        // ���� �Ϸ� ���
        receiveDone.WaitOne();

        // ���� ����
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
            Console.WriteLine("������ ����Ǿ����ϴ�.");
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("���� �� ���� �߻�: " + e.ToString());
        }
    }

    static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            int bytesSent = clientSocket.EndSend(ar);
            Console.WriteLine("������ ���� �Ϸ�. ���۵� ����Ʈ ��: " + bytesSent);
            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("������ ���� �� ���� �߻�: " + e.ToString());
        }
    }

    static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            int bytesRead = clientSocket.EndReceive(ar);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("���� ����: " + response);
            receiveDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine("������ ���� �� ���� �߻�: " + e.ToString());
        }
    }
}
