using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ��ġ ����� Ȥ�� �� ������ �����ϰ��ϱ� ���ؼ�,, �� ���� ���̵� ���������� ����,,
namespace ConstantsVector
{
    public enum EFrontToBack // �� �տ��� �� �ڷ� ���� ��ġ
    {
        X = -625,
        Y = -180
    }
    public enum EBackToFront // �� �ڿ��� �� ������ ���� ��ġ
    {
        X = -425,
        Y = 20
    }
    public enum EFirstPos    // ���� �տ��� ���̴� ��ġ
    {
        X = -575,
        Y = -130
    }
    public enum ESecondPos   // �߰����� ���̴� ��ġ
    {
        X = -525,
        Y = -80
    }
    public enum EThirdPos    // ���������� ���̴� ��ġ
    {
        X = -475,
        Y = -30
    }
}

public class ProfileMoving : MonoBehaviour
{    
    private RecordUIManager mUIManager;   // UI Manager ��ũ��Ʈ

    private RectTransform   rProflieBG;   // UI�� �̵��� RectTransform�� �����

    private Vector2         vOriginalPos; // ������ ���͸� ��´�
    private Vector2         vNextPos;     // ���� �������� �Ѿ ���͸� ��´�
    private Vector2         vPrevPos;     // ���� �������� �Ѿ ���͸� ��´�

    private Animator        mPageAnim;    // ������ �ѱ�� �ִϸ��̼� ���

    private float           mMoveSpeed;   // ������ �Ѿ�� �ӵ� ���

    [HideInInspector]
    public  bool            mIsMoving;    // �������� �����̴��� �Ǵ��ϴ� bool��

    public  bool mIsUpset;                // �Ҹ���Ƽ�� ������ ��ü��Ƽ�� ������

    void Awake()
    {
        mMoveSpeed = 250f; // ������ �ѱ�� �ӵ�

        mUIManager = GameObject.Find("UIManager").GetComponent<RecordUIManager>();
        mPageAnim  = GetComponent<Animator>();
        rProflieBG = GetComponent<RectTransform>();

        // �ִϸ��̼��� Ȱ��ȭ�Ǹ� �̵��� �����Ǳ� ������ �ʿ��� ��쿡�� ����Ѵ�
        mPageAnim.enabled = false; 

        InitPosValue();   // ���͵��� �ʱ�ȭ��Ų��.
        mIsMoving = true; // �ʱⰪ�� ������ �� �ִ�.
    }

