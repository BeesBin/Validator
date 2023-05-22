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
        // 이 객체에서 처리하는 URI 접두사를 가져온다.
        listener.Start();
        // 이 객체가 들어오는 요청을 받을 수 있도록 한다.

        while (true)
        {
            var context = listener.GetContext();
            // 들어오는 요청을 기다리고, 요청을 받으면 반환된다.
            Console.WriteLine("Request : " + context.Request.Url);
            byte[] data = Encoding.UTF8.GetBytes("HelloWorld");
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }
    }
}
