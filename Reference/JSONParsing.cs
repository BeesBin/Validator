using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Route
{
    public string PathPrefix { get; set; }
    public string Url { get; set; }
}

public class Configuration
{
    public int Port { get; set; }
    public List<Route> Routes { get; set; }
}

public class Program
{
    public static void Main()
    {
        string json = @"{
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

        Configuration config = JsonConvert.DeserializeObject<Configuration>(json);

        int desiredPort = 5001;
        string desiredPathPrefix = "/front";

        foreach (var route in config.Routes)
        {
            if (config.Port == desiredPort && route.PathPrefix == desiredPathPrefix)
            {
                Console.WriteLine("Matching URL: " + route.Url);
                break;
            }
        }
    }
}

string jsonString = File.ReadAllText("config.json");
Configuration config = JsonConvert.DeserializeObject<Configuration>(jsonString);

string targetPathPrefix = "/front";
string targetUrl = config.Routes.FirstOrDefault(r => r.PathPrefix == targetPathPrefix)?.Url;

Console.WriteLine("Found URL: " + targetUrl);

/*
 * JSON�� ������ȭ�� �� ������� JSON�� Ű ���� ��ġ�ϴ� ���� ���� �����ϰ� �ڵ����� ���
 * [JsonProperty] �Ӽ��� ����Ͽ� ������� JSON�� Ű ���� ���� ����
public class Configuration
{
    [JsonProperty("port")]
    public int PortNumber { get; set; }

    [JsonProperty("routes")]
    public List<Route> RouteList { get; set; }
}
*/
/*
 * JsonConvert Ŭ������ JSON �����͸� ��ü�� ��ȯ�ϰų� ��ü�� JSON ���ڿ��� ����ȭ
 * DeserializeObject �޼���
 * JsonConvert.DeserializeObject �޼���� JSON ���ڿ��� ������ ������ ��ü�� ������ȭ
 * Ex) JsonConvert.DeserializeObject<Configuration>(json)
 * 
 * SerializeObject �޼���
 * JsonConvert.SerializeObject �޼���� ��ü�� JSON ������ ���ڿ��� ����ȭ
 * Ex) JsonConvert.SerializeObject(obj)
 * */


