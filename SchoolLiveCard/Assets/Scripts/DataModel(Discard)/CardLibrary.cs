using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//卡牌库：整个游戏的所有卡牌
public class CardLibrary
{
    
    private static CardLibrary instance = new CardLibrary();//饿汉单例
    public static CardLibrary Instance
    {
        get => instance;
    }

    private Dictionary<int, CardData> cardDict;
    public Dictionary<int, CardData> CardDict
    {
        get => cardDict;
    }

    public CardLibrary()
    {
        cardDict = new Dictionary<int, CardData>();
        //构造函数中从文件中加载卡牌库
        LoadTestCard();
        //LoadCardsFromFile();
    }

    private void LoadCardsFromFile()
    {
        //以cardID作为Dictionary的Key，若Key重复，说明CardID重复，需要报错
    }
    
    private void LoadTestCard()
    {
        for (int i = 0; i < 5; i++)
        {
            cardDict.Add(i+1, new AttackCardData(i+1, $"攻击卡{i+1}", i+6));
        }
        
    }
}
