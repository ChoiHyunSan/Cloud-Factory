using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userInsert : LogInSystemManager
{
    string URL = "http://localhost/mydb/userInsert.php";

    public InputField InputID, InputPW, InputNickName;
    public Button signUpButtonUI;

    private userSelect userSelect_findData;

    private bool signUpPositive;

    void Awake()
    {
        userSelect_findData = GetComponent<userSelect>();

    }
    public void SignUpCheckBtn() // �ߺ�üũ �ؽ�Ʈ �Ҵ�, �ߺ�Ȯ�� ��ư
    { 
        InputUsername_SignUp = InputID.text;

        // �� �ڽĵ� �ڵ����� �Ҵ��ϰ� ���ϳ�??
        InputPassword_SignUp = InputPW.text;
        InputNickName_SignUp = InputNickName.text;

        // �̹� �ִ� ���̵� �ƴ϶�� �߰� ����!
        if (userSelect_findData.FindUserData(InputUsername_SignUp) == -1)
        {
            signUpPositive = true;

            // �����ϱ� ��ư ��� ���� ó��
            signUpButtonUI.interactable = true;
        }
        else
        {
            signUpPositive = false;
            // �ȳ� UI �������ϱ�
            Debug.Log("�ߺ��� ���̵� ����");

            // �����ϱ� ��ư ��� �Ұ� ó��
            signUpButtonUI.interactable = false;
        }
            
    }

    public void SignUpAllocationBtn() // ���̵� ������ �߰�, �����ϱ� ��ư
    {
        AddUser(InputUsername_SignUp, InputPassword_SignUp, InputNickName_SignUp);
    }

    public void AddUser(string username, string password, string nickname)
    {
        WWWForm form = new WWWForm();

        // AddField
        // ���̺� �߰��� �̸�, �̸���, ��й�ȣ�� ���´�

        // �̹� �ִ� ���̵� �ƴ϶�� �߰� ����!
        if (signUpPositive)
        {
            form.AddField("addUsername", username);
            form.AddField("addNickname", nickname);
            form.AddField("addPassword", password);

            WWW www = new WWW(URL, form);

            // ȸ�������� ������ ������ ������Ʈ(�ڵ�)�ϰ� ����Ƽ���� ������Ʈ(����)�ϰ���
            userSelect_findData.StartCoroutine(userSelect_findData.UsersDataUpdate());
        }

    }
}
