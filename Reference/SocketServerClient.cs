// SERVER
static void Main(string[] args)
{
    IPAddress iPAddress = iPAddress.Parse("127.0.0.1");
    IPEndPoint localEndPoint = new IPEndPoint(iPAddress. 9090);

    Socket listener = new Socket(AddressFamiliy.InterNet, SocketType.Stream, ProtocolType.Tcp);

    try
    {
        listener.Bind(localEndPoint);
        listener.Listen(10);

        Socket handler = listener.Accept();
        byte[] msg = Encoding.ASCII.GetBytes("test");
        handler.Send(msg);
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}

// CLIENT
static void Main(string[] args)
{
    byte[] bytes = new byte[1024];
    try
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new
        IPEndPoint(ipAddress, 9090);

        Socket sender = new Socket(AddressFamiliy.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(remoteEP);
            int bytesRec = sender.Receive(bytes);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}