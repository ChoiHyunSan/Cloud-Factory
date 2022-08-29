using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ������ BGM�� �ν����Ϳ��� �Ҵ�
// �����̴��� ���� ����� �� ���� ���� ���� �� ����
public class SoundManager : MonoBehaviour
{   
    private SeasonDateCalc mSeason; // ����

    // BGM, ȿ���� ����� �ҽ�
    private AudioSource mBGM;
    private AudioSource mSFx;

    public AudioClip[] mSeasonBGMArray = new AudioClip[4]; // 4�������� �޶����� BGM
    public AudioClip[] mBGMArray = new AudioClip[2]; // [0] �׸� [1] ������
    public bool[] isOneTime = new bool[4]; // �뷡 �ѹ��� Ʋ��
    void Awake()
    {        
        // ����� �ҽ� ã��
        mBGM = GameObject.Find("mBGM").GetComponent<AudioSource>();
        mSFx = GameObject.Find("mSFx").GetComponent<AudioSource>();
                
        mSeason = GameObject.Find("Season Date Calc").GetComponent<SeasonDateCalc>();
    }
    // ����Ƽ �����ֱ� Ȱ��
    void Start()
    {
        if (SceneData.Instance) // null check
        {
            // ���� ����� �� ����� ������ ���� ������Ʈ
            mBGM.volume = SceneData.Instance.BGMValue;
            mSFx.volume = SceneData.Instance.SFxValue;
        }

        if ((SceneManager.GetActiveScene().name == "Lobby"))
        {
            mBGM.clip = mBGMArray[0];
            mBGM.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Drawing Room" )
        {
            mBGM.clip = mBGMArray[1];
            mBGM.Play();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Space Of Weather")
        { // ������ ���� ���� ���� �Ұ����� �ߺ� �÷��� ����
            switch (mSeason.mSeason)
            {
                case 1:
                    UpdateAudio(0);
                    break;
                case 2:
                    UpdateAudio(1);
                    break;
                case 3:
                    UpdateAudio(2);
                    break;
                case 4:
                    UpdateAudio(3);
                    break;
                default:
                    break;
            }           
        }
    }

    void UpdateAudio(int _iIndex)
    {
        if (!isOneTime[_iIndex])
        {
            mBGM.clip = mSeasonBGMArray[_iIndex];
            mBGM.Play();
            for (int i = 0; i < 4; i++)
            {
                if (_iIndex == i) continue;

                isOneTime[i] = false;
            }
            isOneTime[_iIndex] = true;
        }
    }

    // BGM ���� ����
    public void SetBGMVolume(float volume)
    {        
        mBGM.volume = volume;
        // ���� ������ �� ���� ����
        SceneData.Instance.BGMValue = volume;
    }

    // SFx ���� ����
    public void SetSFxVolume(float volume)
    {
        mSFx.volume = volume;
        // ���� ������ �� ���� ����
        SceneData.Instance.SFxValue = volume;
    }
}
