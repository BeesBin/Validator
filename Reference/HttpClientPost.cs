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
            // ��û�� URL ����
            string url = "https://example.com/api/resource";

            // JObject ���� �� ������ �߰�
            JObject requestData = new JObject();
            requestData["key"] = "value";

            // JObject�� JSON ���ڿ��� ��ȯ
            string requestBody = requestData.ToString();

            // �����͸� StringContent�� ��ȯ
            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // POST ��û ������
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // ���� ó��
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST ��û ����:");
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("POST ��û ����: " + response.StatusCode);
            }
        }
    }
}
