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

            // ����ڷκ��� �Է� �ޱ�
            Console.WriteLine("Enter request type (GET or POST):");
            string requestType = Console.ReadLine().ToUpper();

            // ��û ���� ������ ����
            JObject requestData = new JObject();
            requestData["key"] = "value";

            // �����͸� StringContent�� ��ȯ
            StringContent content = new StringContent(requestData.ToString(), Encoding.UTF8, "application/json");

            // �б��Ͽ� GET �Ǵ� POST ��û ������
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

            // ���� ó��
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
