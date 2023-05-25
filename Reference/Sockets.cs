//SERVER
Socket handler = listener.Accept();
NetworkStream ns = new NetworkStream(handler);
BinaryReader br = new BinaryReader(ns);
FileStream fs = null;
try
{
    string filename;
    // 파일이름 수신
    while ((filename = br.ReadString()) != null)
    {
        // 파일크기 수신
        int length = (int)br.ReadInt64();
        fs = new FileStream(ReceiveFolder + "/" +
        filename, FileMode.Create);
        while (length > 0)
        {
            // 파일내용 수신
            int nReadLen = br.Read(bytes, 0,
            Math.Min(BUF_SIZE, length));
            fs.Write(bytes, 0, nReadLen);
            length -= nReadLen;
        }
        fs.Close();
        Console.WriteLine(filename + " is received.");
    }
    handler.Shutdown(SocketShutdown.Both);
    handler.Close();
}
}
//네트워크프로그래밍
//▪ CLIENT
sender.Connect(remoteEP);
NetworkStream ns = new NetworkStream(sender);
BinaryWriter bw = new BinaryWriter(ns);
DirectoryInfo di = new DirectoryInfo("./ClientFiles");
FileInfo[] fiArr = di.GetFiles();
foreach (FileInfo infoFile in fiArr)
{
    // 파일이름 전송
    bw.Write(infoFile.Name);
    long lSize = infoFile.Length;
    // 파일크기 전송
    bw.Write(lSize);
    // 파일내용 전송
    FileStream fs = new FileStream(infoFile.FullName, FileMode.Open);
    while (lSize > 0)
    {
        int nReadLen = fs.Read(bytes, 0,
        Math.Min(BUF_SIZE, (int)lSize));
        bw.Write(bytes, 0, nReadLen);
        lSize -= nReadLen;
    }
    fs.Close();