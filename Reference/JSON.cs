using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class JSONMain
{
    static void Main(string[] args)
    {
        JObject json = new JObject();
        json["name"] = "John Doe";
        json["salary"] = 300100;
        
        string jsonstr = json.Tostring();
        // JObject to string : 'JObject'.ToString()

        Console.WriteLine("Json : " + jsonstr);
        JObject json2 = JObject.Parse(jsonstr);
        // string to JObject : 'JObject.Parse('string ')'
        Console.WriteLine($"Name : {json2["name"]}, Salary : {json2["salary"]}");
    }
}