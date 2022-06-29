using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� UI ���
// ����, ��¥ ������Ʈ
// �� �̵�
public class CommonUIManager : MonoBehaviour
{
    [Header("GAME OBJECT")]
    // ������Ʈ Active ����
    public GameObject   gOption;    // �ɼ� ���� ������Ʈ

    [Header("TEXT")]
    // ȭ�鿡 ���̴� TEXT ������Ʈ
    public Text         tSeason;    // ���� �ؽ�Ʈ
    public Text         tdate;      // ��¥ �ؽ�Ʈ
    [Space (3f)]
    public Text         tBgmValue;  // BGM ���� �ؽ�Ʈ
    public Text         tSfxValue;  // SFx ���� �ؽ�Ʈ

    [Header("SLIDER")]
    public Slider       sBGM;       // BGM �����̴�
    public Slider       sSFx;       // SFx �����̴�

    private AudioSource mSFx;       // ȿ���� ����� �ҽ� ����

    void Awake()
    {
        mSFx = GameObject.Find("SFx").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (tSeason && tdate)       // null check
        {
            tdate.text = SeasonDateCalc.Instance.day.ToString() + "����";
            if (SeasonDateCalc.Instance.season == 1)
                tSeason.text = "��";
            else if (SeasonDateCalc.Instance.season == 2)
                tSeason.text = "����";
            else if (SeasonDateCalc.Instance.season == 3)
                tSeason.text = "����";
            else if (SeasonDateCalc.Instance.season == 4)
                tSeason.text = "�ܿ�";
        }

        // ���� �������� ������Ʈ
        if (sBGM && sSFx)           // null check
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

    public void GoSpaceOfWeather()
    {
        SceneManager.LoadScene("Space Of Weather");
    }
    public void GoCloudFactory()
    {
        SceneManager.LoadScene("Cloud Factory");
    }
    public void GoDrawingRoom()
    {
        SceneManager.LoadScene("Drawing Room");
    }
    public void GoRecordOfHealing()
    {
        // ġ���� ��� ���� �� �ε��� ����
        SceneData.Instance.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene("Record Of Healing");
    }
    public void GoPrevScene()
    {
        // ġ���� ����� ������ ������ �̵�   
        SceneManager.LoadScene(SceneData.Instance.prevSceneIndex);
    }

    // �ɼ�â Ȱ��ȭ, ��Ȱ��ȭ
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
}
