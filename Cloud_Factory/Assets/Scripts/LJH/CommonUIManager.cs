using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 공통 UI 담당
// 계절, 날짜 업데이트
// 씬 이동
public class CommonUIManager : MonoBehaviour
{
    [Header("GAME OBJECT")]
    // 오브젝트 Active 관리
    public GameObject   gOption;       // 옵션 게임 오브젝트
    public GameObject   gGuideBook;

    [Header("TEXT")]
    public Text         tDate;         // 날짜 텍스트
    public Text         tYear;         // 날짜 텍스트
    [Space (3f)]
    public Text         tBgmValue;     // BGM 볼륨 텍스트
    public Text         tSfxValue;     // SFx 볼륨 텍스트

    [Header("SLIDER")]
    public Slider       sBGM;          // BGM 슬라이더
    public Slider       sSFx;          // SFx 슬라이더

    [Header("SPRITES")]
    public Sprite[]     sSeasons = new Sprite[4]; // 봄 여름 가을 겨울 달력

    [Header("IMAGES")]
    public Image        iSeasons;      // 달력 이미지

    private AudioSource mSFx;          // 효과음 오디오 소스 예시
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Lobby" ||
            SceneManager.GetActiveScene().name == "Space Of Weather" ||
            SceneManager.GetActiveScene().name == "Drawing Room" ||
            SceneManager.GetActiveScene().name == "Cloud Factory")
            mSFx = GameObject.Find("mSFx").GetComponent<AudioSource>();        
    }

    void Update()
    {
        if (tDate && tYear && iSeasons && SeasonDateCalc.Instance) // null check
        {
            tDate.text = SeasonDateCalc.Instance.mDay.ToString() + "일";
            tYear.text = SeasonDateCalc.Instance.mYear.ToString() + "년차";

            if (SeasonDateCalc.Instance.mSeason == 1)
                iSeasons.sprite = sSeasons[0]; // 봄
            else if (SeasonDateCalc.Instance.mSeason == 2)
                iSeasons.sprite = sSeasons[1]; // 여름
            else if (SeasonDateCalc.Instance.mSeason == 3)
                iSeasons.sprite = sSeasons[2]; // 가을
            else if (SeasonDateCalc.Instance.mSeason == 4)
                iSeasons.sprite = sSeasons[3]; // 겨울
        }

        // 현재 음량으로 업데이트
        if (sBGM && sSFx && SceneData.Instance)           // null check
        {
            sBGM.value = SceneData.Instance.BGMValue;
            sSFx.value = SceneData.Instance.SFxValue;
        }
        if (tBgmValue && tSfxValue) // null check
        {
            // 소수점 -2 자리부터 반올림
            tBgmValue.text = Mathf.Ceil(sBGM.value * 100).ToString();
            tSfxValue.text = Mathf.Ceil(sSFx.value * 100).ToString();
        }
    }
    
    // 씬 이동 버튼들
    public void GoSpaceOfWeather()
    {        
        LoadingSceneController.Instance.LoadScene("Space Of Weather");
    }
    public void GoCloudFactory()
    {
        LoadingSceneController.Instance.LoadScene("Cloud Factory");
    }
    public void GoDrawingRoom()
    {
        LoadingSceneController.Instance.LoadScene("Drawing Room");
    }
    public void GoRecordOfHealing()
    {
        // 치유의 기록 이전 씬 인덱스 저장
        SceneData.Instance.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;

        LoadingSceneController.Instance.LoadScene("Record Of Healing");
    }
    public void GoPrevScene()
    {
        // 치유의 기록을 들어가기전 씬으로 이동   
        LoadingSceneController.Instance.LoadScene(SceneData.Instance.prevSceneIndex);
    }
    public void GoGiveCloud()
    {
        LoadingSceneController.Instance.LoadScene("Give Cloud");
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().go2CloudFacBtn();
    }
    public void GoCloudStorage()
    {
        LoadingSceneController.Instance.LoadScene("Cloud Storage");
    }

    // 옵션창 활성화, 비활성화
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
    public void ActiveGuideBook()
    {
        gGuideBook.SetActive(true);
    }
    public void UnActiveGuideBook()
    {
        gGuideBook.SetActive(false);
    }

    // 게임 종료
    public void QuitGame()
    {
        mSFx.Play();
        Application.Quit();
    }
}
