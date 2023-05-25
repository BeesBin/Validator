using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            { "Apple", 3 },
            { "Banana", 1 },
            { "Orange", 2 },
            { "Grape", 4 },
            { "Cherry", 5 }
        };

        var sortedKeys = dictionary.Keys.OrderByDescending(key => key).ToList();

        Console.WriteLine("�������� ���� ���:");
        foreach (string key in sortedKeys)
        {
            Console.WriteLine(key);
        }
    }
}

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> stringList = new List<string>()
        {
            "Apple", "Banana", "Orange", "Grape", "Cherry"
        };

        stringList.Sort();
        stringList.Reverse();

        Console.WriteLine("�������� ���� ���:");
        foreach (string str in stringList)
        {
            Console.WriteLine(str);
        }
    }
}

