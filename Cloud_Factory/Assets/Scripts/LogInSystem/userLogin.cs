using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userLogin : LogInSystemManager
{
	public InputField inputUserName, inputPassword;
	public Text loginGuideText;

	private userSelect userSelect_findData;

	string LoginURL = "http://localhost/mydb/login.php";

    void Awake()
    {
		userSelect_findData = GetComponent<userSelect>();

	}

    public void LoginBtn()
    {
		StartCoroutine(LoginToDB(inputUserName.text, inputPassword.text));
	}

	IEnumerator LoginToDB(string username, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("usernamePost", username);
		form.AddField("passwordPost", password);

		WWW www = new WWW(LoginURL, form);

		yield return www;

		// ���̵� ��ϵǾ� �ִٸ�
		if (userSelect_findData.FindUserData(username) != -1)
        {
			// ��й�ȣ���� �´ٸ� �α��� ����!
			if (password == userSelect_findData.GetUserPassword(userSelect_findData.FindUserData(username)))
            {
				loginGuideText.text =
				userSelect_findData.GetUserNickname(userSelect_findData.FindUserData(username))
				+ "�� �ȳ��ϼ���!";
			}
			else
            {
				loginGuideText.text = "��й�ȣ�� Ʋ�Ƚ��ϴ�.";
			}
		}
        else // �ƴ϶��
        {
			loginGuideText.text = "��ϵ� ȸ���� �ƴմϴ�.";
        }

		Invoke("initText", 5.0f);
	}

	void initText()
    {
		loginGuideText.text = "###Cloud Factory###";
		// �ߺ����� ����
		CancelInvoke("initText");
	}
}
