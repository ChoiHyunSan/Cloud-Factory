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

    // ���� ��ư Ŭ�� ��
    public void OpenGuideGather()
    {
        mGuideGather.SetActive(true);
    }
    // ������
    public void CloseGuideGather()
    {
        mGuideGather.SetActive(false);
    }
    // ä���ϱ�
    public void GoingToGather()
    {
        mGuideGather.SetActive(false);
        mGathering.SetActive(true);
        mGatheringTextCount = 0;
        tGatheringText.text = "��� ä�� ��";

        if (SeasonDateCalc.Instance) // null check
        {                            // �� �ش��ϴ� �ִϸ��̼� ���
            Invoke("PrintGatheringText", 0.5f); // 0.5�� �����̸��� . �߰�
            if (SeasonDateCalc.Instance.mSeason == 1) // ���̶��
            {                
                mGatheringAnim.SetBool("Spring", true);
                mGatheringAnim.SetBool("Summer", false);
            }
            else if (SeasonDateCalc.Instance.mSeason == 2) // �����̶��
            {
                mGatheringAnim.SetBool("Spring", false);
                mGatheringAnim.SetBool("Summer", true);
            }
            else if (SeasonDateCalc.Instance.mSeason == 3) // �����̶��
            {

            }
            else if (SeasonDateCalc.Instance.mSeason == 4) // �ܿ��̶��
            {

            }
        }
        // 5�� ���� ������ �� ��� ���
        Invoke("Gathering", 5.0f);
    }
    void Gathering()
    {
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
            Invoke("PrintGatheringText", 0.5f); // 0.5�� �����̸��� . �߰�
        }
        else // �ʱ�ȭ
        {
            mGatheringTextCount = 0;
            tGatheringText.text = "��� ä�� ��";
            Invoke("PrintGatheringText", 0.5f); // 0.5�� �����̸��� . �߰�
        }
    }
    
    public void CloseResultGather()
    {
        mGatherResult.SetActive(false);        
    }
}
