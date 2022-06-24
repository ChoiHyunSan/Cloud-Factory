using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ �� ���� ��� ��ũ��Ʈ
public class SeasonDateCalc : MonoBehaviour
{
    // SeasonDateCalc�� �ν��Ͻ��� ��� ���� ����
    private static SeasonDateCalc instance = null;

    // m = Ŭ���� ��� ���� ǥ��
    public float m_second; // ��, �ð�, 600��=10��=�Ϸ�
    public int   m_day; // �� (1~20��)
    public int   m_week; // �� (5�ϸ��� 1��, 4�ְ� �ִ�)
    public int   m_season; // ��, ���� (4�ָ��� 1��, ��,����,����,�ܿ� ������ 4��)
    public int   m_year; // �� (~)

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

    // SeasonDateCalc Instance�� ������ �� �ִ� ������Ƽ, �ٸ� Ŭ�������� ��밡��
    public static SeasonDateCalc Instance
    {
        get
        {
            if (null == instance) return null;

            return instance;
        }
    }

    void Update()
    {
        // �� ���
        m_second += Time.deltaTime;
        // �� ���
        // 20�� ����
        if (m_day > 20) m_day = 1;
        else m_day += dayCalc(ref m_second);
        // �� ���        
        m_week = weekCalc(ref m_day);
        // ��, ���� ���
        m_season += seasonCalc(ref m_week);
        // �� ���
        m_year += yearCalc(ref m_season);
    }

    // ref�� �����ؼ� ������ �ּ� �� ����
    int dayCalc(ref float second)
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
    int weekCalc(ref int day)
    {        
        // 0~4���� 1�ְ� ��������
        // ex) day�� 1���� ����, 5���̶��, 5-1 / 5 = 0 >> +1 >> 1����
        // 6-1 / 5 = 1 >> +1 >> 2����
        return ((day - 1) / 5) + 1;
    }
    int seasonCalc(ref int week)
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
    int yearCalc(ref int month)
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
