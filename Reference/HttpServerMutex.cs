using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

class HttpServer
{
    private static Dictionary<string, string> reportList = new Dictionary<string, string>();
    private static Mutex mutex = new Mutex();

    static void Main(string[] args)
    {
        string url = "http://localhost:8080/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.Start();

        Console.WriteLine("Listening for requests on {0}", url);

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            Task.Run(() => ProcessRequest(context));
        }
    }

    static void ProcessRequest(HttpListenerContext context)
    {
        string responseBody = string.Empty;

        if (context.Request.HttpMethod == "GET")
        {
            string reportName = context.Request.QueryString["reportName"];

            mutex.WaitOne();

            if (reportList.ContainsKey(reportName))
            {
                string contents = reportList[reportName];
                responseBody = contents;
            }
            else
            {
                responseBody = "Hello, Gets!";
            }

            mutex.ReleaseMutex();
        }
        else if (context.Request.HttpMethod == "POST")
        {
            string reportName = context.Request.QueryString["reportName"];

            mutex.WaitOne();

            string requestData = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();
            reportList[reportName] = requestData;
            responseBody = "Good, POSTS!";

            mutex.ReleaseMutex();
        }
        else
        {
            responseBody = "Invalid request method";
        }

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseBody);

        context.Response.ContentLength64 = buffer.Length;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.OutputStream.Close();
    }
}
