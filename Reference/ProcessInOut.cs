// �ܺ� ���α׷� �����ϱ�

Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.Arguments = "argument1 argument2";
process.Start();
process.WaitForExit();

// �ܺ� ���α׷� ���� �� ��� ��� �б�:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.UseShellExecute = false;
process.Start();
string output = process.StandardOutput.ReadToEnd();
process.WaitForExit();

// �ܺ� ���α׷� �����ϰ� �Է� ������ �����ϱ�:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardInput = true;
process.StartInfo.UseShellExecute = false;
process.Start();
StreamWriter writer = process.StandardInput;
writer.WriteLine("input_data");
writer.Close();
process.WaitForExit();

// �ܺ� ���α׷� �����ϰ� �Է� ������ �����ϰ� ��� ��� �б�:
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

// StreamReader�� ����Ͽ� �ܺ� ���μ����� ����� �д� ����
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.UseShellExecute = false;
process.Start();

StreamReader reader = process.StandardOutput;
string output = reader.ReadToEnd();

process.WaitForExit();

Console.WriteLine(output);

// �ܺ� ���α׷� �����ϰ� �񵿱������� ��� ��� �б�:
Process process = new Process();
process.StartInfo.FileName = "external_program.exe";
process.StartInfo.RedirectStandardOutput = true; // �ܺ� ���α׷��� ����� �����̷�Ʈ
process.StartInfo.UseShellExecute = false; // ���� ������� �ʰ� ���μ����� ���� ����
process.OutputDataReceived += (sender, e) => // ��� �����͸� �񵿱������� �д� �̺�Ʈ �ڵ鷯 ����
{
    if (!string.IsNullOrEmpty(e.Data))
    {
        // ��� ������ ó��
        Console.WriteLine(e.Data);
    }
};
process.Start();
process.BeginOutputReadLine(); // BeginOutputReadLine �޼��带 ȣ���Ͽ� ��� �����͸� �񵿱������� �б� �����մϴ�.
                               // �ܺ� ���α׷��� ��� �����Ͱ� �غ�Ǹ� �̺�Ʈ �ڵ鷯�� ȣ��˴ϴ�
process.WaitForExit();

// �� �پ� �Է��ϰ� �񵿱������� ����� �ޱ�
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
                // ��� ������ ó��
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

            // �Է� ������ ����
            process.StandardInput.WriteLine(input);
        }

        process.WaitForExit();
    }
}

// �Ǵٸ� ����
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
                // ��� ������ ó��
                Console.WriteLine(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();

        string input = "Hello";

        // �Է� ������ ����
        process.StandardInput.WriteLine(input);

        // �ܺ� ���μ����� ����� �񵿱������� �ޱ� ���� ������ �����峪 �̺�Ʈ �ڵ鷯 ���� ����� �� �ֽ��ϴ�.

        process.WaitForExit();
    }
}
