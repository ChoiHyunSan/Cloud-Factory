using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class YardHandleSystem : MonoBehaviour
{
    public IngredientList[] mRarityList;

    private bool mIsAble; 
    struct Yard //���� ����ü ����
    {
        private GameObject self;

        private int gatherCnt;//unsigned int �� ��ĥ �� ������? ��ģ�ٸ� �ؿ� ���ǹ��� ������ �ٲ���
        private Sprite[] sprites;

        public void init(GameObject gameObj,Sprite[] _sprites) //�ʱ�ȭ �Լ�
        {
            self = gameObj;
            sprites = new Sprite[2];

            if (_sprites.Length != 2) return; //Overflow ����
            else
                Debug.Log("[Yard Init] Not Right Sprite array input");
            sprites[0] = _sprites[0];
            sprites[1] = _sprites[1];
        }

        public bool canGather() //ä�� ���� ���� 
        {
            if (gatherCnt <= 0) return false; //ä�� ���� Ƚ���� 0 ���� �̸� ���� Ƚ�� ��� ����.
            else return true;
        }

        public void gather() //ä�� �ϴ� ����
        {
            if (gatherCnt == 0) return; //�̹� ���� Ƚ���� ��� ���� �Ǹ� ä���� ������� �ʴ´�.





            gatherCnt--;
            updateSprite();

        }

        private void rndGatSys(GameObject GInventory) //random gather System
        {
            int invenLev = 1; //���߿� GetComponent�� ������ ����

        }

        private void activeBoard() //ä�� �˾� â Ȱ��ȭ
        {

        }

        private void updateSprite()
        {
            //gatherCnt�� ���� yardSprite �ٲ�
            if (gatherCnt == 0) self.GetComponent<SpriteRenderer>().sprite = sprites[0];
            else if (gatherCnt >= 0 && self.GetComponent<SpriteRenderer>().sprite != sprites[1])
                self.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    };
    // Start is called before the first frame update
    void Start()
    {
      //  mRarityList = new IngredientList[3]; //��͵� 1,2,3 ... 4(�� ���� �߰�)

        for (int i = 0; i < mRarityList.Length; i++)
            mRarityList[i].init();
 
    }

    
    public void  bGathered()
    {
        Dictionary<IngredientData, int> results = getRndGatheredResult();
        Debug.Log("[System]��" + results.Count + "�� ä���Ǿ����ϴ�!");
        foreach(KeyValuePair<IngredientData, int> result in results)
        {
            Debug.Log("[System]ä�� ����| ����:" + result.Key + "|����: "+result.Value);
            
        }
    }

    private Dictionary<IngredientData, int> getRndGatheredResult() //�������� ä���� ����Ʈ ����.
    {
        //Key: ��� ����  Value: ȹ���� ��� ����
        Dictionary<IngredientData, int> results= new Dictionary<IngredientData, int>();
         
        //��͵� ����, ���߿����� ���� ����.
        //Random: ��͵�, ��͵� �� ����, ��� ��, ä���� ��� ���� ��
        int cnt = 0;
      //  int total = Random.Range(3, 6); 
        int total = 3; 
        while (cnt <= total)
        {
            IngredientData tmp = mRarityList[getRndRarityType(1)].getRndIngredient();
            Debug.Log(tmp);
            if (results.ContainsKey(tmp)) continue; //�ߺ�����
            cnt++;
            results.Add(tmp, Random.Range(1, 6));
        }

        return results;
    }

   
   private int getRndRarityType(int _invenLv) //�Ű�����: �κ��丮 lv, �κ��丮 lv�� ���� � ��͵��� ��ᰡ ������ return
    {
        int randomValue= Random.Range(0,100);
        int rarity = 0;
        switch(_invenLv)
        {
            case 1: //100%�� Ȯ���� rarity = 1;
                rarity = 1;
                break;
            case 2:
                if (randomValue < 80) rarity = 1;
                else rarity = 2;
                break;
            case 3:
                if (randomValue < 60) rarity = 1;
                else if (60 <= randomValue && randomValue < 90) rarity = 2;
                else rarity = 3;
                break;
            default:
                break;
        }

        return rarity;
    }
}
