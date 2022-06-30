using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SaveUnitManager : MonoBehaviour
{
    // SaveUnitManager �ν��Ͻ��� ��� ���� ����
    private static SaveUnitManager instance = null;
        
    // ��� ���� �־� ���� ���̱� ������ �ߺ��� �ı�ó��
    // ��� ������ ����ǰ� �ε�� ������ �𸣱� ������
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);            
        }
    }

    // Awake->OnEnable->Start������ �����ֱ�
    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        // �� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    // ���� ����� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded: " + scene.name + " | SceneBuildIndex: " + scene.buildIndex);

        //================================================================================//
        //==================================���� �� ����==================================//
        //================================================================================//

        // ���Ӱ� �ε��� ���� �����͸� �����Ѵ�
        SceneData.Instance.currentSceneIndex = scene.buildIndex;

        // �����ϴ� �Լ� ȣ��
        // �ϴ��� �ϳ��ϱ� �̷��� �ְ� �������� Ŭ���� ���� �����ϱ�
        FileStream fSceneBuildIndexStream
            // ���� ��� + ���� ���� ���� ��ο� json ���� / ���� SAVE
            = new FileStream(Application.dataPath + "/Data/SceneBuildIndex.json", FileMode.OpenOrCreate);

        // sData�� ������ ����ȭ�Ѵ�        
        string sSceneData = JsonConvert.SerializeObject(SceneData.Instance.currentSceneIndex);

        // text �����ͷ� ���ڵ��Ѵ�
        byte[] bSceneData = Encoding.UTF8.GetBytes(sSceneData);

        // text �����͸� �ۼ��Ѵ�
        fSceneBuildIndexStream.Write(bSceneData, 0, bSceneData.Length);
        fSceneBuildIndexStream.Close();

        //================================================================================//
        //=================================��¥ ���� ����=================================//
        //================================================================================//

        //SeasonDateDataBox.Instance.mSecond = SeasonDateDataBox.Instance.mSecond;
        //SeasonDateDataBox.Instance.mDay    = SeasonDateDataBox.Instance.mDay;
        //SeasonDateDataBox.Instance.mWeek   = SeasonDateDataBox.Instance.mWeek;
        //SeasonDateDataBox.Instance.mSeason = SeasonDateDataBox.Instance.mSeason;
        //SeasonDateDataBox.Instance.mYear   = SeasonDateDataBox.Instance.mYear;

        FileStream fSeasonDateStream
            = new FileStream(Application.dataPath + "/Data/SeasonDate.json", FileMode.OpenOrCreate);

        //SeasonDateDataBox seasonDate = new SeasonDateDataBox();
        SeasonDateCalc seasonDate = this.gameObject.AddComponent<SeasonDateCalc>();

        // Class�� Json���� �ѱ�� self ���� �ݺ��� �Ͼ�� ������
        // �ܺζ��̺귯���� �����ϰ� ����Ƽ Utility�� Ȱ���Ѵ�.

        // Ŭ������ �ɹ��������� json���Ϸ� ��ȯ�Ѵ� (class, prettyPrint) true�� �б� ���� ���·� ��������
        string sSeasonData = JsonUtility.ToJson(seasonDate, true);
        Debug.Log(sSeasonData);

        // ���ڵ�
        byte[] bSeasonData = Encoding.UTF8.GetBytes(sSeasonData);

        // �ۼ�
        fSeasonDateStream.Write(bSeasonData, 0, bSeasonData.Length);
        fSeasonDateStream.Close();
    }

    // ����� ��
    void OnDisable()
    {
        // ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
