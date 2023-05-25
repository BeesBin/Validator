static async Task Main()
{
    // ������ IP �ּҿ� ��Ʈ ��ȣ ����
    IPAddress serverIP = IPAddress.Parse("127.0.0.1");
    int serverPort = 9090;

    // ���ÿ� ������ Ŭ���̾�Ʈ ��
    int numClients = 5;

    // Ŭ���̾�Ʈ���� ���ķ� ����
    Task[] clientTasks = CreateClients(serverIP, serverPort, numClients);
    await Task.WhenAll(clientTasks);

    Console.WriteLine("��� Ŭ���̾�Ʈ�� �۾��� �Ϸ�Ǿ����ϴ�.");
}

static Task[] CreateClients(IPAddress serverIP, int serverPort, int numClients)
{
    Task[] clientTasks = new Task[numClients];
    for (int i = 0; i < numClients; i++)
    {
        clientTasks[i] = Task.Run(async () =>
        {
            try
            {
                // ���� ���� �� ������ ����
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    await clientSocket.ConnectAsync(serverIP, serverPort);
                    Console.WriteLine($"Ŭ���̾�Ʈ {i + 1}: ������ ���� �Ϸ�");

                    // ������ �۽� �� ���� (����)

                    // �������� ���� ����
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    Console.WriteLine($"Ŭ���̾�Ʈ {i + 1}: �������� ���� ����");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ŭ���̾�Ʈ {i + 1}���� ���� �߻�: {ex.Message}");
            }
        });
    }

    return clientTasks;
}
