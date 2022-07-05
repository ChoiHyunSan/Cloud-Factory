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
    public GameObject   gOption;       // �ɼ� ���� ������Ʈ

    // ������
    public GameObject   gSpeechBubble; // ������ ��ȭâ
    public GameObject   gOkNoGroup;    // ������ ���� ���� �׷�

    [Header("TEXT")]
    public Text         tDate;         // ��¥ �ؽ�Ʈ
    public Text         tYear;         // ��¥ �ؽ�Ʈ
    [Space (3f)]
    public Text         tBgmValue;     // BGM ���� �ؽ�Ʈ
    public Text         tSfxValue;     // SFx ���� �ؽ�Ʈ

    [Header("SLIDER")]
    public Slider       sBGM;          // BGM �����̴�
    public Slider       sSFx;          // SFx �����̴�

    [Header("SPRITES")]
    public Sprite[]     sSeasons = new Sprite[4]; // �� ���� ���� �ܿ� �޷�

    [Header("IMAGES")]
    public Image        iSeasons;      // �޷� �̹���

    private AudioSource mSFx;          // ȿ���� ����� �ҽ� ����

    public bool mIsNext;              // ġ���� ��� ���� ������   
    public bool mIsPrev;              // ġ���� ��� ���� ������

    private ProfileMoving mProfile1; // ������ ������ ��� ��ũ��Ʈ
    private ProfileMoving mProfile2; // ������ ������ ��� ��ũ��Ʈ
    private ProfileMoving mProfile3; // ������ ������ ��� ��ũ��Ʈ

    void Awake()
    {
        mSFx = GameObject.Find("SFx").GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Record Of Healing")
        {
            mProfile1 = GameObject.Find("I_ProfileBG1").GetComponent<ProfileMoving>();
            mProfile2 = GameObject.Find("I_ProfileBG2").GetComponent<ProfileMoving>();
            mProfile3 = GameObject.Find("I_ProfileBG3").GetComponent<ProfileMoving>();
        }
    }

    void Update()
    {
        if (tDate && tYear && iSeasons)        // null check
        {
            tDate.text = SeasonDateCalc.Instance.mDay.ToString() + "��";
            tYear.text = SeasonDateCalc.Instance.mYear.ToString() + "����";

            if (SeasonDateCalc.Instance.mSeason == 1)
                iSeasons.sprite = sSeasons[0]; // ��
            else if (SeasonDateCalc.Instance.mSeason == 2)
                iSeasons.sprite = sSeasons[1]; // ����
            else if (SeasonDateCalc.Instance.mSeason == 3)
                iSeasons.sprite = sSeasons[2]; // ����
            else if (SeasonDateCalc.Instance.mSeason == 4)
                iSeasons.sprite = sSeasons[3]; // �ܿ�
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


    /*
     ����
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

    /*
     ������
     */

    public void ActiveOk()
    {
        gSpeechBubble.SetActive(true);
        gOkNoGroup.SetActive(false);

        // �������� �� �޼ҵ� ȣ��
    }

    public void ActiveNo()
    {
        gSpeechBubble.SetActive(true);
        gOkNoGroup.SetActive(false);

        // �������� �� �޼ҵ� ȣ��
    }

    /*
     ġ���� ���
     */

    public void ShowNextProfile()
    {
        mIsNext = true;
        mIsPrev = false;

        mProfile1.mIsMoving = true;
        mProfile2.mIsMoving = true;
        mProfile3.mIsMoving = true;
    }
    public void ShowPrevProfile()
    {
        mIsNext = false;
        mIsPrev = true;

        mProfile1.mIsMoving = true;
        mProfile2.mIsMoving = true;
        mProfile3.mIsMoving = true;
    }
    public void ShowUpsetMoongti()
    {

    }
    public void ShowAllMoongti()
    {

    }

}
