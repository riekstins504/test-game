using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    [Header("Enemy HUD")] 
    public Text enemyNameText;
    public Text enemyLevel;
    public ValueBarUI enemyHpBar;
    public ValueBarUI enemyMagicBar;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<EnemySO>("EnemyEnterBattle", InitEnemyHUD);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", SetEnemyHpUI);
    }
    
    private void InitEnemyHUD(EnemySO enemySo)
    {
        enemyNameText.text = enemySo.fighterName;
        enemyLevel.text = $"Lv.{enemySo.level}";
        HpChangeInfo info;
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
