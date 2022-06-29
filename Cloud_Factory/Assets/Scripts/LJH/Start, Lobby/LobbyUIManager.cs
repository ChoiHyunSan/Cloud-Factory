using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Space Of Weather");
        mSFx.Play();
    }
    public void ContinueGame()
    {
        // json ������ ���� ���� �ҷ��ͼ� �� �̸� �Ҵ��ϱ�
        // SceneManager.LoadScene("");
        mSFx.Play();
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

