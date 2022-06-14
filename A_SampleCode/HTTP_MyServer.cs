using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_SERVER
{
    class MyServer
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8080/");
            listener.Start();

            while(true)
            {
                var context = listener.GetContext();
                Console.WriteLine(string.Format("Request({0}) : {1}", context.Request.HttpMethod, context.Request.Url));
                DateTime dt = DateTime.Now;
                String strRes = "";
                if (context.Request.HttpMethod == "GET")
                {
                    strRes = dt.ToString();
                }
                else
                {

                }
                
                byte[] data = Encoding.UTF8.GetBytes(strRes);
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.StatusCode = 200;
                context.Response.Close();
            }
        }
    }
}
