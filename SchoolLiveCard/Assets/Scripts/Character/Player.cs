using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Player : IDamagable
{
    public readonly PlayerSO playerConfig;

    public List<GameObject> handsCard  = new List<GameObject>(); //手上的卡

    public int maxHp;
    public int currentHp;
    public int magic;


    public Player(PlayerSO playerScriptableObject)
    {
        playerConfig = playerScriptableObject;
        LoadDataFromSO();
    }

    public Player(int hp)
    {
        currentHp = hp;
    }

    public void LoadDataFromSO()//主要是加载一些动态数据，比如血量，魔力值
    {
        maxHp = playerConfig.maxHp;
        currentHp = playerConfig.currentHp;
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
        //Debug.Log($"Player hp: {currentHp}");
        currentHp = Math.Max(0, currentHp - damageValue);
        //Debug.Log($"Player hp: {currentHp}");
        HpChangeInfo hpInfo = new HpChangeInfo();
        hpInfo.curHp = currentHp;
        hpInfo.maxHp = maxHp;
        hpInfo.changedHp = -damageValue;
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



