

/* JSON : JavaScript Object Notation
 * 데이터 오브젝트를 전달하기 위해 텍스트를 사용하는 개방형 표준 포맷
 * 속성-값 쌍 (attribute-value paris), array data types, any other serializable value, 키-값 쌍
 * 
 * 수(Number) : "age":45
 * 문자열(String) : "name":"spiderman"
 * 참/거짓(Boolean) : "married":true
 * 배열(Array) : "specialty":["martial art", "gun"]
 * 객체(Object) : "vaccine":{"1st":"done", "2nd":"expected"}
 * 빈 값 : "address":null
 * 
 * Ex) "children":[{"name","spiderboy","age":10},{"name":"spidergirl","age":8}]
 * Key: chidren, Value: Array
 *				 Object: Key:"name" Value:string, Key:"age" Value:Number
 * 
 * Newtonsoft.Json (https://www.newtonsoft.com/json)
 * 
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class JsonBasic
{
	public static void main(string args[])
    {
        JObject json = new JObject();
        json["name"] = "John Doe";
        json["salary"] = 300100;

        string jsonstr = json.ToString();
        // Json to String : JObject.ToString(##JObject##)
        JObject json2 = JObject.Parse(jsonstr);
        // String to Json : JObject.Parse(##str##)
    }
}