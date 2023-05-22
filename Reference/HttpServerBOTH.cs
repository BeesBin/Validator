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
        // 서버의 URL 설정
        string serverUrl = "http://localhost:8080/";

        // HttpListener 객체 생성 및 Prefix 등록
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(serverUrl);

        try
        {
            // HttpListener 시작
            listener.Start();
            Console.WriteLine("HTTP server started. Listening on " + serverUrl);

            // 요청 대기
            while (true)
            {
                // 요청 수락
                HttpListenerContext context = await listener.GetContextAsync();

                // 요청 처리를 위한 Task 실행
                Task.Run(() => ProcessRequest(context));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }
        finally
        {
            // HttpListener 종료
            listener.Stop();
        }
    }

    static void ProcessRequest(HttpListenerContext context)
    {
        // 요청 객체
        HttpListenerRequest request = context.Request;

        // 응답 객체
        HttpListenerResponse response = context.Response;

        // 요청 메서드와 URL 확인
        string method = request.HttpMethod;
        string url = request.Url.ToString();

        Console.WriteLine("Received request: " + method + " " + url);

        // 응답 데이터 설정
        string responseBody = "";
        byte[] buffer;

        if (method == "GET")
        {
            responseBody = "Hello, Gets!";
            buffer = Encoding.UTF8.GetBytes(responseBody);
        }
        else if (method == "POST")
        {
            // 요청 데이터에서 "ReportName" 추출
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

        // 응답 헤더 설정
        response.StatusCode = 200;
        response.ContentType = "text/plain";
        response.ContentLength64 = buffer.Length;

        // 응답 데이터 전송
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}
