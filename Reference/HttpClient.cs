using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

class HttpClientMain
{
    static void Main(string[] args)
    {
        HttpClient client = new HttpClient();
        var res = client.GetAsync("http://127.0.0.1:8080/helloworld").Result;
        Console.WriteLine("Response : " + res.StatusCode + " - " + res.Content.ReadAsStringAsync().Result);
    }
}

using Microsoft.AspNetCore.Http;

public async Task HandleRequest(HttpContext context)
{
    // HTTP 요청의 헤더 값 가져오기
    var headers = context.Request.Headers;

    // 특정 헤더 값 읽기
    string userAgent = headers["User-Agent"];

    // 헤더 값 출력
    Console.WriteLine($"User-Agent: {userAgent}");

    // 응답 작성
    await context.Response.WriteAsync("HTTP 요청의 헤더 값을 읽었습니다.");
}
