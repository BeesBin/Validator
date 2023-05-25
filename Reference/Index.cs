
1. 3번 부터 읽고, 이해가 안가면 앞에 기능 요약을 읽는다.
2. 읽으면서 사용 요소 파악하면, 스크립트 보면서 붙여 넣는다. 전체 구조 생각 하면서 작성하기.
3. 프로그램 완성 끝나면, 테스트에 심혈을 기울인다.



/*1. File I/O*/
// Text File Read & Print
static void PrintFile(string filename)
{
	string line;
	StreamReader file = new StreamReader(filename);
	while ((line = file.ReadLine()) != null)
	{
		System.Console.WriteLine(line);
	}
	file.Close();
}

// Binary File Read & Write
static void CopyFile(string InputFilename, string OutputFilename)
{
	const int BUF_SIZE = 4096;
	byte[] buffer = new byte[BUF_SIZE];
	int nFReadLen;
	FileStream fs_in = new FileStream(InputFilename, FileMode.Open, FileAccess.Read);
	FileStream fs_out =
	new FileStream(OutputFilename, FileMode.Create, FileAccess.Write);
	while ((nFReadLen = fs_in.Read(buffer, 0, BUF_SIZE)) > 0)
	{
		fs_out.Write(buffer, 0, nFReadLen);
	}
	fs_in.Close();
	fs_out.Close();
}

// File/Directory List 출력
void FileDirList()
{
	string[] subdirectoryEntries = Directory.GetDirectories(".");
	foreach (string subdirectory in subdirectoryEntries)
		Console.WriteLine("[{0}]", subdirectory);
	string[] fileEntries = Directory.GetFiles(".");
	foreach (string fileName in fileEntries)
		Console.WriteLine(fileName);
}

DirectoryInfo di = new DirectoryInfo("./INPUT");
FileInfo[] fiArr = di.GetFiles("*.*", SearchOption.AllDirectories);
foreach (var f in fiArr)
{
	Console.WriteLine(f.Name);
}

/* Process & Thread */
// Process : 운영 체제가 바라보는 일의 단위
// Thread : Process 내에서 다시 나누어지는 일의 단위

// 외부 프로세스실행
static string getProcessOutput(string fileName, string args)
{
	ProcessStartInfo start = new ProcessStartInfo();
	start.FileName = fileName;
	start.UseShellExecute = false;
	start.RedirectStandardOutput = true;
	start.CreateNoWindow = true;
	start.Arguments = args;
	
	Process process = Process.Start(start);
	StreamReader reader = process.StandardOutput;
	return reader.ReadLine();
}
static void Main(string[] args)
{
	string output = getProcessOutput("add_2sec.exe", "2 3");
	Console.WriteLine(output);
}

// Thread
class ThreadSample
{
	public class Worker
	{
		// This method will be called when the thread is started.
		public void DoWork()
		{
			Console.WriteLine("Thread is running ");
		}
	}
	static void Main(string[] args)
	{
		// Create the thread object. This does not start the thread.
		Worker workerObject1 = new Worker();
		Thread workerThread1 = new Thread(workerObject1.DoWork);
		// Start the worker thread.
		workerThread1.Start();
		// Use the Join method to block the current thread
		// until the object's thread terminates.
		workerThread1.Join();
	}
}

/* 4. Mutex (Synchronization) */
class ThreadSample
{
	public class Worker
	{
		private static Mutex mut = new Mutex();
		// This method will be called when the thread is started.
		public void DoWork()
		{
			mut.WaitOne();
			SaveFile(data);
			mut.ReleaseMutex();
		}
	}
	static void Main(string[] args)
	{
		Worker workerObject1 = new Worker();
		Thread workerThread1 = new Thread(workerObject1.DoWork);
		workerThread1.Start();
		workerThread1.Join();
	}
}

/* 5. Encryption/Decryption */
Encoding.UTF8.GetBytes(str); // string to byte
 // byte to String



// Base64
void Base64Sample(string str)
{
	byte[] byteStr = System.Text.Encoding.UTF8.GetBytes(str);
	string encodedStr;
	byte[] decodedBytes;
	// 문자열을 Base64로인코딩
	encodedStr = Convert.ToBase64String(byteStr);
	// 인코딩 결과를 문자열로 변환
	decodedBytes = Convert.FromBase64String(encodedStr);
	//문자열을 Base64로디코딩
	Console.WriteLine(Encoding.Default.GetString(decodedBytes));
	// 디코딩 결과를 문자열로 변환 후 출력
}

// SHA-256
void SHA256Sample(string strInput)
{
	// 입력 문자열 바이트 배열로 변환
	byte[] hashValue;
	byte[] byteInput = System.Text.Encoding.UTF8.GetBytes(strInput);
	SHA256 mySHA256 = SHA256Managed.Create();
	hashValue = mySHA256.ComputeHash(byteInput);
	// SHA - 256 클래스 인스턴스 생성
	// SHA - 256 암호화
	for (int i = 0; i < hashValue.Length; i++)
		Console.Write(String.Format("{0:X2}", hashValue[i]));
	// 2자리의 16진수 문자열로 포맷하는 방법
    // 결과 : 03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4
}

6. Network - Socket

7. Network - Http

8. Json

