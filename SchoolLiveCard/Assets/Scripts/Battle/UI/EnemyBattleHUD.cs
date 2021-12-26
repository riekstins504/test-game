using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    [Header("Enemy HUD")] 
    public Text enemyNameText;
    public Text enemyLevel;
    public ValueBarUI enemyHpBar;
    public ValueBarUI enemyMagicBar;


    private void OnEnable()
    {
        EventCenter.GetInstance().AddEventListener<EnemySO>("EnemyEnterBattle", InitEnemyHUD);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", SetEnemyHpUI);
    }

    private void OnDisable()
    {
        EventCenter.GetInstance().RemoveEventListener<EnemySO>("EnemyEnterBattle", InitEnemyHUD);
        EventCenter.GetInstance().RemoveEventListener<HpChangeInfo>("EnemyTakenDamage", SetEnemyHpUI);
    }

    private void InitEnemyHUD(EnemySO enemySo)
    {
        if (enemyHpBar != null)
        {
            Debug.Log("enemyHpBar != null");
        }

        if (enemyMagicBar != null)
        {
            Debug.Log("enemyMagicBar != null");
        }
        
        enemyNameText.text = enemySo.enemyName;
        enemyLevel.text = $"Lv.{enemySo.level}";
        enemyHpBar.SetValueBar(enemySo.maxHp,enemySo.maxHp);
        enemyMagicBar.SetValueBar(enemySo.initMagic, enemySo.initMagic);
    }

    private void SetEnemyHpUI(HpChangeInfo hpInfo)
    {
        enemyHpBar.SetValueBar(hpInfo.curHp,hpInfo.maxHp);
    }
    
    
    private void SetEnemyMagicUI(HpChangeInfo magicInfo)
    {
        
    }
}