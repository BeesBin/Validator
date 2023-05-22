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
 * JSON을 역직렬화할 때 변수명과 JSON의 키 값이 일치하는 것이 가장 간단하고 자동적인 방법
 * [JsonProperty] 속성을 사용하여 변수명과 JSON의 키 값을 직접 연결
public class Configuration
{
    [JsonProperty("port")]
    public int PortNumber { get; set; }

    [JsonProperty("routes")]
    public List<Route> RouteList { get; set; }
}
*/
/*
 * JsonConvert 클래스는 JSON 데이터를 객체로 변환하거나 객체를 JSON 문자열로 직렬화
 * DeserializeObject 메서드
 * JsonConvert.DeserializeObject 메서드는 JSON 문자열을 지정한 유형의 객체로 역직렬화
 * Ex) JsonConvert.DeserializeObject<Configuration>(json)
 * 
 * SerializeObject 메서드
 * JsonConvert.SerializeObject 메서드는 객체를 JSON 형식의 문자열로 직렬화
 * Ex) JsonConvert.SerializeObject(obj)
 * */


