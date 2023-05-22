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
        // 127.0.0.1:5002���� ���� ������ ��
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

        // URL ������ ���� ������ �Ľ�
        var dataObject = JObject.Parse(receivedData);
        int port = dataObject.Value<int>("port");
        JArray routes = dataObject.Value<JArray>("routes");

        // ã�� ���(prefix)
        string targetPathPrefix = "/auth";

        // �ش��ϴ� URL ã��
        string targetUrl = FindUrlByPathPrefix(routes, targetPathPrefix);

        // URL�� HTTP ��û ������
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

        return null; // �ش��ϴ� ���(prefix)�� ���� ��� null ��ȯ
    }

    private static async Task SendHttpRequest(string url)
    {
        // HTTP ��û ������
        HttpResponseMessage response = await client.GetAsync(url);

        // ���� ó��
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            // ���� ���� ó��
        }
        else
        {
            Console.WriteLine($"HTTP ��û ����: {response.StatusCode}");
        }
    }
}
