// 외부 프로그램 실행하기

Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.Arguments = "argument1 argument2";
process.Start();
process.WaitForExit();

// 외부 프로그램 실행 및 출력 결과 읽기:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.UseShellExecute = false;
process.Start();
string output = process.StandardOutput.ReadToEnd();
process.WaitForExit();

// 외부 프로그램 실행하고 입력 데이터 전달하기:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardInput = true;
process.StartInfo.UseShellExecute = false;
process.Start();
StreamWriter writer = process.StandardInput;
writer.WriteLine("input_data");
writer.Close();
process.WaitForExit();

// 외부 프로그램 실행하고 입력 데이터 전달하고 출력 결과 읽기:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardInput = true;
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.UseShellExecute = false;
process.Start();
StreamWriter writer = process.StandardInput;
writer.WriteLine("input_data");
writer.Close();
string output = process.StandardOutput.ReadToEnd();
process.WaitForExit();

// StreamReader를 사용하여 외부 프로세스의 출력을 읽는 예시
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.UseShellExecute = false;
process.Start();

StreamReader reader = process.StandardOutput;
string output = reader.ReadToEnd();

process.WaitForExit();

Console.WriteLine(output);

// 외부 프로그램 실행하고 비동기적으로 출력 결과 읽기:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true; // 외부 프로그램의 출력을 리다이렉트
process.StartInfo.UseShellExecute = false; // 셸을 사용하지 않고 프로세스를 직접 실행
process.OutputDataReceived += (sender, e) => // 출력 데이터를 비동기적으로 읽는 이벤트 핸들러 설정
{
    if (!string.IsNullOrEmpty(e.Data))
    {
        // 출력 데이터 처리
        Console.WriteLine(e.Data);
    }
};
process.Start();
process.BeginOutputReadLine(); // BeginOutputReadLine 메서드를 호출하여 출력 데이터를 비동기적으로 읽기 시작합니다.
                               // 외부 프로그램의 출력 데이터가 준비되면 이벤트 핸들러가 호출됩니다
process.WaitForExit();

// 한 줄씩 입력하고 비동기적으로 출력을 받기
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process process = new Process();
        process.StartInfo.FileName = "external_program.exe";
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                // 출력 데이터 처리
                Console.WriteLine(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();

        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                break;

            // 입력 데이터 전달
            process.StandardInput.WriteLine(input);
        }

        process.WaitForExit();
    }
}

// 또다른 예시
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process process = new Process();
        process.StartInfo.FileName = "external_program.exe";
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                // 출력 데이터 처리
                Console.WriteLine(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();

        string input = "Hello";

        // 입력 데이터 전달
        process.StandardInput.WriteLine(input);

        // 외부 프로세스의 출력을 비동기적으로 받기 위해 별도의 스레드나 이벤트 핸들러 등을 사용할 수 있습니다.

        process.WaitForExit();
    }
}
