using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static ManualResetEvent listenSignal = new ManualResetEvent(false);

    static async Task Main()
    {
        // ������ IP �ּҿ� ��Ʈ ��ȣ ����
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 9090;

        // ���� ���� ����
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // ���� ������ IP �ּҿ� ��Ʈ ��ȣ�� ���ε�
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
        listener.Bind(localEndPoint);

        // ���� ������ ������ ������ ������ �غ�
        listener.Listen(10);

        Console.WriteLine("������ ���۵Ǿ����ϴ�. Ŭ���̾�Ʈ ������ ��� ���Դϴ�...");

        while (true)
        {
            // Ŭ���̾�Ʈ ������ �񵿱������� ����
            listenSignal.Reset();
            listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            listenSignal.WaitOne();
        }
    }

    private static void AcceptCallback(IAsyncResult ar)
    {
        // Ŭ���̾�Ʈ ������ �����ϰ� ���� ����
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // ���� ���� ��û�� ó���ϱ� ���� ��� ���·� ����
        listenSignal.Set();

        // ���� �����͸� ������ ����
        byte[] buffer = new byte[1024];

        // Ŭ���̾�Ʈ���� ������ ����� �񵿱������� ó��
        handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), new StateObject(handler, buffer));
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        // StateObject���� �����͸� �����ϰ� ���ϰ� ���۸� ������
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.Socket;
        byte[] buffer = state.Buffer;

        try
        {
            // Ŭ���̾�Ʈ�κ��� ���� �����͸� ����
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Ŭ���̾�Ʈ�κ��� �����͸� �����߽��ϴ�: " + receivedData);

                // ���� ������ ó�� ����


                // Ŭ���̾�Ʈ���� ������ ����
                byte[] response = Encoding.ASCII.GetBytes("�������� Ŭ���̾�Ʈ�� ������ �����ϴ�.");
                handler.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallback), handler);

                // ���� �����͸� �񵿱������� �����ϱ� ���� ��� ���·� ����
                handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                // Ŭ���̾�Ʈ ������ ������ ���
                handler.Close();
            }
        }
        catch (SocketException)
        {
            // Ŭ���̾�Ʈ���� ������ ������ ���
            handler.Close();
        }
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // �����͸� ������ �Ϸ�
            Socket handler = (Socket)ar.AsyncState;
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Ŭ���̾�Ʈ�� �����͸� �����߽��ϴ�.");
        }
        catch (SocketException)
        {
            // Ŭ���̾�Ʈ���� ������ ������ ���
            Socket handler = (Socket)ar.AsyncState;
            handler.Close();
        }
    }

    private class StateObject
    {
        public Socket Socket { get; }
        public byte[] Buffer { get; }

        public StateObject(Socket socket, byte[] buffer)
        {
            Socket = socket;
            Buffer = buffer;
        }
    }
}
