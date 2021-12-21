using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : MonoBehaviour
{
    public static UIBag Instance;
    //每行多少个卡牌
    public int colCount = 3;
    public List<CardSO> bagDataList = new List<CardSO>();
    public List<BagCard> bagCardList = new List<BagCard>();
    public List<Transform> rowContentList = new List<Transform>();
    public Transform horContent;
    public Transform ViewContent;
    public Text uiGold;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        RefreshUI();
        RefreshGold();
    }
    
    public void RefreshGold()
    {
        Debug.Log("Refresh Gold!");
        uiGold.text = BattleSystem.Instance.Player.playerConfig.Gold.ToString();
    }

    public void RefreshUI()
    {
        bagCardList.Clear();
        int length = bagDataList.Count;
        //添加，删除所需行数
        int rowLength = Mathf.CeilToInt((float)length / colCount);
        rowLength = rowLength - rowContentList.Count;
        Debug.Log(rowLength);
        //添加新行数
        if (rowLength >= 0)
        {
            for (int i = 0; i < rowLength; i++)
            {
                rowContentList.Add(Instantiate(horContent.gameObject, ViewContent).transform);
                Debug.Log("Create HorCard!");
            }
        }
        //删除行数
        else
        {
            while (rowLength < 0)
            {
                GameObject con = rowContentList[rowContentList.Count - 1].gameObject;
                rowContentList.RemoveAt(rowContentList.Count - 1);
                Destroy(con);
                rowLength++;
                Debug.Log("Destroy HorCard!");
            }
        }

        int cardIndex = 0;
        int rowIndex = 0;

        //添加元素
        while (true)
        {
            if (length == 0)
            {
                break;
            }
            if (length < colCount)
            {
                Transform rowContent = rowContentList[rowIndex];
                rowContent.gameObject.SetActive(true);
                foreach (Transform item in rowContent)
                {
                    item.gameObject.SetActive(true);
                }

                for (int i = 0; i < colCount; i++)
                {
                    if (i >= length)
                    {
                        rowContent.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        CreateBagCard(rowContent.GetChild(i).GetComponent<BagCard>(), bagDataList[cardIndex], rowContent);
                        cardIndex++;
                    }
                }
                break;
            }
            else
            {
                Transform rowContent = rowContentList[rowIndex];
                rowContent.gameObject.SetActive(true);
                foreach (Transform item in rowContent)
                {
                    item.gameObject.SetActive(true);
                }
                for (int i = 0; i < colCount; i++)
                {
                    CreateBagCard(rowContent.GetChild(i).GetComponent<BagCard>(), bagDataList[cardIndex], rowContent);
                    cardIndex++;
                }
                length -= colCount;
            }
            rowIndex++;
        }
    }

    void CreateBagCard(BagCard bc, CardSO data, Transform content)
    {
        bc.Init(data);
        bagCardList.Add(bc);
        LayoutRebuilder.ForceRebuildLayoutImmediate(bc.transform.parent.GetComponent<RectTransform>());

    }

    public void AddCardData(CardSO data)
    {
        bagDataList.Add(data);
        RefreshUI();
    }

    public void RemoveCardData(CardSO data)
    {
        if (bagDataList.Contains(data))
        {
            bagDataList.Remove(data);
        }
        RefreshUI();
    }

    public void ClearCardData()
    {
        bagDataList.Clear();
        RefreshUI();
    }


}
