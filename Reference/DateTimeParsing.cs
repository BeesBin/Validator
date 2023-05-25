string dateTimeString = "2023-05-23 10:30:45";
DateTime dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

string dateString = "23/05/2023";
DateTime date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

string dateTimeString = "23-05-2023 10:30";
DateTime dateTime = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

DateTime timestamp = DateTime.Now;

// 예제 1: 24시간 형식으로 시간 표시
string formattedTime1 = timestamp.ToString("HH:mm:ss");
Console.WriteLine(formattedTime1);  // 출력 예: "15:30:45"

// 예제 2: 12시간 형식으로 시간 표시 (오전/오후 구분)
string formattedTime2 = timestamp.ToString("hh:mm:ss tt");
Console.WriteLine(formattedTime2);  // 출력 예: "03:30:45 PM"

// 예제 3: 월을 알파벳 축약형으로 표시
string formattedDate1 = timestamp.ToString("dd MMM yyyy");
Console.WriteLine(formattedDate1);  // 출력 예: "15 Jan 2023"

// 예제 4: 월을 숫자로 표시
string formattedDate2 = timestamp.ToString("dd/MM/yyyy");
Console.WriteLine(formattedDate2);  // 출력 예: "15/01/2023"

// 예제 5: 연도를 두 자리로 표시
string formattedYear = timestamp.ToString("yy");
Console.WriteLine(formattedYear);  // 출력 예: "23"

// 예제 6: 달력 주차 표시
string formattedWeekOfYear = timestamp.ToString("ww");
Console.WriteLine(formattedWeekOfYear);  // 출력 예: "03"

// 예제 7: 요일 표시
string formattedDayOfWeek = timestamp.ToString("dddd");
Console.WriteLine(formattedDayOfWeek);  // 출력 예: "Monday"

// 예제 8: 요일을 약어로 표시
string formattedDayOfWeekAbbreviated = timestamp.ToString("ddd");
Console.WriteLine(formattedDayOfWeekAbbreviated);  // 출력 예: "Mon"

// 예제 9: 현재 날짜와 시간 표시
string formattedDateTime = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
Console.WriteLine(formattedDateTime);  // 출력 예: "2023-01-15 15:30:45"

// 예제 10: 기본 형식 지정자 (일반적으로 사용되는 형식)
string formattedDefault = timestamp.ToString("G");
Console.WriteLine(formattedDefault);  // 출력 예: "1/15/2023 3:30:45 PM"
