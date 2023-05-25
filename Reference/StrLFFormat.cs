using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        string dataFormat = "{0}\n"; // 데이터 포맷
        string resultStr = "Hello, World!"; // 송신할 데이터

        try
        {
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버에 연결
            sender.Connect("서버 IP 주소", 9090);

            // 데이터 포맷에 LF를 붙여서 데이터 생성
            string data = string.Format(dataFormat, resultStr);

            // 데이터 전송
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            sender.Send(byteData);

            // 소켓 연결 종료
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            Console.WriteLine("데이터 전송 완료.");
        }
        catch (Exception e)
        {
            Console.WriteLine("오류: " + e.ToString());
        }
    }
}

// string Join
string formattedEntry = string.Format("{0}#{1},{2}", timestampString, busId, busValue.ToString("D5"));
string.Join(",", formattedBusData);