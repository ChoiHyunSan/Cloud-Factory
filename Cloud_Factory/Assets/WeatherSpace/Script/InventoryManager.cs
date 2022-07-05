using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    /////////////////
    //���� ���� ����//
    /////////////////
    public List<IngredientData>  mType;
    public List<int>  mCnt;

    private int mMaxStockCnt = 10; //�켱�� 10�����ϱ��� ����
    private int mMaxInvenCnt = 30; //�켱�� 30�����ϱ��� ����

    private void Start()
    {
        mType = new List<IngredientData>(); //����Ʈ �ʱ�ȭ
        mCnt = new List<int>(); //����Ʈ �ʱ�ȭ
    }


    //////////////////////////////
    //ä�� ���� �κ��丮 ���� �Լ�//
    //////////////////////////////
    public bool addStock(KeyValuePair<IngredientData, int> _stock)
    {
        if (mType.Count >= mMaxInvenCnt) return false; // �κ��丮 ��ü�� �ƿ� ���� �� ��� return false
        //�κ��丮�� �ڸ��� �ִ� ���
        if (!mType.Contains(_stock.Key)) //�κ��� ���� ���� ������ ���� �׳� �ְ� return true
        {
            mType.Add(_stock.Key);
            mCnt.Add(_stock.Value);
            return true;
        }

        //�κ��丮 �ȿ� ������ ��� ������ �̹� �ִ� ���
        int idx = mType.IndexOf(_stock.Key); //index �� ����.

        if (mCnt[idx] >= mMaxStockCnt) return false;//�κ��丮 ��� �� ���尡�� ���� ����
        int interver = mMaxStockCnt - (_stock.Value+ mCnt[idx]); //���尡�� ���� - (���ο�� �߰����� �� �κ��丮�� ����ɰ���) = �������� ���
        if (interver >= 0) mCnt[idx] = mMaxStockCnt; //���̰� 0���� ũ�� ������ Max Cnt
        else
            mCnt[idx] += _stock.Value; //�ش� ������ ī��Ʈ �߰�.

        return true;
    }


    //////////////////////////////
    /////////��� ���� �Լ�////////
    //////////////////////////////
    //��� ���ýÿ� �ش� ��� ������ 0�� �Ǹ� ����Ʈ���� ����, �ش� ��ᰳ���� 0���� 1�� �Ǹ� ����Ʈ�� �߰�.
    public void updateStockCnt(IngredientData _stockDt,bool _switch)
    {
        //switch = true �� ��쿡�� ��� ����, switch = false �� ��쿡�� ��� ����� ������Ʈ ���
        //�Ű����ڴ� ������ �κ��丮�� ���� �ߴ�, �Ǵ� �����ϴ� ����̴�.
        if (_switch)
            selectStock(_stockDt);
        else
            cancelStock(_stockDt);

        return;
    }

    private void selectStock(IngredientData _stockDt)
    {
        int idx = mType.IndexOf(_stockDt); //index �� ����.
        int updatedCnt = --mCnt[idx];
        if (updatedCnt != 0) return;

        //���� ��� 0�̶�� �ƿ� ����Ʈ���� ����
        mType.RemoveAt(idx);
        mCnt.RemoveAt(idx);
    }

    private void cancelStock(IngredientData _stockDt)
    {
        //�������� ��� ���ÿ��� ��ҵ� ��ᰡ �κ��丮�� �ִ��� �˻�.
        //���� ���ٸ� ������� �� ����Ʈ�� �� ��� �߰��ؼ� ���� +1 �Ѵ�.
        if (mType.Contains(_stockDt)) mCnt[mType.IndexOf(_stockDt)]++;
        else
        {
            mType.Add(_stockDt);
            mCnt.Add(1);
        }
    }
    
    //�κ��丮 ����� ������ �Ŀ��� ������ �������Ͽ��� UI�� �����ִ� ��ϵ� Update���־�� �Ѵ�.

    /////////////////////
    //�κ��丮 ���� �Լ�//
    ////////////////////

    //�������ִ� 2���� ����Ʈ�� ��ųʸ� �������� ����: ��Ȱ�� ������ ���ؼ�!
    private Dictionary<IngredientData, int> mergeList2Dic()
    {
        Dictionary<IngredientData, int> results = new Dictionary<IngredientData, int>();
        foreach(IngredientData stock in mType)
            results.Add(stock, mCnt[mType.IndexOf(stock)]);
        
        return results;
    }

    //����Ʈ ��ü�� �����Ѵ�. ���� �� ���� �� ����Ʈ�� ���� UI�� �ݿ��Ѵ�.
    private Dictionary<IngredientData, int> sortStock(Emotion _emotion) 
    {
        Dictionary<IngredientData, int> tmpList = mergeList2Dic();
        Dictionary<IngredientData, int> results = new Dictionary<IngredientData, int>();

        //�������� �з�: �Էµ��� ������ �����ִ� �͸� �̾Ƽ� tmpList�� �߰��Ѵ�.
        foreach (KeyValuePair<IngredientData, int> stock in tmpList)
        {
            if (!stock.Key.iEmotion.ContainsKey((int)_emotion)) continue;

            results.Add(stock.Key, stock.Value);
        }
        return results;
    }

    private Dictionary<IngredientData, int> sortStock() // �������� �з�:
    {
        Dictionary<IngredientData, int> tmpList = mergeList2Dic();
        Dictionary<IngredientData, int> results = new Dictionary<IngredientData, int>();

        var queryAsc = tmpList.OrderBy(x => x.Value);

        foreach (var dictionary in queryAsc)
            results.Add(dictionary.Key, dictionary.Value);

        return results;
    }
}
