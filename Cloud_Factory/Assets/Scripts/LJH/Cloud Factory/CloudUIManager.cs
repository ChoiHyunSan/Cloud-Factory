using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum ECheckBox // üũ�ڽ� �ε���
{
    Date,
    Number,
    Emotion
}

public class CloudUIManager : MonoBehaviour
{    
    public Sprite       mDropBoxDown; // ȭ��ǥ �Ʒ�
    public Sprite       mDropBoxUp;   // ȭ��ǥ ��

    public Image        mArrow;       // ��� �ڽ� ȭ��ǥ

    public GameObject   mTemplate;    // ��� �ڽ� ����
    public GameObject[] mGiveCloudCheckBox = new GameObject[3]; // ���� ����ȭ�� üũ �ڽ� 3��

    public TMP_Dropdown mSortDropBox; // ��ӹڽ�

    void Update()
    {
        // ȭ��ǥ �ø�
        if (mTemplate.activeSelf) mArrow.sprite = mDropBoxUp;
        else if (!mTemplate.activeSelf) mArrow.sprite = mDropBoxDown;
    }

    // ���� �Ⱓ ���� ����
    public void SortStorageDate()
    {
        Debug.Log("���� �Ⱓ���� ����");

        // �������� ���� + �ٸ� ����� ����ϴ� ���
        if (!mGiveCloudCheckBox[(int)ECheckBox.Emotion].activeSelf)
        {
            mSortDropBox.interactable = false;
            mGiveCloudCheckBox[(int)ECheckBox.Date].SetActive(true);
        }
        else
            ToggleCheckBox(mGiveCloudCheckBox[(int)ECheckBox.Date]);

        mGiveCloudCheckBox[(int)ECheckBox.Number].SetActive(false);
    }
    // ���� ���� ����
    public void SortNumber()
    {
        Debug.Log("���� ���� ����");

        // �������� ���� + �ٸ� ����� ����ϴ� ���
        if (!mGiveCloudCheckBox[(int)ECheckBox.Emotion].activeSelf)
        {
            mSortDropBox.interactable = false;
            mGiveCloudCheckBox[(int)ECheckBox.Number].SetActive(true);
        }
        else
            ToggleCheckBox(mGiveCloudCheckBox[(int)ECheckBox.Number]);

        mGiveCloudCheckBox[(int)ECheckBox.Date].SetActive(false);
    }
    // �������� ����
    public void SortEmotion()
    {
        Debug.Log("�������� ����");
        if (!mGiveCloudCheckBox[(int)ECheckBox.Date].activeSelf
         && !mGiveCloudCheckBox[(int)ECheckBox.Number].activeSelf)
        {
            // �� ������ �ȵ�
        }
        else
        {
            ToggleDropBox(mSortDropBox);
            ToggleCheckBox(mGiveCloudCheckBox[(int)ECheckBox.Emotion]);
        }            
    }

    // üũǥ�� ���
    void ToggleCheckBox(GameObject CheckBox)
    {
        if (!CheckBox.activeSelf)
            CheckBox.SetActive(true);
        else
            CheckBox.SetActive(false);
    }
    // ��ӹڽ� Ȱ��ȭ ���
    void ToggleDropBox(TMP_Dropdown DropDown)
    {
        if (!DropDown.interactable)
            DropDown.interactable = true;
        else
            DropDown.interactable = false;
    }
    // ���ư��� ��ư
    public void GoToCloudFactory()
    {
        SceneManager.LoadScene("Cloud Factory");
    }
}
