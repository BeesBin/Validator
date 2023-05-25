public string GetSequenceNumber(int number)
{
    string sequenceNumber = number.ToString().PadLeft(3, '0');
    return sequenceNumber;
}

public string GetSequenceNumber(int number)
{
    string sequenceNumber = number.ToString("D3");
    return sequenceNumber;
}

string input = "015";

int number = int.Parse(input);

string result = number.ToString("D3");

Console.WriteLine(result);





using System;

public class Program
{
    public static void Main()
    {
        string input = "http://127.0.0.1:5002/auth/lgcns?id=apple&key=DFGE";

        // 문자열 분리
        string[] parts = input.Split(new[] { ':', '/', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // 포트 추출
        string port = parts[3];

        // 인증(auth), ID, 키(key) 추출
        string auth = "";
        string id = "";
        string key = "";
        foreach (string part in parts)
        {
            if (part.StartsWith("auth"))
            {
                auth = part.Substring(5);
            }
            else if (part.StartsWith("id"))
            {
                id = part.Substring(3);
            }
            else if (part.StartsWith("key"))
            {
                key = part.Substring(4);
            }
        }

        Console.WriteLine("Port: " + port);
        Console.WriteLine("Auth: " + auth);
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Key: " + key);
    }
}

/*
using System;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        string input = "http://127.0.0.1:5002/auth/lgcns?id=apple&key=DFGE";

        // 정규식 패턴을 사용하여 파싱
        string pattern = @"http://\d+\.\d+\.\d+\.\d+:(\d+)/auth/lgcns\?id=(\w+)&key=(\w+)";
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            string port = match.Groups[1].Value;
            string auth = match.Groups[2].Value;
            string id = match.Groups[3].Value;
            string key = match.Groups[4].Value;

            Console.WriteLine("Port: " + port);
            Console.WriteLine("Auth: " + auth);
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Key: " + key);
        }
    }
}
*/