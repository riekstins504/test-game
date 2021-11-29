using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Player : MonoBehaviour,IDamagable
{
    public PlayerSO playerConfig;

    public List<GameObject> handsCard; //手上的卡

    public int maxHp;
    public int currentHp;
    public int magic;



    void Start()
    {
        Debug.Log("Player Start");
        //EventCenter.GetInstance().EventTrigger<PlayerSO>("PlayerEnterBattle", playerConfig);
        
        //抽牌事件处理器
        //OnPlayerDrawCard += UIManager.Instance.PlayerDrawCardAction;
        //打牌事件处理器
        //OnPlayerPlayCard += UIManager.Instance.PlayerPlayCardAction;
    }

    public void LoadDataFromSO()//主要是加载一些动态数据，比如血量，魔力值
    {
        maxHp = playerConfig.maxHp;
        currentHp = playerConfig.maxHp;
        magic = playerConfig.initMagic;
        
    }
    
    public void SaveDataToSO()
    {
        playerConfig.currentHp = currentHp;
    }

    public CardSO DrawOneCard()
    {
        return playerConfig.cardBag[Random.Range(0, playerConfig.cardBag.Length)];
    }

    public bool TakeDamage(int damageValue)
    {
        currentHp = Math.Max(0, currentHp - damageValue);
        Debug.Log($"Player hp: {currentHp}");
        HpInfo hpInfo = new HpInfo(currentHp, maxHp);
        EventCenter.GetInstance().EventTrigger("PlayerTakenDamage",hpInfo);
        if (currentHp <= 0)
        {
            return true;//如果血量到达0，则表示死亡。
        }
        else
        {
            return false;
        }
    }
}



