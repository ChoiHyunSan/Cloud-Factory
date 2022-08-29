using System.Collections;
using System.Collections.Generic;
// ���� ����°� ���ڿ� ó���ϱ� ���ؼ�
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class JsonSaveLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* SAVE     */

        // ������ ������ ��ο� ���� �̸����� FileStream�� ����
        FileStream stream = new FileStream(Application.dataPath + "/Data/test.json", FileMode.OpenOrCreate);
        JsonTestClass jTest1 = new JsonTestClass();
        string jsonData = JsonConvert.SerializeObject(jTest1);
        // ���ڿ��� json�����͸� Encoding.UTF8�� GetBytes �Լ��� byte�迭�� �������
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        // �� �����͸� ���Ͻ�Ʈ���� ���ش�
        stream.Write(data, 0, data.Length);
        stream.Close();

       

        /* LOAD */
        //FileStream stream = new FileStream(Application.dataPath + "/Data/test.json", FileMode.Open);
        //byte[] data = new byte[stream.Length];
        //stream.Read(data, 0, data.Length);
        //stream.Close();
        //string jsonData = Encoding.UTF8.GetString(data);
        //JsonTestClass jTest2 = JsonConvert.DeserializeObject<JsonTestClass>(jsonData);
        //jTest2.Print();

    }
}

/*
������ ������ ���̺� Ȱ��������,
�۾��� ������ ����Ǿ�� �ϴ� ��쿡 �׳� json �����ͷ� ������ 
Ŭ���̾�Ʈ���� json �������� ������ ����
���� Ŭ������ ¥���ϴ� ��쵵 �߻��Ѵ�.
 
���� ���� json�����͸� ������ �м��ؼ� Ŭ������ ¥�� �ǰ����� �ξ�
�����ϰ� ����ġ�� json�����͸� Ŭ������ ����� ����� �ֽ��ϴ�.

���ۿ��� json 2 C sharp�ϸ� json �����͸� c#Ŭ������ ��ȯ���ִ� ����Ʈ���� ����

������ ����Ʈ ��� json�����͸� �Է��ѵ� convert��ư�ϸ� ����
�˾Ƽ� �м��ؼ� �ڵ����� C# Ŭ������ �������
 */
