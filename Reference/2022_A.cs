using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        // 127.0.0.1:5002에서 받은 데이터 값
        string receivedData = @"{
            ""port"": 5001,
            ""routes"": [
                {
                    ""pathPrefix"": ""/front"",
                    ""url"": ""http://127.0.0.1:8081""
                },
                {
                    ""pathPrefix"": ""/auth"",
                    ""url"": ""http://127.0.0.1:5002""
                }
            ]
        }";

        // URL 추출을 위한 데이터 파싱
        var dataObject = JObject.Parse(receivedData);
        int port = dataObject.Value<int>("port");
        JArray routes = dataObject.Value<JArray>("routes");

        // 찾을 경로(prefix)
        string targetPathPrefix = "/auth";

        // 해당하는 URL 찾기
        string targetUrl = FindUrlByPathPrefix(routes, targetPathPrefix);

        // URL로 HTTP 요청 보내기
        await SendHttpRequest(targetUrl);
    }

    private static string FindUrlByPathPrefix(JArray routes, string pathPrefix)
    {
        foreach (var route in routes)
        {
            string currentPathPrefix = route.Value<string>("pathPrefix");
            if (currentPathPrefix == pathPrefix)
            {
                string url = route.Value<string>("url");
                return url;
            }
        }

        return null; // 해당하는 경로(prefix)가 없을 경우 null 반환
    }

    private static async Task SendHttpRequest(string url)
    {
        // HTTP 요청 보내기
        HttpResponseMessage response = await client.GetAsync(url);

        // 응답 처리
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            // 응답 내용 처리
        }
        else
        {
            Console.WriteLine($"HTTP 요청 실패: {response.StatusCode}");
        }
    }
}
