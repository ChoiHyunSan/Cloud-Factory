using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userDelete : LogInSystemManager
{
    string URL = "http://localhost/mydb/userDelete.php";

    // ȸ�� ���� ��ư
    public void DeleteBtn()
    {
        DeleteUser(WhereField_Delete, WhereCondition_Delete);
    }

    // ȸ�� ����
    public void DeleteUser(string wF, string wC)
    {
        WWWForm form = new WWWForm();

        // AddField
        form.AddField("whereField", wF); // username, email, password �߿� �ϳ��� ���´�.
        form.AddField("whereCondition", wC); // ���� Condition���� �Ҵ�� ���� ���´�.

        WWW www = new WWW(URL, form);
    }
}
