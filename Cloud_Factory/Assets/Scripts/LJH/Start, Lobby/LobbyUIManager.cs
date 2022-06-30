using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using Newtonsoft.Json;

// �κ� �� UI ���
// ���� â, �����ϱ�, �̾��ϱ�
public class LobbyUIManager : MonoBehaviour
{
    [Header("GAME OBJECT")]
    // ������Ʈ Active ����
    public GameObject   gOption; // �ɼ� ���� ������Ʈ

    [Header("TEXT")]
    public Text     tBgmValue;   // BGM ���� �ؽ�Ʈ
    public Text     tSfxValue;   // SFx ���� �ؽ�Ʈ

    [Header("SLIDER")]
    public Slider   sBGM;        // BGM �����̴�
    public Slider   sSFx;        // SFx �����̴�

    private AudioSource mSFx;    // ȿ���� ����� �ҽ�

    void Awake()
    {
        mSFx = GameObject.Find("SFx").GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���� �������� ������Ʈ
        if (sBGM && sSFx) // null check
        {
            sBGM.value = SceneData.Instance.BGMValue;
            sSFx.value = SceneData.Instance.SFxValue;
        }
        if (tBgmValue && tSfxValue) // null check
        {
            // �Ҽ��� -2 �ڸ����� �ݿø�
            tBgmValue.text = Mathf.Ceil(sBGM.value * 100).ToString();
            tSfxValue.text = Mathf.Ceil(sSFx.value * 100).ToString();
        }
    }

    /*
     * BUTTON�� �Ҵ��� �޼ҵ�
     */

    public void NewGame()
    {        
        mSFx.Play();

        SceneManager.LoadScene("Space Of Weather");

        // �����͸� �ʱ�ȭ ��Ű�� �Լ� ȣ���ϱ�.
        // ����� �� �̵��� �����ϱ� ������ �׳� ������ �������� �̵��ϸ� �ȴ�.
    }
    public void ContinueGame()
    {
        mSFx.Play();

        // �ε��ϴ� �Լ� ȣ�� �Ŀ� �� �� �ε����� �̵�
        FileStream fSceneBuildIndexStream 
            // �ش� ��ο� �ִ� json ������ ����
            = new FileStream(Application.dataPath + "/Data/SceneBuildIndex.json", FileMode.Open);
        // �����ִ� json ������ byte�迭�� �ִ´�
        byte[] bData = new byte[fSceneBuildIndexStream.Length];
        // ������ �д´�
        fSceneBuildIndexStream.Read(bData, 0, bData.Length);
        fSceneBuildIndexStream.Close();
        // ���ڿ��� ��ȯ�Ѵ�
        string sData = Encoding.UTF8.GetString(bData);

        // ���ڿ��� int������ �Ľ��ؼ� ���� �ε����� Ȱ���Ѵ�
        SceneManager.LoadScene(int.Parse(sData));
    }

    public void ActiveOption()
    {
        mSFx.Play();
        gOption.SetActive(true);
    }
    public void UnActiveOption()
    {
        mSFx.Play();
        gOption.SetActive(false);
    }

    // ���� ����
    public void QuitGame()
    {
        mSFx.Play();
        Application.Quit();
    }

    public void GoCredit()
    {
        // ũ���� ȭ������ ��ȯ
    }
}

