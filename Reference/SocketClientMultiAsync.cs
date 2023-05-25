static async Task Main()
{
    // 서버의 IP 주소와 포트 번호 설정
    IPAddress serverIP = IPAddress.Parse("127.0.0.1");
    int serverPort = 9090;

    // 동시에 접속할 클라이언트 수
    int numClients = 5;

    // 클라이언트들을 병렬로 실행
    Task[] clientTasks = CreateClients(serverIP, serverPort, numClients);
    await Task.WhenAll(clientTasks);

    Console.WriteLine("모든 클라이언트의 작업이 완료되었습니다.");
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
                // 소켓 생성 및 서버에 연결
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    await clientSocket.ConnectAsync(serverIP, serverPort);
                    Console.WriteLine($"클라이언트 {i + 1}: 서버에 접속 완료");

                    // 데이터 송신 및 수신 (생략)

                    // 서버와의 연결 종료
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    Console.WriteLine($"클라이언트 {i + 1}: 서버와의 연결 종료");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"클라이언트 {i + 1}에서 오류 발생: {ex.Message}");
            }
        });
    }

    return clientTasks;
}
