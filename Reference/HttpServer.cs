using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

class HttpServerMain
{
    static void Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://127.0.0.1:8080/");
        // �� ��ü���� ó���ϴ� URI ���λ縦 �����´�.
        listener.Start();
        // �� ��ü�� ������ ��û�� ���� �� �ֵ��� �Ѵ�.

        while (true)
        {
            var context = listener.GetContext();
            // ������ ��û�� ��ٸ���, ��û�� ������ ��ȯ�ȴ�.
            Console.WriteLine("Request : " + context.Request.Url);
            byte[] data = Encoding.UTF8.GetBytes("HelloWorld");
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }
    }
}
