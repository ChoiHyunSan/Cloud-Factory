using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/*
 * �ܺ� ���̺귯���� Newtonsoft�� json ���̺귯��
 */

public class NewtonsoftJsonExample : MonoBehaviour
{
    void Start()
    {
        // JsonTestClass�� ������Ʈ�� �����ؼ� Json �����ͷ� �����        
        JsonTestClass jTest1 = new JsonTestClass();
        // �� ������ ����ȭ��� �θ���
        string jsonData = JsonConvert.SerializeObject(jTest1);
        Debug.Log(jsonData);

        // json �����͸� �ٽ� ������Ʈ�� �ٲ� ���� json�����͸� � ������Ʈ��
        // ��ȯ�ϴ��� ��������� �Լ��� �˷������
        // ����, json�����Ͱ� ���� ������ �Լ��� �˷��� Ŭ������ ������ �ٸ��ٸ�
        // ��ȯ ���߿� ������ �߻��ϱ� ������ �� Ȯ���ϰ� �����ؾ���.
        JsonTestClass jTest2 = JsonConvert.DeserializeObject<JsonTestClass>(jsonData);
        jTest2.Print();

        // ����Ƽ���� json�� ����� �� ������ ��!!
        // 1. �ڱ� ���� ����
        // �� �ڵ��� �ǵ��� Ŭ������ ����ִ� int Ÿ���� i�� json �����ͷ�
        // ���� �����Ϸ��� ��
        // �� �ڵ� �״�� ����ϸ� jsonSerialiszationException ���� �߻� (�ڱ� ���� ����)
        // GameObject obj = new GameObject();
        // obj.AddComponent<TestMono>();
        // // Gameobject.gameobject...
        // Debug.Log(JsonConvert.SerializeObject(obj.GetComponent<TestMono>()));
        // Newtonsoft�� json ���̺귯���δ� �������̺� ��ӹ޴� Ŭ������ ������Ʈ��
        // json �����ͷ� �ø���������� ���� ����.
        // �׷��� ������ �������̺� ��ӹ޴� Ŭ������ ������Ʈ�� �ø���������ϴ� ��ſ�
        // ��ũ��Ʈ�� ������ �ִ� ������Ƽ �߿��� �ʿ��� ������Ƽ�� Ŭ������ ���
        // �ش� Ŭ������ �ø�������� �ϰų�
        // ����Ƽ�� �⺻ �����ϴ� jsonUtility ��� ����ؼ� �ø�������� �ؾ� ��.

        // 2. Vector3�� �ø���������ϴ� ��, �̰� ���� �ڱ� ����        
        JsonVector jVector = new JsonVector();
        // Debug.Log(JsonConvert.SerializeObject(jVector));
        // jVector.vector3.normalized.normalized.normalized ...
        // �ذ���-> �ڵ鸵 ����
        JsonSerializerSettings setting = new JsonSerializerSettings();
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        Debug.Log(JsonConvert.SerializeObject(jVector, setting));
        // ������ �̷��� �����ϰ� �ص� normalized ���ͳ� ������ ���� ���� ���ʿ��� ������
        // �ø�������� �Ǳ� ������ �������� json�������� ���̳� �뷮�� �þ�� ������ ����.
        // �ܺ� ���̺귯���� �̿��ؼ� Vector3���� ��ǥ ������ json�����ͷ� �ø�������� �ϱ� ���ϸ�
        /*
         * public class SerializableVector3
         * {
         *  public float x;
         *  public float y;
         *  public float z;
         * }
         */
        // �̷������� �ø�������� �� ���� Ŭ������ ���� �ø�������� �ؾ��Ѵ�.
    }
}

public class JsonVector
{
    public Vector3 vector3 = new Vector3(3, 3, 3);
}

public class JsonTestClass
{
    // ��밡���� �������
    public int i;
    public float f;
    public bool b;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();
    public Dictionary<string, float> fDictionary = new Dictionary<string, float>();
    public IntVector2 iVector;

    // �����ڿ��� ������� �ʱ�ȭ
    public JsonTestClass()
    {
        i = 10;
        f = 99.9f;
        b = true;
        str = "JSON TEST String";
        iArray = new int[] { 1, 1, 2, 3, 4, 8, 12, 21, 34, 55 };

        for (int idx = 0; idx < 5; idx++)
        {
            iList.Add(2 * idx);
        }

        fDictionary.Add("PIE", Mathf.PI);
        fDictionary.Add("Epsilon", Mathf.Epsilon);
        fDictionary.Add("Sprt(2)", Mathf.Sqrt(2));

        iVector = new IntVector2(3, 2);
    }

    // ����Ʈ�ؼ� �ϳ��� ���
    public void Print()
    {
        Debug.Log("i : " + i);
        Debug.Log("f : " + f);
        Debug.Log("b : " + b);
        Debug.Log("str : " + str);

        for (int idx = 0; idx < iArray.Length; idx++)
        {
            Debug.Log(string.Format("iArray[{0}] = {1}", idx, iArray[idx]));
        }

        for (int idx = 0; idx < iList.Count; idx++)
        {
            Debug.Log(string.Format("iList[{0}] = {1}", idx, iList[idx]));
        }

        foreach (var data in fDictionary)
        {
            Debug.Log(string.Format("iDictionary[{0}] = {1}", data.Key, data.Value));
        }

        Debug.Log("iVector = " + iVector.x + ", " + iVector.y);
    }

    [System.Serializable]
    public class IntVector2
    {
        public int x;
        public int y;
        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}