using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    public static string reqReportName = "";

    static async Task Main(string[] args)
    {
        // ������ URL ����
        string serverUrl = "http://localhost:8080/";

        // HttpListener ��ü ���� �� Prefix ���
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(serverUrl);

        try
        {
            // HttpListener ����
            listener.Start();
            Console.WriteLine("HTTP server started. Listening on " + serverUrl);

            // ��û ���
            while (true)
            {
                // ��û ����
                HttpListenerContext context = await listener.GetContextAsync();

                // ��û ó���� ���� Task ����
                Task.Run(() => ProcessRequest(context));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }
        finally
        {
            // HttpListener ����
            listener.Stop();
        }
    }

    static void ProcessRequest(HttpListenerContext context)
    {
        // ��û ��ü
        HttpListenerRequest request = context.Request;

        // ���� ��ü
        HttpListenerResponse response = context.Response;

        // ��û �޼���� URL Ȯ��
        string method = request.HttpMethod;
        string url = request.Url.ToString();

        Console.WriteLine("Received request: " + method + " " + url);

        // ���� ������ ����
        string responseBody = "";
        byte[] buffer;

        if (method == "GET")
        {
            responseBody = "Hello, Gets!";
            buffer = Encoding.UTF8.GetBytes(responseBody);
        }
        else if (method == "POST")
        {
            // ��û �����Ϳ��� "ReportName" ����
            string requestData;
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                requestData = reader.ReadToEnd();
            }

            var json = JObject.Parse(requestData);
            reqReportName = json.Value<string>("ReportName") ?? "";

            responseBody = "Good, Posts!";
            buffer = Encoding.UTF8.GetBytes(responseBody);
        }
        else
        {
            response.StatusCode = 400;
            responseBody = "Invalid request method.";
            buffer = Encoding.UTF8.GetBytes(responseBody);
        }

        // ���� ��� ����
        response.StatusCode = 200;
        response.ContentType = "text/plain";
        response.ContentLength64 = buffer.Length;

        // ���� ������ ����
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}
