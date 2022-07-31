using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUIManager : MonoBehaviour
{
    public GameObject mGuideGather; // ä���Ұ��� ���Ұ��� �˷��ִ� UI
    public GameObject mGathering;   // ä�� �� ����ϴ� UI
    public GameObject mGatherResult;// ä�� ����� ����ϴ� UI

    public Animator mGatheringAnim; // ä�� �ִϸ��̼�

    public Text tGatheringText;      // ä�� ��... �ؽ�Ʈ
    private int mGatheringTextCount; // ä�� �� '.' ��� ����

    public RectTransform mGatherImageRect; // ä�� �̹��� Rect Transform

    public RectTransform[] mFxShine       = new RectTransform[5]; // 5���� ä�� ��� ȸ�� ȿ��
    public RectTransform[] mGatherRect    = new RectTransform[5]; // 5���� ä�� ��� UI �̵�
    public GameObject[]    mGatherObj     = new GameObject[5]; // 5���� ä�� ���� ������Ʈ

    public int mRandomGather; // ��� ä�� ���� ����

    void Update()
    {
        if (mGatherResult.activeSelf)
        {
            // ä�� ��� ȿ��
            mFxShine[0].Rotate(0, 0, 25.0f * Time.deltaTime, 0);
            mFxShine[1].Rotate(0, 0, 25.0f * Time.deltaTime, 0);
            mFxShine[2].Rotate(0, 0, 25.0f * Time.deltaTime, 0);
            mFxShine[3].Rotate(0, 0, 25.0f * Time.deltaTime, 0);
            mFxShine[4].Rotate(0, 0, 25.0f * Time.deltaTime, 0);
        }
    }
    // ���� ��ư Ŭ�� ��, ä���Ͻðھ��ϱ�? ������Ʈ Ȱ��ȭ    
    public void OpenGuideGather()
    {
        mGuideGather.SetActive(true);
    }
    // ������, ä���Ͻðھ��ϱ�? ������Ʈ ��Ȱ��ȭ    
    public void CloseGuideGather()
    {
        mGuideGather.SetActive(false);
    }
    // ä���ϱ�
    public void GoingToGather()
    {
        mGuideGather.SetActive(false);
        mGathering.SetActive(true);
        mGatheringTextCount = 0; // �ʱ�ȭ
        tGatheringText.text = "��� ä�� ��"; // �ʱ�ȭ

        if (SeasonDateCalc.Instance) // null check
        {                            // �� �ش��ϴ� �ִϸ��̼� ���
            Invoke("PrintGatheringText", 0.5f); // 0.5�� �����̸��� . �߰�
            if (SeasonDateCalc.Instance.mSeason == 1) // ���̶��
            {
                mGatherImageRect.sizeDelta = new Vector2(1090, 590); // �̹��� ������ ���߱�
                
                mGatheringAnim.SetBool("Spring", true);
                mGatheringAnim.SetBool("Summer", false);
                mGatheringAnim.SetBool("Fall", false);
                mGatheringAnim.SetBool("Winter", false);
            }
            else if (SeasonDateCalc.Instance.mSeason == 2) // �����̶��
            {
                mGatherImageRect.sizeDelta = new Vector2(1090, 590); // �̹��� ������ ���߱�

                mGatheringAnim.SetBool("Spring", false);
                mGatheringAnim.SetBool("Summer", true);
                mGatheringAnim.SetBool("Fall", false);
                mGatheringAnim.SetBool("Winter", false);
            }
            else if (SeasonDateCalc.Instance.mSeason == 3) // �����̶��
            {
                mGatherImageRect.sizeDelta = new Vector2(735, 420); // �̹��� ������ ���߱�

                mGatheringAnim.SetBool("Spring", false);
                mGatheringAnim.SetBool("Summer", false);
                mGatheringAnim.SetBool("Fall", true);
                mGatheringAnim.SetBool("Winter", false);
            }
            else if (SeasonDateCalc.Instance.mSeason == 4) // �ܿ��̶��
            {
                mGatherImageRect.sizeDelta = new Vector2(560, 570); // �̹��� ������ ���߱�

                mGatheringAnim.SetBool("Spring", false);
                mGatheringAnim.SetBool("Summer", false);
                mGatheringAnim.SetBool("Fall", false);
                mGatheringAnim.SetBool("Winter", true);
            }
        }
        // 5�� ���� ä�� �� ��� ���
        Invoke("Gathering", 5.0f);
    }
    void Gathering()
    {
        // ���� �۾�
        mRandomGather = Random.Range(0, 5); // 0~4
        
        if (mRandomGather % 2 == 1) // Ȧ��
        {
            mGatherRect[0].anchoredPosition = new Vector3(125.0f, 0.0f, 0.0f);
            mGatherRect[1].anchoredPosition = new Vector3(-125.0f, 0.0f, 0.0f);
            mGatherRect[2].anchoredPosition = new Vector3(375.0f, 0.0f, 0.0f);
            mGatherRect[3].anchoredPosition = new Vector3(-375.0f, 0.0f, 0.0f);
        }
        else
        {
            mGatherRect[0].anchoredPosition = new Vector3(0, 0.0f, 0.0f);
            mGatherRect[1].anchoredPosition = new Vector3(-225.0f, 0.0f, 0.0f);
            mGatherRect[2].anchoredPosition = new Vector3(225.0f, 0.0f, 0.0f);
            mGatherRect[3].anchoredPosition = new Vector3(-450.0f, 0.0f, 0.0f);
        }

        switch (mRandomGather) // active ����
        {
            case 0:
                mGatherObj[0].SetActive(true);
                mGatherObj[1].SetActive(false);
                mGatherObj[2].SetActive(false);
                mGatherObj[3].SetActive(false);
                mGatherObj[4].SetActive(false);
                break;
            case 1:
                mGatherObj[0].SetActive(true);
                mGatherObj[1].SetActive(true);
                mGatherObj[2].SetActive(false);
                mGatherObj[3].SetActive(false);
                mGatherObj[4].SetActive(false);
                break;
            case 2:
                mGatherObj[0].SetActive(true);
                mGatherObj[1].SetActive(true);
                mGatherObj[2].SetActive(true);
                mGatherObj[3].SetActive(false);
                mGatherObj[4].SetActive(false);
                break;
            case 3:
                mGatherObj[0].SetActive(true);
                mGatherObj[1].SetActive(true);
                mGatherObj[2].SetActive(true);
                mGatherObj[3].SetActive(true);
                mGatherObj[4].SetActive(false);
                break;
            case 4:
                mGatherObj[0].SetActive(true);
                mGatherObj[1].SetActive(true);
                mGatherObj[2].SetActive(true);
                mGatherObj[3].SetActive(true);
                mGatherObj[4].SetActive(true);
                break;
            default:
                break;
        }


        mGathering.SetActive(false);
        mGatherResult.SetActive(true);

        CancelInvoke(); // �κ�ũ �浹 ������ ���ؼ� ��� ����� ������ ��� �κ�ũ ������
    }
    // ����Լ��� ��ħǥ�� ��������� ����Ѵ�
    void PrintGatheringText()
    {
        mGatheringTextCount++;
        tGatheringText.text = tGatheringText.text + ".";

        if (mGatheringTextCount <= 3)
        {
            Invoke("PrintGatheringText", 0.25f); // 0.25�� �����̸��� . �߰�
        }
        else // �ʱ�ȭ
        {
            mGatheringTextCount = 0;
            tGatheringText.text = "��� ä�� ��";
            Invoke("PrintGatheringText", 0.25f); // 0.25�� �����̸��� . �߰�
        }
    }
    // ä�� ��!
    public void CloseResultGather()
    {
        mGatherResult.SetActive(false);        
    }
}
