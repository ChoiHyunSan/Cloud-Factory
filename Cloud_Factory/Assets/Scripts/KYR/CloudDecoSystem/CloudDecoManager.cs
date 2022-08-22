using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cloud
{
    public int mShelfLife;
    public List<EmotionInfo> mEmotions;

    private bool mState; //0 = ��� 1 = ����
    private Image cloudImage;
    private List<Image> decoImages;
    
    public Cloud(List<EmotionInfo> Emotions)
    {
        mEmotions = Emotions;

        //�����Լ��� �ڵ����� ������ ����
        setShelfLife(mEmotions);
        setCloudImage(mEmotions);
        setDecoImage(mEmotions);
    }

    private void setShelfLife(List<EmotionInfo> Emotions)
    {
        //������ ���� �´� �����Ⱓ
    }
    private void setCloudImage(List<EmotionInfo> Emotions)
    {
        //������ ���� �´� base �����̹���
    }

    private void setDecoImage(List<EmotionInfo> Emotions)
    {
        //������ ���� �´� ���� �̹���
    }
}
public class CloudDecoManager : MonoBehaviour
{

    //���ڿ� ��ư �׷�
    public GameObject B_decoParts;
    public GameObject B_PosNeg;


    public Cloud mtargetCloud;// �������忡�� ���� ������ ����.

    private bool cursorChasing;
    private GameObject selectedParts;
    private List<List<GameObject>> mUIDecoParts;
    private InventoryManager inventoryManager;

    //����ġ�� �����
    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;
  
   
    private void Start()
    {
        mUIDecoParts = new List<List<GameObject>>();

        //(�� �̵��ÿ��� ����)
        //inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();


        initParam();
        init();
    }

    private void initParam()
    {
        cursorChasing = false;
        //UI ����
        for (int i = 0; i < 3; i++)
        {
            Transform tmp = B_decoParts.transform.GetChild(i);
            List<GameObject> Ltmp = new List<GameObject>();
            for (int j = 0; j < 3; j++)
                Ltmp.Add(tmp.GetChild(j).gameObject);


            mUIDecoParts.Add(Ltmp);
        }

    }
    private void init()
    {
        //Cloud data ��������.(���̵��ÿ�������)
       // mtargetCloud = inventoryManager.createdCloudData;

       
        //Ŭ���� �����Ϳ� ���� UI�� �̹��� ����.

    }

    private Cloud getTargetCloudData()
    {
        return GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>().createdCloudData;
    }


    //UI Button Function
    public void EClickedDecoParts()
    {
        GameObject target = EventSystem.current.currentSelectedGameObject;
        selectedParts = Instantiate(target.transform.GetChild(0).gameObject, Input.mousePosition, target.transform.rotation);
        selectedParts.AddComponent<DecoParts>();
        selectedParts.GetComponent<DecoParts>().init(top_right_corner, bottom_left_corner);
        selectedParts.AddComponent<Button>();
        selectedParts.transform.SetParent(this.transform, true);

        selectedParts.GetComponent<Button>().onClick.AddListener(newPartsAttach);
     
        cursorChasing = true;
    }

    public void newPartsAttach()
    {
        if (!selectedParts.GetComponent<DecoParts>().canAttached) return;
        cursorChasing = false;
    }

    
    private void Update_PartsMoving()
    {
        if (!cursorChasing) return;
        selectedParts.transform.position = Input.mousePosition;
    }

    private void Update()
    {
        
        Update_PartsMoving();
    }

    //Gizmo//
    private void DrawRectange(Vector2 top_right_corner, Vector2 bottom_left_corner)
    {
        Vector2 center_offset = (top_right_corner + bottom_left_corner) * 0.5f;
        Vector2 displacement_vector = top_right_corner - bottom_left_corner;
        float x_projection = Vector2.Dot(displacement_vector, Vector2.right);
        float y_projection = Vector2.Dot(displacement_vector, Vector2.up);

        Vector2 top_left_corner = new Vector2(-x_projection * 0.5f, y_projection * 0.5f) + center_offset;
        Vector2 bottom_right_corner = new Vector2(x_projection * 0.5f, -y_projection * 0.5f) + center_offset;

        Gizmos.DrawLine(top_right_corner, top_left_corner);
        Gizmos.DrawLine(top_left_corner, bottom_left_corner);
        Gizmos.DrawLine(bottom_left_corner, bottom_right_corner);
        Gizmos.DrawLine(bottom_right_corner, top_right_corner);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        DrawRectange(top_right_corner, bottom_left_corner);

    }


}
