DateTime startTime = new DateTime(2023, 5, 24, 10, 30, 0);
DateTime endTime = new DateTime(2023, 5, 24, 12, 15, 0);

TimeSpan duration = endTime - startTime;
int seconds = (int)duration.TotalSeconds;

Console.WriteLine("�� �ð� ������ �ð� ����: " + seconds + "��");


