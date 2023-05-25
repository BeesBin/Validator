using System;
using System.Threading.Tasks;

// Task
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task task = Task.Run(() =>
        {
            Console.WriteLine("작업을 실행합니다.");
            // 작업 수행
        });

        Console.WriteLine("작업을 시작했습니다.");

        await task; // 작업이 완료될 때까지 대기

        Console.WriteLine("작업이 완료되었습니다.");
    }
}

// Task 반환 값 처리
Task<int> task = Task.Run(() => CalculateResult());
int result = task.Result;

// 여러 개의 Task 병렬 실행
Task[] tasks = new Task[3];
tasks[0] = Task.Run(() => DoTask1());
tasks[1] = Task.Run(() => DoTask2());
tasks[2] = Task.Run(() => DoTask3());
Task.WaitAll(tasks);

// Task 결과를 모아서 처리
List<Task<int>> tasks = new List<Task<int>>();
for (int i = 0; i < 5; i++)
{
    int index = i; // 클로저 변수로 인덱스 값 보존
    tasks.Add(Task.Run(() => CalculateResult(index)));
}
Task.WaitAll(tasks.ToArray());
int[] results = tasks.Select(t => t.Result).ToArray();


// Task 취소
CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken token = cts.Token;
// CancellationToken은 작업 내부에서 작업 취소 여부를 확인하는 데 사용

Task task = Task.Run(() => DoTask(token), token);
// 생성한 CancellationToken을 Task에 전달하여 작업 취소 여부를 확인하도록
// 특정 조건에 따라 작업 취소
if (condition)
{
    cts.Cancel();
}


// Task 연속 실행
Task task = Task.Run(() => DoTask1())
    .ContinueWith(previousTask => DoTask2())
    .ContinueWith(previousTask => DoTask3());
task.Wait();

// Task 딜레이 및 타임아웃
Task task = Task.Delay(2000).ContinueWith(t => DoTask());
if (task.Wait(3000))
{
    // 타임아웃 전에 작업 완료
}
else
{
    // 타임아웃 발생
}

// Task WaitAll WhenAll 차이
// 동기 비동기의 차이일 뿐 똑같다.
Task.WaitAll()
await Task.WhenAll()


class Program
{
    static void Main()
    {
        // Task를 사용한 멀티태스킹(병렬 수행) 예시
        Task task1 = Task.Run(DoTask1);
        Task task2 = Task.Run(DoTask2);

        // 다른 작업 수행
        Console.WriteLine("Main task continues its work.");

        // 두 개의 Task가 완료될 때까지 대기
        Task.WaitAll(task1, task2);

        Console.WriteLine("All tasks completed.");

        // 프로그램 종료 대기
        Console.ReadLine();
    }

    static void DoTask1()
    {
        Console.WriteLine("Task 1 started.");

        // 작업 수행
        Task.Delay(2000).Wait();

        Console.WriteLine("Task 1 completed.");
    }

    static void DoTask2()
    {
        Console.WriteLine("Task 2 started.");

        // 작업 수행
        Task.Delay(3000).Wait();

        Console.WriteLine("Task 2 completed.");
    }
}

// Thread를 사용하면 직접 스레드를 생성하고 관리
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Thread를 사용한 병렬 수행 예시
        Thread thread1 = new Thread(DoTask1);
        Thread thread2 = new Thread(DoTask2);

        // 두 개의 스레드 시작
        thread1.Start();
        thread2.Start();

        // 다른 작업 수행
        Console.WriteLine("Main task continues its work.");

        // 두 개의 스레드가 완료될 때까지 대기
        thread1.Join();
        thread2.Join();

        Console.WriteLine("All tasks completed.");

        // 프로그램 종료 대기
        Console.ReadLine();
    }

    static void DoTask1()
    {
        Console.WriteLine("Task 1 started.");

        // 작업 수행
        Thread.Sleep(2000);

        Console.WriteLine("Task 1 completed.");
    }

    static void DoTask2()
    {
        Console.WriteLine("Task 2 started.");

        // 작업 수행
        Thread.Sleep(3000);

        Console.WriteLine("Task 2 completed.");
    }
}

