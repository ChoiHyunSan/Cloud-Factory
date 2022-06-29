using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class YardHandleSystem : MonoBehaviour
{
    public IngredientList[] Lrarity;

    private bool isAble;
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
    void Awake()
    {
        Lrarity = new IngredientList[3]; //��͵� 1,2,3 ... 4(�� ���� �߰�)

        for (int i = 0; i < Lrarity.Length; i++)
            Lrarity[i].init();
 
    }

    private List<IngredientData> getRndGatheredResult() //�������� ä���� ����Ʈ 3�� ����.
    {
        List<IngredientData> result = new List<IngredientData>();
        //��͵� ����, ���߿����� ���� ����.
        int cnt = 0;
        while(true)
        {
            if (cnt >= 3) break;
            IngredientData tmp = Lrarity[getRndRarityType(1)].getRndIngredient();
            if (result.Contains(tmp)) continue; //�ߺ�����
            cnt++;
            result.Add(tmp);
        }

        return result;
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
