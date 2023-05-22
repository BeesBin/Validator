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

/*
JToken
JToken은 JSON 데이터의 일반적인 개념을 나타내는 추상 클래스
JObject, JArray, JValue 등의 구체적인 하위 클래스를 가짐
JToken은 JSON 데이터를 탐색하고 쿼리하는 데 사용됩니다.

JObject
JObject 클래스는 JSON 객체: 키와 값의 쌍으로 구성
JObject는 특정 키를 사용하여 JSON 객체의 속성에 접근하고 값을 가져오거나 설정할 수 있는 메서드와 속성을 제공

JArray
JArray 클래스는 JSON 배열을 나타냅니다.
JArray는 여러 개의 값이 순서대로 저장된 컬렉션을 제공
배열의 각 요소는 JToken으로 나타내며, 이를 사용하여 배열의 값에 접근하고 조작할 수 있습니다.
*/