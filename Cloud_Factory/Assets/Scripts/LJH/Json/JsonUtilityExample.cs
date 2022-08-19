using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ����Ƽ���� �����ϴ� jsonUtility
 */

public class JsonUtilityExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // jsonutility�� ������ �⺻���� ������ Ÿ�԰� �迭,
        // ����Ʈ�� ���� �ø������� �����Ѵ�
        // ��ųʸ��� ���� ������ Ŭ������ ������ ����
        // ���� ������ Ŭ������ ��쿡�� [System.Serializable] ��Ʈ����Ʈ��
        // �ٿ���߸� Json�����ͷ� ��ȯ�Ǹ�, ��ųʸ��� �ƿ� �������� ����.
        JsonTestClass jTest1 = new JsonTestClass();
        string jsonData = JsonUtility.ToJson(jTest1);
        Debug.Log(jsonData);

        JsonTestClass jTest2 = JsonUtility.FromJson<JsonTestClass>(jsonData);
        jTest2.Print();

        // jsonUtility�� ����ϸ� ���ʿ��� ���� �����ϰ� �ʿ��� ��ǥ�� ����ȴ�
        JsonVector jVector = new JsonVector();
        string jsonData2 = JsonUtility.ToJson(jVector);
        Debug.Log(jsonData2);

        // ���� �������̺� ��ӹ޴� Ŭ������ ������Ʈ�� �ø�������� ����
        GameObject obj = new GameObject();
        var test1 = obj.AddComponent<TestMono>();
        test1.i = 100;
        test1.v3 /= 10;
        // getcomponent������ ����������� Ŭ������ �̸����� �ø���������ؾ���
        string jsonData3 = JsonUtility.ToJson(obj.GetComponent<TestMono>());
        Debug.Log(jsonData3);

        // ������Ʈ�� �ٽ� ��ȯ
        // JsonUtility.FromJson<TestMono>(jsonData3);
        // ���ο� �ν��Ͻ��� ������ �� ���ٴ� �����߻� �� ��ø�������� ����
        // �ذ��� ->
        GameObject obj2 = new GameObject();
        JsonUtility.FromJsonOverwrite(jsonData3, obj2.AddComponent<TestMono>());

        // �� ����� �������̺� ��ӹ޴� ������Ʈ�� Json �����͸�
        // �ٽ� ������Ʈ�� ������� ���� ���¸� ���� ������Ʈ�� �̸� ������ �ڿ�
        // ��ø������� �ؾ��Ѵٴ� ��

        // �� ����� ����Ͽ� ���ӳ��� �����͸� json�����ͷ� �����ؼ�
        // ���� ���� ��Ȳ�� �����ϰų�, �ٽ� �ҷ��ͼ� ������� �����ϴ�
        // ���̺� �ε� ��� �����Ҽ�����
    }
}
