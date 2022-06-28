using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YardHandleSystem : MonoBehaviour
{
    public IngredientData[] ingredients;
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
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
