using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData", order = 1)]
public class IngredientData : ScriptableObject
{
    public enum Emotion
    {   //PLEASURE���� 0~ �� ���� ����
        PLEASURE,
        UNREST,
        SADNESS,
        IRRITATION,
        ACCEPT,
        SUPCON, //SUPRISE+CONFUSION
        DISGUST,
        INTEXPEC, //INTERSTING+EXPECTATION
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
    private Dictionary<int, int> iEmotion;

    public emotioninfo[] emotions;


    public void init()
    {
        iEmotion = new Dictionary<int, int>();
        foreach (emotioninfo emotion in emotions)
        {
            iEmotion.Add(emotion.getKey2Int(), emotion.getValue());
        }
    }
}