    // �����̴� �Ŷ� ������� ������Ʈ ����ؾ� �������� ������ȣ�� �Ǵ� Update�� ���ؼ� ���� �Ȼ���
    void FixedUpdate() 
    {
        if (mUIManager) // null check
        {
            if (mIsMoving) // �����δٸ�
            {
                // ���� ������ ��ư�� ������
                // ���� rect ��Ŀ �������� ���� ��ġ���� �����ϵ���
                if (mUIManager.mIsNext && rProflieBG.anchoredPosition.x > vNextPos.x
                                       && rProflieBG.anchoredPosition.y > vNextPos.y)
                {
                    // mMoveSpeed ��ŭ �����δ�.
                    this.gameObject.transform.
                        Translate(new Vector2(-1, -1) * mMoveSpeed * Time.deltaTime);
                }
                // ���� ��ġ�� �����ϰų� �� �̻� �Ѿ ��� �ٷ� ���� ��ġ������ ����
                else if (mUIManager.mIsNext && rProflieBG.anchoredPosition.x <= vNextPos.x
                                            && rProflieBG.anchoredPosition.y <= vNextPos.y)
                {
                    InitPosValue();
                }

                // ���� ������ ��ư�� ������
                if (mUIManager.mIsPrev && rProflieBG.anchoredPosition.x < vPrevPos.x
                                       && rProflieBG.anchoredPosition.y < vPrevPos.y)
                {
                    this.gameObject.transform.
                        Translate(new Vector2(1, 1) * mMoveSpeed * Time.deltaTime);
                }
                else if (mUIManager.mIsPrev && rProflieBG.anchoredPosition.x >= vPrevPos.x
                                            && rProflieBG.anchoredPosition.y >= vPrevPos.y)
                {
                    InitPosValue();
                }

                // �������� �Ѿ�� �ִϸ��̼��� ������ ��ġ
                if (rProflieBG.anchoredPosition.x <= (int)ConstantsVector.EFrontToBack.X
                 && rProflieBG.anchoredPosition.y <= (int)ConstantsVector.EFrontToBack.Y)
                {
                    // �Ҹ� ��Ƽ�� ��ü ����� ������ �ѱ�� ����� �ٸ�
                    // �Ҹ� ��Ƽ�� ����
                    if (mIsUpset == true)
                    {
                        // ��ġ �̵�
                        rProflieBG.anchoredPosition = new Vector2((int)ConstantsVector.EThirdPos.X,
                                                                  (int)ConstantsVector.EThirdPos.Y);
                        InitPosValue();                // ���� ����
                    }
                    // ��ü����
                    else if (mIsUpset == false)
                    {
                        this.gameObject.transform.SetSiblingIndex(3);
                        Invoke("DelaySibling", 0.12f);

                        mPageAnim.enabled = true;           // �ִϸ����� ��Ƽ���Ͽ� ����� �� �ְ� �Ѵ�                    
                        mPageAnim.SetTrigger("isLastPage"); // �ִϸ��̼� ����
                        Invoke("DelayMoveProfile", 0.5f);
                    }
                }
                // ���� �ؿ� ���̴� �Ϳ��� ������ �̵��� ��
                else if (rProflieBG.anchoredPosition.x >= (int)ConstantsVector.EBackToFront.X
                      && rProflieBG.anchoredPosition.y >= (int)ConstantsVector.EBackToFront.Y)
                {
                    // �� ������ �̵�
                    rProflieBG.anchoredPosition = new Vector2((int)ConstantsVector.EFirstPos.X, 
                                                              (int)ConstantsVector.EFirstPos.Y);
                    InitPosValue();                     // ���� ����
                }
            }

            // ���̾��Ű �ú� �����Ͽ� UI �켱���� ����
            // ���� �տ� ���� ��
            if (rProflieBG.anchoredPosition.x == (int)ConstantsVector.EFirstPos.X
             && rProflieBG.anchoredPosition.y == (int)ConstantsVector.EFirstPos.Y)
            {
                this.gameObject.transform.SetSiblingIndex(2); 
            }
            // �߰�
            else if (rProflieBG.anchoredPosition.x == (int)ConstantsVector.ESecondPos.X
                  && rProflieBG.anchoredPosition.y == (int)ConstantsVector.ESecondPos.Y)
            {
                this.gameObject.transform.SetSiblingIndex(1);
            }
            // ���� �ڿ� ���� ��
            else if (rProflieBG.anchoredPosition.x == (int)ConstantsVector.EThirdPos.X
                  && rProflieBG.anchoredPosition.y == (int)ConstantsVector.EThirdPos.Y)
            {
                this.gameObject.transform.SetSiblingIndex(0);
            }            
        }
    }
    // 0�� �ú� �ε��� ��¦ ������ ����
    void DelaySibling()
    {
        this.gameObject.transform.SetSiblingIndex(0);
    }
    // �ִϸ��̼��� ���۵Ǵ� ���� �ð� ������
    void DelayMoveProfile()
    {
        // ��ġ �̵�
        rProflieBG.anchoredPosition = new Vector2((int)ConstantsVector.EThirdPos.X,
                                                  (int)ConstantsVector.EThirdPos.Y);
        mPageAnim.enabled = false; // �ִϸ��̼� ��   
        InitPosValue();                // ���� ����
    }
    // ���� ����
    void InitPosValue()
    {   
        vOriginalPos = rProflieBG.anchoredPosition;                       // ���� ��ġ
        vNextPos = new Vector2(vOriginalPos.x - 50, vOriginalPos.y - 50); // ���������� ��ġ
        vPrevPos = new Vector2(vOriginalPos.x + 50, vOriginalPos.y + 50); // ���������� ��ġ
        mIsMoving = false;                                                // �̵��� �����
    }    
}
