using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CloudSystem
{
    public delegate void EventHandler(string name);//����̸� Ȥ�� Ű�� ���ڷ� �޾� �ѱ�.
}

public class CloudMakeSystem : MonoBehaviour
{
    ////////////////////////////////////////////////////////
    ///////            Interface Value               ///////
    ////////////////////////////////////////////////////////
    public CloudSystem.EventHandler event_Selected;  //�κ��丮���� ��ἱ�ý� �̺�Ʈ ȣ��_ �Լ� �� �����ϰ� ����...
    public CloudSystem.EventHandler event_UnSelected;  // ���õ� ��� ��� 
    public CloudSystem.EventHandler event_createBtn; //�������� ��ư

    public Sprite default_sprite;//private���� �ٲ� ����.

    ////////////////////////////////////////////////////////
    ///////            private Value                 ///////
    ////////////////////////////////////////////////////////
    private Transform T_mtrl;//parent transform of mtrl gameObject

    //Data
    [SerializeField]
    public IngredientList mtrlDATA; // ��� ��� ������ ���� �ִ� ����Ʈ scriptable data
    //���߿� private���� �ٲܰ���./

    //UI
    [SerializeField]
    private int UI_mtrl_count; //������ �� �ִ� ��� ����
    [SerializeField]
    private List<GameObject> UI_slct_mtrl; //UI_selected_material
    [SerializeField]
    private Text UI_btn_txt;

    //Debug
    private List<string> L_mtrls;// ���߿� ������ �ɵ�? �ణ ����� ����..��

    //count
    private int total;

    private void selectMtrl(string name)
    {
        if (total > 5) return; //�ִ� 5������ ���� ����

        //
        Debug.Log(name + "�� ���õǾ����ϴ�.");
        L_mtrls.Add(name);
        //

        total++; //update total count

        IngredientData data = mtrlDATA.itemList.Find(item => name == item.ingredientName);
        UI_slct_mtrl[total - 1].GetComponent<SpriteRenderer>().sprite = data.image;


        //
        Debug.Log(name + "�� �������� ����Ʈ�� �߰��˴ϴ�.");
        for (int i = 0; i < L_mtrls.Count; i++)
        {
            Debug.Log("���� " + L_mtrls[i] + "����.");
        }
        //
    }

    private void deselectMtrl(string name)
    {
        GameObject ERSD = UI_slct_mtrl.Find(item => name == item.name); //������ ��ĭ ã��.
        ERSD.GetComponent<SpriteRenderer>().sprite =default_sprite;

        
        listReOrder(UI_slct_mtrl.FindIndex(item => ERSD == item));
    }


    private void listReOrder(int idx) //����ƮUI ����
    {
        if (total <= 0) return;

        //
        L_mtrls.RemoveAt(idx); 
        //

        total--; //update total count

        //list reorder Algorithm
        for (int i = idx; i < total; i ++ )
        {
            GameObject curr = UI_slct_mtrl[i];
            GameObject next = UI_slct_mtrl[i + 1];
            curr.GetComponent<SpriteRenderer>().sprite = next.GetComponent<SpriteRenderer>().sprite;
        }

        //exception handling
        if (total == 0) return;
        UI_slct_mtrl[total].GetComponent<SpriteRenderer>().sprite = default_sprite;
    }

    private void Del_ReadCSV(string name)
    {
        //���� ���չ� ������ �׶� ��ũ��Ʈ �ۼ�.
        Debug.Log("������Ḧ Ȯ���մϴ�.");
    }
    private void Del_CreateCloud(string name)
    {
        if (total < 5)
        {
            Debug.Log("������ �����մϴ�.");
            return;
        }//5�� ��ä��� ���� �ȵ�

        float time = 5f;
        //�ڷ�ƾ
        UI_btn_txt.text = "����� ��";
        StartCoroutine(isMaking(time));
        
    }

 

    IEnumerator isMaking(float time)
    {
        this.transform.Find("Button").GetComponent<Button>().enabled = false;

        //�� ��Ӱ�
        for (int i = 0; i < total; i++)
        {
            UI_slct_mtrl[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        yield return new WaitForSeconds(time);

        this.transform.Find("Button").GetComponent<Button>().enabled = true;

        yield return new WaitForSeconds(1);
        //�� ���
        for (int i = 0; i < total; i++)
        {
            UI_slct_mtrl[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        //UI �ʱ�ȭ
        init_UI();

        total = 0;
        UI_btn_txt.text = "�����ϱ�";

        Debug.Log("������ ����������ϴ�.");

        yield break;
    }


    private void init_UI()
    {
        for (int i = 0; i < total; i++)
        {
            UI_slct_mtrl[i].GetComponent<SpriteRenderer>().sprite = default_sprite;
        }
    }


    private void init()
    {
        T_mtrl = this.transform.Find("selectedIngredient").transform;
        total = 0;
        UI_mtrl_count = T_mtrl.childCount;
        UI_slct_mtrl = new List<GameObject>(); //�������� UIâ�� ���õ� ���.
        L_mtrls = new List<string>(); //�������� UIâ�� ���õ� ���.
        UI_btn_txt = this.transform.Find("Button").GetComponentInChildren<Text>();
        UI_btn_txt.text = "�����ϱ�";

        for (int i = 0; i < UI_mtrl_count; i++)
        {
            UI_slct_mtrl.Add(T_mtrl.GetChild(i).gameObject);
        }

        //event
        event_Selected = selectMtrl;
        

        event_UnSelected = deselectMtrl;

        event_createBtn = Del_ReadCSV;
        event_createBtn += Del_CreateCloud;
    }

    ////////////////////////////////////////////////////////
    ///////                Pipeline                  ///////
    ////////////////////////////////////////////////////////
    void Start()
    {
        init();
    }

}
