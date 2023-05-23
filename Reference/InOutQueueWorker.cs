using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// JSON ������ ó���� Ŭ���� ����
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
        // HttpClient �ν��Ͻ� ����
        using (HttpClient client = new HttpClient())
        {
            while (true)
            {
                try
                {
                    // URI�� GET ��û ������
                    string uri = "http://127.0.0.1:8080/queueInfo";
                    HttpResponseMessage response = await client.GetAsync(uri);

                    // ���� Ȯ��
                    if (response.IsSuccessStatusCode)
                    {
                        // ���� ���� ��������
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // JSON �Ľ�
                        QueueInfoResponse queueInfo = JsonConvert.DeserializeObject<QueueInfoResponse>(jsonResponse);

                        // ������ ����
                        int inputQueueCount = queueInfo.inputQueueCount;
                        List<string> inputQueueURIs = queueInfo.inputQueueURIs;
                        string outputQueueURI = queueInfo.outputQueueURI;

                        // ������ ������ ���
                        Console.WriteLine("Input Queue Count: " + inputQueueCount);
                        Console.WriteLine("Input Queue URIs:");
                        foreach (string inputQueueURI in inputQueueURIs)
                        {
                            Console.WriteLine(inputQueueURI);
                        }
                        Console.WriteLine("Output Queue URI: " + outputQueueURI);

                        // Worker �ν��Ͻ� ���� �� ����
                        List<Task> workerTasks = new List<Task>();
                        foreach (string inputQueueUri in inputQueueURIs)
                        {
                            Worker worker = new Worker(client, inputQueueUri, outputQueueURI);
                            workerTasks.Add(worker.ProcessAsync());
                        }

                        // ��� Worker �۾��� �Ϸ�� ������ ���
                        await Task.WhenAll(workerTasks);
                    }
                    else
                    {
                        // ��û�� ������ ���
                        Console.WriteLine("HTTP ��û ����. ���� �ڵ�: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // ���� ó��
                    Console.WriteLine("���� �߻�: " + ex.Message);
                }
            }
        }
    }
}

// Worker Ŭ���� ����
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
            // GET ��û ������
            HttpResponseMessage response = await _client.GetAsync(_inputQueueUri);

            // ���� Ȯ��
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                // ���� ������ ó�� ���� �ۼ�
                await ProcessResponseAsync(responseContent);
            }
            else
            {
                Console.WriteLine("GET ��û ����. ���� �ڵ�: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("���� �߻�: " + ex.Message);
        }
    }

    private async Task ProcessResponseAsync(string responseContent)
    {
        // JSON ���� �Ľ�
        ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(responseContent);

        // ���� ������ ���
        Console.WriteLine("Received response from " + _inputQueueUri + ":");
        Console.WriteLine("Timestamp: " + responseData.timestamp);
        Console.WriteLine("Value: " + responseData.value);
        Console.WriteLine();

        // POST ��û ������
        await SendPostRequestAsync(responseContent);
    }

    private async Task SendPostRequestAsync(string data)
    {
        try
        {
            // ������ �غ�
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            // POST ��û ������
            HttpResponseMessage response = await _client.PostAsync(_outputQueueUri, content);

            // ���� Ȯ��
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST ��û ����. URI: " + _outputQueueUri);
            }
            else
            {
                Console.WriteLine("POST ��û ����. ���� �ڵ�: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("���� �߻�: " + ex.Message);
        }
    }
}

// ���� �����͸� ó���� Ŭ���� ����
public class ResponseData
{
    public int timestamp { get; set; }
    public string value { get; set; }
}
