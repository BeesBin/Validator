using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        string dataFormat = "{0}\n"; // ������ ����
        string resultStr = "Hello, World!"; // �۽��� ������

        try
        {
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // ������ ����
            sender.Connect("���� IP �ּ�", 9090);

            // ������ ���˿� LF�� �ٿ��� ������ ����
            string data = string.Format(dataFormat, resultStr);

            // ������ ����
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            sender.Send(byteData);

            // ���� ���� ����
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            Console.WriteLine("������ ���� �Ϸ�.");
        }
        catch (Exception e)
        {
            Console.WriteLine("����: " + e.ToString());
        }
    }
}

// string Join
string formattedEntry = string.Format("{0}#{1},{2}", timestampString, busId, busValue.ToString("D5"));
string.Join(",", formattedBusData);