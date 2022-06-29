using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ �� ���� ��� ��ũ��Ʈ
public class SeasonDateCalc : MonoBehaviour
{
    // SeasonDateCalc�� �ν��Ͻ��� ��� ���� ����
    private static SeasonDateCalc instance = null;    
    // SeasonDateCalc Instance�� ������ �� �ִ� ������Ƽ, �ٸ� Ŭ�������� ��밡��
    public  static SeasonDateCalc Instance
    {
        get
        {
            if (null == instance) return null;

            return instance;
        }
    }

    [SerializeField]
    private float    mSecond; // ��, �ð�, 600��=10��=�Ϸ�
    [SerializeField]
    private int      mDay;    // �� (1~20��)
    public  float    day 
    {
        get { return mDay; }
    }
    [SerializeField]
    private int      mWeek;   // �� (5�ϸ��� 1��, 4�ְ� �ִ�)
    [SerializeField]
    private int      mSeason; // ��, ���� (4�ָ��� 1��, ��,����,����,�ܿ� ������ 4��)
    public  float    season 
    {
        get { return mSeason; }
    }
    private int      mYear;   // �� (~)
    public  float    year
    {
        get { return mYear; }
    }

    void Awake()
    {
        // �ν��Ͻ� �Ҵ�
        if (null == instance)
        {
            instance = this;
            // ��� ������ ��¥ ����ؾ��ϹǷ�
            // ��, title�������� �����Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // �̹� �����ϸ� �������� ����ϴ� ���� �����
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // �� ���
        mSecond += Time.deltaTime;
        // �� ���
        // 20�� ����
        if (mDay > 20) mDay = 1;
        else mDay += CalcDay(ref mSecond);
        // �� ���        
        mWeek = CalcWeek(ref mDay);
        // ��, ���� ���
        mSeason += CalcSeason(ref mWeek);
        // �� ���
        mYear += CalcYear(ref mSeason);
    }

    // ref�� �����ؼ� ������ �ּ� �� ����
    int CalcDay(ref float second)
    {
        int temp = 0;
        // 10�д� 1��, 600�ʴ� 1�� �߰�
        if (second >= 600.0f)
        {
            temp += 1;
            second = 0;
        }
        return temp;
    }
    int CalcWeek(ref int day)
    {        
        // 0~4���� 1�ְ� ��������
        // ex) day�� 1���� ����, 5���̶��, 5-1 / 5 = 0 >> +1 >> 1����
        // 6-1 / 5 = 1 >> +1 >> 2����
        return ((day - 1) / 5) + 1;
    }
    int CalcSeason(ref int week)
    {
        int temp = 0;
        // 4�ְ� �ִ�, 5�������ʹ� ����
        if (week > 4)
        {
            temp += 1;
            week = 1;
        }
        return temp;
    }
    int CalcYear(ref int month)
    {
        int temp = 0;
        if (month > 4)
        {
            temp += 1;
            month = 1;
        }
        return temp;
    }
}
