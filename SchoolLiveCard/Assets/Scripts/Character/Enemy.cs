using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour,IDamagable
{
    public EnemySO enemyConfig;
   
    //public List<CardSO> cardsBag;

    public List<GameObject> handsCard;//手上的卡

    private int maxHp;
    private int currentHp;
    private int magic;

    void Start()
    {
        Debug.Log("Enemy Start");
    }

    public void LoadDataFromSO()//主要是加载一些动态数据，比如血量
    {
        maxHp = enemyConfig.maxHp;
        currentHp = enemyConfig.maxHp;
        magic = enemyConfig.initMagic;
    }
    
    public void SaveDataToSO()
    {
        
    }

    public bool ChooseCardToPlay()
    {
        if (handsCard.Count > 0)
        {
            BattleSystem.currentEnemyCard = handsCard[Random.Range(0, handsCard.Count)];
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public CardSO DrawOneCard()
    {
        return enemyConfig.cardBag[Random.Range(0, enemyConfig.cardBag.Length)];
    }

    public bool TakeDamage(int damageValue)
    {
        currentHp = Math.Max(0, currentHp - damageValue);
        Debug.Log($"Enemy hp: {currentHp}");
        HpInfo hpInfo = new HpInfo(currentHp, maxHp);
        EventCenter.GetInstance().EventTrigger("EnemyTakenDamage",hpInfo);
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
