using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// JSON 응답을 처리할 클래스 정의
public class QueueInfoResponse
{
    public int inputQueueCount { get; set; }
    public List<string> inputQueueURIs { get; set; }
    public string outputQueueURI { get; set; }
}

class Program
{
    static async Task Main()
    {
        // HttpClient 인스턴스 생성
        using (HttpClient client = new HttpClient())
        {
            while (true)
            {
                try
                {
                    // URI로 GET 요청 보내기
                    string uri = "http://127.0.0.1:8080/queueInfo";
                    HttpResponseMessage response = await client.GetAsync(uri);

                    // 응답 확인
                    if (response.IsSuccessStatusCode)
                    {
                        // 응답 본문 가져오기
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // JSON 파싱
                        QueueInfoResponse queueInfo = JsonConvert.DeserializeObject<QueueInfoResponse>(jsonResponse);

                        // 데이터 추출
                        int inputQueueCount = queueInfo.inputQueueCount;
                        List<string> inputQueueURIs = queueInfo.inputQueueURIs;
                        string outputQueueURI = queueInfo.outputQueueURI;

                        // 추출한 데이터 사용
                        Console.WriteLine("Input Queue Count: " + inputQueueCount);
                        Console.WriteLine("Input Queue URIs:");
                        foreach (string inputQueueURI in inputQueueURIs)
                        {
                            Console.WriteLine(inputQueueURI);
                        }
                        Console.WriteLine("Output Queue URI: " + outputQueueURI);

                        // Worker 인스턴스 생성 및 실행
                        List<Task> workerTasks = new List<Task>();
                        foreach (string inputQueueUri in inputQueueURIs)
                        {
                            Worker worker = new Worker(client, inputQueueUri, outputQueueURI);
                            workerTasks.Add(worker.ProcessAsync());
                        }

                        // 모든 Worker 작업이 완료될 때까지 대기
                        await Task.WhenAll(workerTasks);
                    }
                    else
                    {
                        // 요청이 실패한 경우
                        Console.WriteLine("HTTP 요청 실패. 상태 코드: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // 예외 처리
                    Console.WriteLine("오류 발생: " + ex.Message);
                }
            }
        }
    }
}

// Worker 클래스 정의
class Worker
{
    private readonly HttpClient _client;
    private readonly string _inputQueueUri;
    private readonly string _outputQueueUri;

    public Worker(HttpClient client, string inputQueueUri, string outputQueueUri)
    {
        _client = client;
        _inputQueueUri = inputQueueUri;
        _outputQueueUri = outputQueueUri;
    }

    public async Task ProcessAsync()
    {
        try
        {
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(_inputQueueUri);

            // 응답 확인
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                // 응답 데이터 처리 로직 작성
                await ProcessResponseAsync(responseContent);
            }
            else
            {
                Console.WriteLine("GET 요청 실패. 상태 코드: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("오류 발생: " + ex.Message);
        }
    }

    private async Task ProcessResponseAsync(string responseContent)
    {
        // JSON 응답 파싱
        ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(responseContent);

        // 응답 데이터 사용
        Console.WriteLine("Received response from " + _inputQueueUri + ":");
        Console.WriteLine("Timestamp: " + responseData.timestamp);
        Console.WriteLine("Value: " + responseData.value);
        Console.WriteLine();

        // POST 요청 보내기
        await SendPostRequestAsync(responseContent);
    }

    private async Task SendPostRequestAsync(string data)
    {
        try
        {
            // 데이터 준비
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            // POST 요청 보내기
            HttpResponseMessage response = await _client.PostAsync(_outputQueueUri, content);

            // 응답 확인
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST 요청 성공. URI: " + _outputQueueUri);
            }
            else
            {
                Console.WriteLine("POST 요청 실패. 상태 코드: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("오류 발생: " + ex.Message);
        }
    }
}

// 응답 데이터를 처리할 클래스 정의
public class ResponseData
{
    public int timestamp { get; set; }
    public string value { get; set; }
}
