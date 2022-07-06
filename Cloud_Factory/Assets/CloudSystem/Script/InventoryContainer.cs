using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryContainer : MonoBehaviour
{
    public List<GameObject> mInvenStocks;
    public GameObject[] mTxtInfoPrefab;

    [SerializeField]
    CloudMakeSystem Cloudmakesystem;



    public void updateInven(Dictionary<IngredientData, int> invenData)
    {
        //invenData�� invenContainer(UI)List�� �־��ش�.
        int tmp = 0;
        foreach(KeyValuePair<IngredientData, int> stock in invenData)
        {
            GameObject invenUI = transform.GetChild(tmp).gameObject;
            mInvenStocks.Add(invenUI);

            if(invenUI.transform.childCount == 0)
            {
                GameObject cntTxt = Instantiate(mTxtInfoPrefab[0]);
                cntTxt.transform.SetParent(invenUI.transform, false);
                cntTxt.transform.GetComponent<Text>().text = stock.Value.ToString();

                GameObject nameTxt = Instantiate(mTxtInfoPrefab[1]);
                nameTxt.transform.SetParent(invenUI.transform, false);
                nameTxt.transform.GetComponent<Text>().text = stock.Key.ingredientName.ToString();
            }
            
            //��ư ������Ʈ�� ������ ������ش�.
            if (invenUI.transform.GetComponent<Button>() == null)
            {
                Button btn = invenUI.AddComponent<Button>();
                btn.onClick.AddListener(clicked);     
            }

            //Image Update
            invenUI.transform.GetComponent<Image>().sprite = stock.Key.image;

            //Name Upadate
            invenUI.name = stock.Key.ingredientName;

            tmp++;
        }
    }

    public void clicked() //matarial in inventory selected
    {
        Cloudmakesystem.E_Selected(EventSystem.current.currentSelectedGameObject.name);

    }

    void Start()
    {
        Cloudmakesystem = GameObject.FindWithTag("CloudSystem").GetComponent<CloudMakeSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
