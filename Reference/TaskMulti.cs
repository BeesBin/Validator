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
            Console.WriteLine("�۾��� �����մϴ�.");
            // �۾� ����
        });

        Console.WriteLine("�۾��� �����߽��ϴ�.");

        await task; // �۾��� �Ϸ�� ������ ���

        Console.WriteLine("�۾��� �Ϸ�Ǿ����ϴ�.");
    }
}

// Task ��ȯ �� ó��
Task<int> task = Task.Run(() => CalculateResult());
int result = task.Result;

// ���� ���� Task ���� ����
Task[] tasks = new Task[3];
tasks[0] = Task.Run(() => DoTask1());
tasks[1] = Task.Run(() => DoTask2());
tasks[2] = Task.Run(() => DoTask3());
Task.WaitAll(tasks);

// Task ����� ��Ƽ� ó��
List<Task<int>> tasks = new List<Task<int>>();
for (int i = 0; i < 5; i++)
{
    int index = i; // Ŭ���� ������ �ε��� �� ����
    tasks.Add(Task.Run(() => CalculateResult(index)));
}
Task.WaitAll(tasks.ToArray());
int[] results = tasks.Select(t => t.Result).ToArray();


// Task ���
CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken token = cts.Token;
// CancellationToken�� �۾� ���ο��� �۾� ��� ���θ� Ȯ���ϴ� �� ���

Task task = Task.Run(() => DoTask(token), token);
// ������ CancellationToken�� Task�� �����Ͽ� �۾� ��� ���θ� Ȯ���ϵ���
// Ư�� ���ǿ� ���� �۾� ���
if (condition)
{
    cts.Cancel();
}


// Task ���� ����
Task task = Task.Run(() => DoTask1())
    .ContinueWith(previousTask => DoTask2())
    .ContinueWith(previousTask => DoTask3());
task.Wait();

// Task ������ �� Ÿ�Ӿƿ�
Task task = Task.Delay(2000).ContinueWith(t => DoTask());
if (task.Wait(3000))
{
    // Ÿ�Ӿƿ� ���� �۾� �Ϸ�
}
else
{
    // Ÿ�Ӿƿ� �߻�
}

// Task WaitAll WhenAll ����
// ���� �񵿱��� ������ �� �Ȱ���.
Task.WaitAll()
await Task.WhenAll()


class Program
{
    static void Main()
    {
        // Task�� ����� ��Ƽ�½�ŷ(���� ����) ����
        Task task1 = Task.Run(DoTask1);
        Task task2 = Task.Run(DoTask2);

        // �ٸ� �۾� ����
        Console.WriteLine("Main task continues its work.");

        // �� ���� Task�� �Ϸ�� ������ ���
        Task.WaitAll(task1, task2);

        Console.WriteLine("All tasks completed.");

        // ���α׷� ���� ���
        Console.ReadLine();
    }

    static void DoTask1()
    {
        Console.WriteLine("Task 1 started.");

        // �۾� ����
        Task.Delay(2000).Wait();

        Console.WriteLine("Task 1 completed.");
    }

    static void DoTask2()
    {
        Console.WriteLine("Task 2 started.");

        // �۾� ����
        Task.Delay(3000).Wait();

        Console.WriteLine("Task 2 completed.");
    }
}

// Thread�� ����ϸ� ���� �����带 �����ϰ� ����
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Thread�� ����� ���� ���� ����
        Thread thread1 = new Thread(DoTask1);
        Thread thread2 = new Thread(DoTask2);

        // �� ���� ������ ����
        thread1.Start();
        thread2.Start();

        // �ٸ� �۾� ����
        Console.WriteLine("Main task continues its work.");

        // �� ���� �����尡 �Ϸ�� ������ ���
        thread1.Join();
        thread2.Join();

        Console.WriteLine("All tasks completed.");

        // ���α׷� ���� ���
        Console.ReadLine();
    }

    static void DoTask1()
    {
        Console.WriteLine("Task 1 started.");

        // �۾� ����
        Thread.Sleep(2000);

        Console.WriteLine("Task 1 completed.");
    }

    static void DoTask2()
    {
        Console.WriteLine("Task 2 started.");

        // �۾� ����
        Thread.Sleep(3000);

        Console.WriteLine("Task 2 completed.");
    }
}

