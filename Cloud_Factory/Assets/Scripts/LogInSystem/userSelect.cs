using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class userSelect : LogInSystemManager
{
    string URL = "http://localhost/mydb/userSelect.php";
    public string[] usersData;

    IEnumerator Start()
    {
        WWW users = new WWW(URL);
        yield return users;

        string usersDataString = users.text;
        usersData = usersDataString.Split(';');

#region USER INFO DEBUG
        Debug.Log(users.text);
        Debug.Log(usersData.Length);
        for (int i = 0; i < usersData.Length - 1; i++)
        {
            Debug.Log(GetValueData(usersData[i], "username:"));
            Debug.Log(GetValueData(usersData[i], "nickname:"));
            Debug.Log(GetValueData(usersData[i], "password:"));
            Debug.Log("------------------------------------");
        }
#endregion
    }

    // �������� �ڵ� ������Ʈ�� ����� ����Ƽ������ ������Ʈ�ϰ���
    public IEnumerator UsersDataUpdate()
    {
        WWW users = new WWW(URL);
        yield return users;

        string usersDataString = users.text;
        usersData = usersDataString.Split(';');

#region USER INFO UPDATE DEBUG
        Debug.Log("UPDATE!!");
        Debug.Log(users.text);
        Debug.Log(usersData.Length);
        for (int i = 0; i < usersData.Length - 1; i++)
        {
            Debug.Log(GetValueData(usersData[i], "username:"));
            Debug.Log(GetValueData(usersData[i], "nickname:"));
            Debug.Log(GetValueData(usersData[i], "password:"));
            Debug.Log("------------------------------------");
        }
#endregion
    }

    // ȸ�����Ե� �̸�,�̸���,��й�ȣ�� �ҷ��´�.
    string GetValueData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

    // userData�� �ִ���! ���࿡ �ִٸ� �� �Լ��� Ȱ���ؼ� �����͸� ������ �� �� ����
    // �ִٸ� ȸ�� index���� ������, ���ٸ� -1���� ����
    // inputUsername : �α��ο� ����� ���� ���̵�
    public int FindUserData(string inputUsername)
    {
        for (int i = 0; i < usersData.Length - 1; i++)
        {
            // �Է��� ���̵� ���� ������ ã��
            if (inputUsername == GetValueData(usersData[i], "username:"))
            {
                Debug.Log(GetValueData(usersData[i], "username:"));
                Debug.Log(GetValueData(usersData[i], "nickname:"));
                Debug.Log(GetValueData(usersData[i], "password:"));

                return i;
            }
        }
        return -1;
    }

    // USER NICKNAME ������
    public string GetUserNickname(int index)
    {
        return GetValueData(usersData[index], "nickname:");
    }

    // USER PASSWORD ������
    public string GetUserPassword(int index)
    {
        return GetValueData(usersData[index], "password:");
    }


}
