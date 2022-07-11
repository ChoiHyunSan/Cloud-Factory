using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{   //PLEASURE���� 0~ �� ���� ����
    PLEASURE, //���
    UNREST, //�Ҿ�
    SADNESS, //����
    IRRITATION, //¥��
    ACCEPT,//����
    SUPCON, //SUPRISE+CONFUSION ���,ȥ��
    DISGUST, //����
    INTEXPEC, //INTERSTING+EXPECTATION ����,���
    LOVE,
    ROMANCE, //������ȭ�� ROMANCE COMICS���� PURE LOVE���� ���� �� ���Ƽ� �̷��� ��.
    AWE,
    CONTRAY,//�ݴ�
    BLAME,
    DESPISE,
    AGGRESS,//AGGRESSION ���ݼ�
    OPTIMISM,//��õ
    BITTER,
    LOVHAT, //LOVE AND HATRED
    FREEZE,
    CHAOTIC//ȥ��������
}

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData", order = 1)]
public class IngredientData : ScriptableObject
{
    

    [System.Serializable]
    public struct emotioninfo
    {
        [SerializeField]
        private Emotion Key;
        [SerializeField]
        private int Value;

        void init(Emotion _Key, int _Value)
        {
            Key = _Key;
            Value = _Value;
        }

        public int getKey2Int()
        {
            return (int)Key; //Emotion Enum �� ������(index��)���� ��ȯ�ؼ� ����
        }

        public int getValue()
        {
            return Value;
        }
    }

    public string ingredientName; //��� �̸�

    public Sprite image;// �̹���

    //��͵� : ��͵��� ���� ������ ���� ���� �� ������ �޶�����.
    [SerializeField]
    private int rarity;

    [SerializeField]
    private emotioninfo[] emotions;

    public Dictionary<int, int> iEmotion;

   


    public void init()
    {
        iEmotion = new Dictionary<int, int>();
        foreach (emotioninfo emotion in emotions)
        {
            iEmotion.Add(emotion.getKey2Int(), emotion.getValue());
        }
    }
}