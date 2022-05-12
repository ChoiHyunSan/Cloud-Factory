using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userUpdate : LogInSystemManager
{
    string URL = "http://localhost/mydb/userUpdate.php";

    public void UpdateBtn()
    {
        UpdateUser(InputUsername_Update, InputPassword_Update, InputNickName_Update
                , WhereField_Update, WhereCondition_Update);
    }

    public void UpdateUser(string username, string password, string nickname, string wF, string wC)
    {
        WWWForm form = new WWWForm();

        // AddField
        // ������ �̸�, �̸���, ��й�ȣ�� ���´�
        form.AddField("editUsername", username); 
        form.AddField("editPassword", password);
        form.AddField("editNickname", nickname);

        form.AddField("whereField", wF); // username, email, password �߿� �ϳ��� ���´�.
        form.AddField("whereCondition", wC); // ���� Condition���� �Ҵ�� ���� ���´�.

        WWW www = new WWW(URL, form);

        
    }
}
