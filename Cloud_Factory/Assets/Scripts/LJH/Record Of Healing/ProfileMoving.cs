using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ BG ���� ��ġ
// 1 : -575 -130 0
// 2 : -525 -80  0 
// 3 : -475 -30  0
// TEMP --> �� : -425 20 0
// TEMP --> �� : -625 -180 0

// 1,2,3 ��ġ�� �ִ� ��� ���� ����
// ������ ��ġ ����

// �ʱ� �ε���
// 0���� 4���� ������� �տ��� �ڷ�

// �ν��Ͻ�ȭ�ؼ� ĵ������ �ֱ�
// ���� ��ġ���� x -50 y -60 ����(if������ rect~�ɾ) ������ �ӵ��� Translate (����)
// �� �ڿ� �ν��Ͻ��ϳ� ����� �� �տ� �ִ� ���� �� ��ġ���� �����ϸ� ����

// ���� ��ġ���� x +50 y +60 ����(if������ rect~�ɾ) ������ �ӵ��� Translate (����)
// �� �տ� �ν��Ͻ��ϳ� ����� �� �ڿ� �ִ� ���� �� ��ġ���� �����ϸ� ����

// ��� ����� ��ġ������ ���� �����ϱ�


// ����ó���� ��
// ���������� �����ٰ� �ٷ� ���������������� �߰��� �ִ� ���� ���;ߴ�

// �ú��ε���
// 2 (���� ��) 1 (�߰�) 0 (���� ��)

public class ProfileMoving : MonoBehaviour
{
    private CommonUIManager mUIManager;

    // UI�� �̵��� RectTransform�̴�
    private RectTransform rProflieBG;
    public RectTransform rParentProflie;
    public Vector2 vOriginalPos;
    public Vector2 vNextPos;
    public Vector2 vPrevPos;

    private Image iProfile;

    private float mMoveSpeed;

    [HideInInspector]
    public bool isMoving;

    void Awake()
    {
        Debug.Log(gameObject.name + " : "+ transform.GetSiblingIndex());
        mMoveSpeed = 0.25f;
        mUIManager = GameObject.Find("UIManager").GetComponent<CommonUIManager>();
        iProfile = GetComponent<Image>();

        // rectTransform ������
        rProflieBG = GetComponent<RectTransform>();
        rParentProflie = this.gameObject.transform.parent.GetComponent<RectTransform>();
        InitPosValue();
        isMoving = true;
    }

    void Update()
    {
        if (mUIManager) // null check
        {
            if (isMoving)
            {
                // ���� ������ ��ư�� ������
                // ���� rect ��Ŀ �������� ���� ��ġ���� �����ϵ���
                if (mUIManager.mIsNext && (rProflieBG.anchoredPosition.x > vNextPos.x
                                       && rProflieBG.anchoredPosition.y > vNextPos.y))
                {
                    this.gameObject.transform.
                        Translate(new Vector2(-mMoveSpeed * Time.timeScale, -mMoveSpeed * Time.timeScale));
                }
                else if (mUIManager.mIsNext && rProflieBG.anchoredPosition.x <= vNextPos.x
                      && rProflieBG.anchoredPosition.y <= vNextPos.y)
                {
                    InitPosValue();
                }

                // �ݴ��
                if (mUIManager.mIsPrev && (rProflieBG.anchoredPosition.x < vPrevPos.x
                                           && rProflieBG.anchoredPosition.y < vPrevPos.y))
                {
                    this.gameObject.transform.
                        Translate(new Vector2(mMoveSpeed * Time.timeScale, mMoveSpeed * Time.timeScale));
                }
                else if (mUIManager.mIsPrev && rProflieBG.anchoredPosition.x >= vPrevPos.x
                     && rProflieBG.anchoredPosition.y >= vPrevPos.y)
                {
                    InitPosValue();
                }

                // TEMP�� �տ� ���� ��
                if (rProflieBG.anchoredPosition.x <= -625
                                       && rProflieBG.anchoredPosition.y <= -180)
                {
                    rProflieBG.anchoredPosition = new Vector2(-475, -30);
                    //if (this.gameObject.transform.parent.GetSiblingIndex() == 2)
                    //{
                    //    this.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("isLastPage");
                    //    //Invoke("Delay", 0.5f);
                    //}

                    InitPosValue();
                }
                // TMEP�� �ڿ� ���� ��
                else if (rProflieBG.anchoredPosition.x >= -425
                                          && rProflieBG.anchoredPosition.y >= 20)
                {
                    rProflieBG.anchoredPosition = new Vector2(-575, -130);
                    InitPosValue();
                }
            }

            // ���� �տ� ���� ��
            if (rProflieBG.anchoredPosition.x == -575 && rProflieBG.anchoredPosition.y == -130)
            {
                this.gameObject.transform.parent.SetSiblingIndex(2);
            }
            // �߰�
            else if (rProflieBG.anchoredPosition.x == -525 && rProflieBG.anchoredPosition.y == -80)
            {
                this.gameObject.transform.parent.SetSiblingIndex(1);
            }
            // ���� �ڿ� ���� ��
            else if (rProflieBG.anchoredPosition.x == -475 && rProflieBG.anchoredPosition.y == -30)
            {
                this.gameObject.transform.parent.SetSiblingIndex(0);
            }            
        }
    }

    void Delay()
    {
        rProflieBG.anchoredPosition = new Vector2(-475, -30);
        rParentProflie.anchoredPosition = new Vector2(0, 0);
    }

    void InitPosValue()
    {
        vOriginalPos = rProflieBG.anchoredPosition;
        vNextPos = new Vector2(vOriginalPos.x - 50, vOriginalPos.y - 50);
        vPrevPos = new Vector2(vOriginalPos.x + 50, vOriginalPos.y + 50);

        isMoving = false;
    }
}
