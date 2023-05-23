using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PrjServer
{
    /*1. Client에서 Server에 접속하면 Server는 현재 날짜와시각을 Client로 전송하고, Client
는 전송 받은 값을 출력하시오.
2. Client에서 Server에 접속하여파일을 전송하는프로그램을작성하시오.
➢ ClientFiles 폴더의모든 파일을전송하여 ServerFiles폴더에 저장
- Client는 파일 전송 완료 후 종료
- Server는 파일을 수신 완료하고다시 Client 접속 대기
- Server는 ‘QUIT’입력을 받으면종료
*/

    class DateSocketServer
    {
        // Incoming data from the client.
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();

                    // Echo the data back to the client.
                    byte[] msg = Encoding.ASCII.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        static void Main(string[] args)
        {
            StartListening();
        }
    }
}
