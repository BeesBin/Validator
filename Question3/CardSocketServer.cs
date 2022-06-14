using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Question3
{
    class CardSocketServer
    {
        // 소켓서버가 실제로 하는 일의 처리 로직을 담음
        public Socket listener;

        public void DoSocketWork()
        {
            byte[] buffer = new byte[4096];

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEP = new IPEndPoint(ipAddress, 27015);

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
            try
            {
                listener.Bind(localEP);
                listener.Listen(10);

                while (true)
                {
                    Socket handler = listener.Accept();

                    NetworkStream ns = new NetworkStream(handler);
                    BinaryReader br = new BinaryReader(ns);

                    string filename;
                    while((filename = br.ReadString()) != null)
                    {
                        int length = (int)br.ReadInt64();

                        while(length > 0)
                        {
                            int nReadLen = br.Read(buffer, 0, Math.Min(4096, length));
                            SaveFile(filename, buffer, nReadLen);
                            length -= nReadLen;
                        }
                    }
                }
            }catch(Exception e)
            {

            }
        }

        private void SaveFile(string fname, byte[] buf, int len)
        {
            string path = "..\\SERVER\\" + fname;
            FileStream fs = new FileStream(path, FileMode.Append);
            fs.Write(buf, 0, len);
            fs.Close();
        }
    }
}
