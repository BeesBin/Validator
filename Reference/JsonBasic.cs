

/* JSON : JavaScript Object Notation
 * ������ ������Ʈ�� �����ϱ� ���� �ؽ�Ʈ�� ����ϴ� ������ ǥ�� ����
 * �Ӽ�-�� �� (attribute-value paris), array data types, any other serializable value, Ű-�� ��
 * 
 * ��(Number) : "age":45
 * ���ڿ�(String) : "name":"spiderman"
 * ��/����(Boolean) : "married":true
 * �迭(Array) : "specialty":["martial art", "gun"]
 * ��ü(Object) : "vaccine":{"1st":"done", "2nd":"expected"}
 * �� �� : "address":null
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