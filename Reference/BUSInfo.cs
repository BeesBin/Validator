using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "BUS.txt";

        // 파일 읽기
        string fileContent = File.ReadAllText(filePath);

        // 데이터 파싱 및 저장
        List<DataEntry> dataEntries = ParseData(fileContent);

        // 저장된 데이터 출력
        foreach (DataEntry entry in dataEntries)
        {
            Console.WriteLine("Timestamp: " + entry.Timestamp);
            foreach (KeyValuePair<string, int> busData in entry.BusData)
            {
                Console.WriteLine("Bus: " + busData.Key + ", Value: " + busData.Value);
            }
            Console.WriteLine();
        }

        // 데이터 출력을 파일로 저장
        using (StreamWriter writer = new StreamWriter("OUT.txt"))
        {
            foreach (DataEntry entry in dataEntries)
            {
                string outputLine = FormatDataEntry(entry);
                writer.WriteLine(outputLine);
            }
        }

        Console.WriteLine("Data written to file: OUT.txt");
    }

    static List<DataEntry> ParseData(string data)
    {
        List<DataEntry> dataEntries = new List<DataEntry>();

        string[] lines = data.Split('\n');
        foreach (string line in lines)
        {
            string[] parts = line.Trim().Split('#');

            if (parts.Length == 2)
            {
                string[] timestampAndData = parts[0].Split(',');
                string[] busData = parts[1].Split(',');

                if (timestampAndData.Length == 2 && busData.Length % 2 == 0)
                {
                    DataEntry entry = new DataEntry();
                    entry.Timestamp = DateTime.Parse(timestampAndData[0]);

                    for (int i = 0; i < busData.Length; i += 2)
                    {
                        string busId = busData[i];
                        int busValue = int.Parse(busData[i + 1]);
                        entry.BusData.Add(busId, busValue);
                    }

                    dataEntries.Add(entry);
                }
            }
        }

        return dataEntries;
    }

    static string FormatDataEntry(DataEntry entry)
    {
        string timestampString = entry.Timestamp.ToString("HH:mm:ss");

        List<string> formattedBusData = new List<string>();
        foreach (KeyValuePair<string, int> busData in entry.BusData)
        {
            string busId = busData.Key;
            int busValue = busData.Value;

            string formattedEntry = string.Format("{0}#{1},{2}", timestampString, busId, busValue.ToString("D5"));
            formattedBusData.Add(formattedEntry);
        }

        return string.Join(",", formattedBusData);
    }
}

class DataEntry
{
    public DateTime Timestamp { get; set; }
    public Dictionary<string, int> BusData { get; set; } = new Dictionary<string, int>();
}
