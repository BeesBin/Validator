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

            // 사용자로부터 입력 받기
            Console.WriteLine("Enter request type (GET or POST):");
            string requestType = Console.ReadLine().ToUpper();

            // 요청 본문 데이터 생성
            JObject requestData = new JObject();
            requestData["key"] = "value";

            // 데이터를 StringContent로 변환
            StringContent content = new StringContent(requestData.ToString(), Encoding.UTF8, "application/json");

            // 분기하여 GET 또는 POST 요청 보내기
            HttpResponseMessage response;
            if (requestType == "GET")
            {
                response = await httpClient.GetAsync(url);
            }
            else if (requestType == "POST")
            {
                response = await httpClient.PostAsync(url, content);
            }
            else
            {
                Console.WriteLine("Invalid request type. Exiting the program.");
                return;
            }

            // 응답 처리
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request succeeded:");
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Request failed: " + response.StatusCode);
            }
        }
    }
}
