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
        // 서버의 IP 주소와 포트 번호 설정
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 9090;

        // 서버 소켓 생성
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 서버 소켓을 IP 주소와 포트 번호에 바인딩
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
        listener.Bind(localEndPoint);

        // 서버 소켓이 들어오는 연결을 수신할 준비
        listener.Listen(10);

        Console.WriteLine("서버가 시작되었습니다. 클라이언트 연결을 대기 중입니다...");

        while (true)
        {
            // 클라이언트 연결을 비동기적으로 수신
            listenSignal.Reset();
            listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            listenSignal.WaitOne();
        }
    }

    private static void AcceptCallback(IAsyncResult ar)
    {
        // 클라이언트 연결을 수락하고 소켓 생성
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // 다음 연결 요청을 처리하기 위해 대기 상태로 설정
        listenSignal.Set();

        // 받은 데이터를 저장할 버퍼
        byte[] buffer = new byte[1024];

        // 클라이언트와의 데이터 통신을 비동기적으로 처리
        handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), new StateObject(handler, buffer));
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        // StateObject에서 데이터를 추출하고 소켓과 버퍼를 가져옴
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.Socket;
        byte[] buffer = state.Buffer;

        try
        {
            // 클라이언트로부터 받은 데이터를 읽음
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("클라이언트로부터 데이터를 수신했습니다: " + receivedData);

                // 받은 데이터 처리 로직


                // 클라이언트에게 응답을 보냄
                byte[] response = Encoding.ASCII.GetBytes("서버에서 클라이언트로 응답을 보냅니다.");
                handler.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallback), handler);

                // 다음 데이터를 비동기적으로 수신하기 위해 대기 상태로 설정
                handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                // 클라이언트 연결이 닫혔을 경우
                handler.Close();
            }
        }
        catch (SocketException)
        {
            // 클라이언트와의 연결이 끊겼을 경우
            handler.Close();
        }
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // 데이터를 보내고 완료
            Socket handler = (Socket)ar.AsyncState;
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("클라이언트로 데이터를 전송했습니다.");
        }
        catch (SocketException)
        {
            // 클라이언트와의 연결이 끊겼을 경우
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
