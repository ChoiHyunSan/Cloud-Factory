using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class InventoryContainer : MonoBehaviour
{

    public List<GameObject> mUiInvenStocks;
    public GameObject[] mTxtInfoPrefab;
    public Sprite mDefaultSprite;

    [SerializeField]
    CloudMakeSystem Cloudmakesystem;
    [SerializeField]
    InventoryManager inventoryManager;

    private Dictionary<IngredientData, int> mUiStocksData; //UI�� �������� StocksData

    /////////////////////
    //�κ��丮 ���� UI//
    ////////////////////
    [SerializeField]
    private Dropdown mDropDown;


    void Start()
    {
        Cloudmakesystem = GameObject.FindWithTag("CloudSystem").GetComponent<CloudMakeSystem>();
        mDropDown = GameObject.Find("Dropdown").GetComponent<Dropdown>(); //���� ������ ������Ʈ�� �˻� ����.
    }

    /////////////////////
    //Button Interact///
    ////////////////////
    public void clicked() //matarial in inventory selected
    {
        if (inventoryManager == null)
            inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();

        string name = EventSystem.current.currentSelectedGameObject.name;
        Cloudmakesystem.E_Selected(name);
        updateStockCnt(name, true);
    }

    public void unclicked() //matarial in cloudmaker deselected
    {
        GameObject target = EventSystem.current.currentSelectedGameObject;
        Sprite sprite = target.GetComponent<Image>().sprite;

        if (sprite.name == "Circle") return; //����ó��

        updateStockCnt(getDataWithSprite(sprite.name).ingredientName, false);

        Cloudmakesystem.E_UnSelected(target.name);
        Debug.Log("Ŭ��");
    }
   
    public void sortWithCnt() //Button Interaction Function
    {
        mUiStocksData = sortStock();

        updateInven(mUiStocksData);
    }

    public void activeDropDown()
    {
        if (mDropDown.interactable) mDropDown.interactable = false;
        else
        {
            mDropDown.interactable = true;
            mDropDown.value = 0;
            OnDropdownEvent();
        }
    }

    //DropDown public Method
    public void OnDropdownEvent()
    {     
        Debug.Log("[DropdownEvent] {" + mDropDown.value + "} clicked.");

        Dictionary<IngredientData, int> sortedDt = sortStock(mDropDown.value);

        //���� �ִ� ���� ������Ʈ�� ���� ������ ���� ���Ѵ�.
        int difference = sortedDt.Count - mUiInvenStocks.Count;
        //���� �ִ°� �� ���� ��� : ������ �ʱ�ȭ
        if (difference < 0) //difference�� 2�̸� 
        {
            for (int i = sortedDt.Count; i < mUiInvenStocks.Count; i++)
            {
                mUiInvenStocks[i].name = "000";
                mUiInvenStocks[i].GetComponent<Image>().sprite = mDefaultSprite;

                if (mUiInvenStocks[i].transform.GetComponent<Button>())
                {
                    Destroy(mUiInvenStocks[i].GetComponent<Button>());
                }
               
                if (mUiInvenStocks[i].transform.childCount != 0)
                {
                    Destroy(mUiInvenStocks[i].transform.GetChild(1).gameObject);
                    Destroy(mUiInvenStocks[i].transform.GetChild(0).gameObject);
                }
                      
            }
        }
        else
        {
            //���� �ִ°� �� ���� ��� : ���׸�ŭ ������Ʈ ����
            for (int i = mUiInvenStocks.Count; i < sortedDt.Count; i++)
            {
                GameObject invenUI = mUiInvenStocks[i];

                if (invenUI.transform.childCount == 0)
                {
                    GameObject cntTxt = Instantiate(mTxtInfoPrefab[0]);
                    cntTxt.transform.SetParent(invenUI.transform, false);
                    cntTxt.transform.GetComponent<Text>().text = "0";

                    GameObject nameTxt = Instantiate(mTxtInfoPrefab[1]);
                    nameTxt.transform.SetParent(invenUI.transform, false);
                    nameTxt.transform.GetComponent<Text>().text = "000";
                }

                //��ư ������Ʈ�� ������ ������ش�.
                if (invenUI.transform.GetComponent<Button>() == null)
                {
                    Button btn = invenUI.AddComponent<Button>();
                    btn.onClick.AddListener(clicked);
                }

                //Image Update
                invenUI.transform.GetComponent<Image>().sprite = mDefaultSprite;

                //Name Upadate
                invenUI.name = "000";
            }
        }

        initInven(sortedDt, "private");
        updateInven(sortedDt);
    }

    //������ �������� ���� �������� �Ѿ ��, ������ ä�� �κ��丮 �����͸� ���������� UI�κ��丮�� �Ѱ��ش�.
    public void initInven(Dictionary<IngredientData, int> invenData, string order)
    {
        if(order == "public")
        {
            mUiStocksData = new Dictionary<IngredientData, int>();
            mUiStocksData = invenData; //UI��Ͽ� ����!

            int tmp = 0;
            foreach (KeyValuePair<IngredientData, int> stock in mUiStocksData)
            {
                GameObject invenUI = transform.GetChild(tmp).gameObject;
                mUiInvenStocks.Add(invenUI); //UIInvenGameObject List �߰�.
                tmp++;
            }
        }
       

        setInven(invenData);
    }

    private void setInven(Dictionary<IngredientData, int> _mData)
    {

        //invenData�� invenContainer(UI)List�� �־��ش�.
        int tmp = 0;
        foreach (KeyValuePair<IngredientData, int> stock in _mData)
        {
            GameObject invenUI = mUiInvenStocks[tmp];

            if (invenUI.transform.childCount == 0)
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


    //�ش� data ��ųʸ��� ���� ��ŭ �����͸� �ٲ۴�.
    private void updateInven(Dictionary<IngredientData, int> _mData)
    {
        int tmp = 0;
        foreach (KeyValuePair<IngredientData, int> data in _mData)
        {
            GameObject stockObj = mUiInvenStocks[tmp];

            //GameObject name
            stockObj.name = data.Key.ingredientName;
            //�̹���
            stockObj.transform.GetComponent<Image>().sprite = data.Key.image;
            //cnt
            stockObj.transform.GetChild(0).GetComponent<Text>().text = data.Value.ToString();
            //name
            stockObj.transform.GetChild(1).GetComponent<Text>().text = data.Key.ingredientName.ToString();

            tmp++; //plus index value
        }
    }

    private IngredientData getDataWithSprite(string _spritename) //Sprite�� �Ű������� �ش� ������ data�� �˻��Ѵ�.
    {
        IngredientData data = inventoryManager.mIngredientDatas[inventoryManager.minvenLevel - 1].mItemList.Find(item => _spritename == item.image.name);

        return data;
    }


    //////////////////////////////
    /////////��� ���� �Լ�////////
    //////////////////////////////
    //��� ���ýÿ� �ش� ��� ������ 0�� �Ǹ� ����Ʈ���� ����, �ش� ��ᰳ���� 0���� 1�� �Ǹ� ����Ʈ�� �߰�.
    private void updateStockCnt(string _dtName, bool _switch)
    {
        //switch = true �� ��쿡�� ��� ����, switch = false �� ��쿡�� ��� ����� ������Ʈ ���
        //�Ű����ڴ� ������ �κ��丮�� ���� �ߴ�, �Ǵ� �����ϴ� ����̴�.
        if (_switch)
            selectStock(_dtName);
        else
            cancelStock(_dtName);

        return;
    }

    private GameObject findObjectWithData(IngredientData _stockDt)
    {
        GameObject result = null;
        foreach(GameObject stock in mUiInvenStocks)
        {
            if (stock.name == _stockDt.ingredientName) 
                result = stock;
        }
        return result;
    }
    private void selectStock(string dataName)
    {
        IngredientData stockDt = inventoryManager.mIngredientDatas[inventoryManager.minvenLevel - 1].mItemList.Find(item => dataName == item.ingredientName);
        
        mUiStocksData[stockDt]--;

        GameObject uiGameObj = findObjectWithData(stockDt);
        uiGameObj.transform.GetChild(0).GetComponent<Text>().text = mUiStocksData[stockDt].ToString();

        if (mUiStocksData[stockDt] != 0) return;

        //���� ��� 0�̶�� �ƿ� ����Ʈ���� ����
        removeStockInInven(stockDt, uiGameObj);
    }

    private void cancelStock(string dataName)
    {
        IngredientData _stockDt = inventoryManager.mIngredientDatas[inventoryManager.minvenLevel - 1].mItemList.Find(item => dataName == item.ingredientName);
        GameObject uiGameObj = findObjectWithData(_stockDt);
        //�������� ��� ���ÿ��� ��ҵ� ��ᰡ �κ��丮�� �ִ��� �˻�.
        //���� ���ٸ� ������� �� ����Ʈ�� �� ��� �߰��ؼ� ���� +1 �Ѵ�.
        if (mUiStocksData.ContainsKey(_stockDt))
        {
            mUiStocksData[_stockDt]++;
            uiGameObj.transform.GetChild(0).GetComponent<Text>().text = mUiStocksData[_stockDt].ToString();
        }   
        else
            addStockInInven(_stockDt, uiGameObj);
    }

    private void addStockInInven(IngredientData stockDt, GameObject uiGameObj)
    {
        //1. ������ ������ �ϳ��� ���� �����.
        updateInven(mUiStocksData);

        //Data �߰�
        mUiStocksData.Add(stockDt,1); //����Ʈ���� �ش� data �߰�

        //�κ��丮 ��ü ������Ʈ

        //2. �κ��丮�� ������ stock�� ������Ʈ �߰� �� �̹��� �ʱ�ȭ.
        // tmp instance
        GameObject lastStockInInven = mUiInvenStocks[mUiStocksData.Count-1];
        
        //Component �߰�
        GameObject cntTxt = Instantiate(mTxtInfoPrefab[0]);
        cntTxt.transform.SetParent(lastStockInInven.transform, false);
        cntTxt.transform.GetComponent<Text>().text = mUiStocksData[stockDt].ToString();

        GameObject nameTxt = Instantiate(mTxtInfoPrefab[1]);
        nameTxt.transform.SetParent(lastStockInInven.transform, false);
        nameTxt.transform.GetComponent<Text>().text = stockDt.ingredientName.ToString();

        Button btn = lastStockInInven.AddComponent<Button>();
        btn.onClick.AddListener(clicked);

        //Image Update
        lastStockInInven.transform.GetComponent<Image>().sprite = stockDt.image; 

        //Name Upadate
        lastStockInInven.name = stockDt.ingredientName; ; //Game Object Name �ʱ�ȭ       
    }

    private void removeStockInInven(IngredientData stockDt, GameObject uiGameObj)
    {
        mUiStocksData.Remove(stockDt); //����Ʈ���� �ش� data ����

        //�κ��丮 ��ü ������Ʈ

        //1. �κ��丮�� ������ stock�� ������Ʈ ���� �� �̹��� �ʱ�ȭ.
        // tmp instance
        GameObject lastStockInInven = mUiInvenStocks[mUiStocksData.Count];
        lastStockInInven.name = "000"; //Game Object Name �ʱ�ȭ
        lastStockInInven.transform.GetComponent<Image>().sprite = mDefaultSprite; //img�ʱ�ȭ
        Destroy(lastStockInInven.transform.GetComponent<Button>()); // button component ����
        Destroy(lastStockInInven.transform.GetChild(0).gameObject); // cnt txt ����
        Destroy(lastStockInInven.transform.GetChild(1).gameObject); // name txt ����

        //2. ������ ������ �ϳ��� ���� �����.
        updateInven(mUiStocksData);
    }

    

    //�κ��丮 ����� ������ �Ŀ��� ������ �������Ͽ��� UI�� �����ִ� ��ϵ� Update���־�� �Ѵ�.

    /////////////////////
    //�κ��丮 ���� �Լ�//
    ////////////////////


    //����Ʈ ��ü�� �����Ѵ�. ���� �� ���� �� ����Ʈ�� ���� UI�� �ݿ��Ѵ�.
    private Dictionary<IngredientData, int> sortStock(int _emotion) //�������� �з�
    {
        Dictionary<IngredientData, int> results = new Dictionary<IngredientData, int>();

        //�������� �з�: �Էµ��� ������ �����ִ� �͸� �̾Ƽ� tmpList�� �߰��Ѵ�.
        foreach (KeyValuePair<IngredientData, int> stock in mUiStocksData)
        {
            //�ش� ������ stock�� iEmotion����Ʈ�� �����ϴ��� Ȯ��
            if (!stock.Key.iEmotion.ContainsKey(_emotion)) continue;
            //���������� ����

            //������������ ����
            var queryAsc = stock.Key.iEmotion.OrderByDescending(x => x.Value);// int, int
            Debug.Log("[Emotion Sorting] Target:{" + _emotion + "} : PreOrderEmo :{" + queryAsc.First().Key + "}");
            //ù��° ���� ���� ũ�Ƿ� ���� ū ���� �Ű����ڿ� ���� �����̶�� �߰����ش�.
            if(_emotion != queryAsc.First().Key) continue;
            Debug.Log("��Ͽ� �߰��Ǿ����ϴ�");
            results.Add(stock.Key, stock.Value);
        }

        
        return results;
    }

    private Dictionary<IngredientData, int> sortStock() // �������� �з�:
    {
        Dictionary<IngredientData, int> results = new Dictionary<IngredientData, int>();

        var queryAsc = mUiStocksData.OrderBy(x => x.Value);

        foreach (var dictionary in queryAsc)
            results.Add(dictionary.Key, dictionary.Value);

        return results;
    }
}
