using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            // 요청할 URL 설정
            string url = "https://example.com/api/resource";

            // JObject 생성 및 데이터 추가
            JObject requestData = new JObject();
            requestData["key"] = "value";

            // JObject를 JSON 문자열로 변환
            string requestBody = requestData.ToString();

            // 데이터를 StringContent로 변환
            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // POST 요청 보내기
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // 응답 처리
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST 요청 성공:");
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("POST 요청 실패: " + response.StatusCode);
            }
        }
    }
}
