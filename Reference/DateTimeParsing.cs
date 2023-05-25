string dateTimeString = "2023-05-23 10:30:45";
DateTime dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

string dateString = "23/05/2023";
DateTime date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

string dateTimeString = "23-05-2023 10:30";
DateTime dateTime = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

DateTime timestamp = DateTime.Now;

// ���� 1: 24�ð� �������� �ð� ǥ��
string formattedTime1 = timestamp.ToString("HH:mm:ss");
Console.WriteLine(formattedTime1);  // ��� ��: "15:30:45"

// ���� 2: 12�ð� �������� �ð� ǥ�� (����/���� ����)
string formattedTime2 = timestamp.ToString("hh:mm:ss tt");
Console.WriteLine(formattedTime2);  // ��� ��: "03:30:45 PM"

// ���� 3: ���� ���ĺ� ��������� ǥ��
string formattedDate1 = timestamp.ToString("dd MMM yyyy");
Console.WriteLine(formattedDate1);  // ��� ��: "15 Jan 2023"

// ���� 4: ���� ���ڷ� ǥ��
string formattedDate2 = timestamp.ToString("dd/MM/yyyy");
Console.WriteLine(formattedDate2);  // ��� ��: "15/01/2023"

// ���� 5: ������ �� �ڸ��� ǥ��
string formattedYear = timestamp.ToString("yy");
Console.WriteLine(formattedYear);  // ��� ��: "23"

// ���� 6: �޷� ���� ǥ��
string formattedWeekOfYear = timestamp.ToString("ww");
Console.WriteLine(formattedWeekOfYear);  // ��� ��: "03"

// ���� 7: ���� ǥ��
string formattedDayOfWeek = timestamp.ToString("dddd");
Console.WriteLine(formattedDayOfWeek);  // ��� ��: "Monday"

// ���� 8: ������ ���� ǥ��
string formattedDayOfWeekAbbreviated = timestamp.ToString("ddd");
Console.WriteLine(formattedDayOfWeekAbbreviated);  // ��� ��: "Mon"

// ���� 9: ���� ��¥�� �ð� ǥ��
string formattedDateTime = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
Console.WriteLine(formattedDateTime);  // ��� ��: "2023-01-15 15:30:45"

// ���� 10: �⺻ ���� ������ (�Ϲ������� ���Ǵ� ����)
string formattedDefault = timestamp.ToString("G");
Console.WriteLine(formattedDefault);  // ��� ��: "1/15/2023 3:30:45 PM"
