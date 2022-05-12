using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInSystemManager : MonoBehaviour
{
    // ȸ������ UI
    [HideInInspector]
    public GameObject SignUpUI;

    // ȸ�����Կ��� �Է��� ���� ID, PW, NickName
    [HideInInspector]
    public string InputUsername_SignUp, InputPassword_SignUp, InputNickName_SignUp;

    // ȸ���������� �Է��� ���� ������ ���ǰ� ������ ��
    [HideInInspector]
    public string WhereField_Delete, WhereCondition_Delete;

    // ȸ������ �������� �Է��� ���� ID, PW, NickName, ���ǰ� �� ������ ��
    [HideInInspector]
    public string InputUsername_Update, InputPassword_Update, InputNickName_Update, WhereField_Update, WhereCondition_Update;

    // ȸ������ UI�� Ȱ��ȭ ��Ű�� �Լ� (��ưUI�� �Ҵ�)
    public void SignUpUIActive()
    {
        SignUpUI.SetActive(true);
    }

    // ȸ������ UI�� ��Ȱ��ȭ ��Ű�� �Լ� (��ưUI�� �Ҵ�)
    public void SignUpUIUnActive()
    {
        SignUpUI.SetActive(false);
    }

}

