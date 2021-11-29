using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform playCardZoneTrans;
    public RectTransform PlayCardZoneTrans
    {
        get => playCardZoneTrans;
    }

    [SerializeField]
    private Text remainTimeText;

    public PlayerHandUI playerHand;
    public GameObject enemyHand;
    public Button endRoundBtn;

    [Header("Enemy HUD")] 
    public Text enemyNameText;
    public Text enemyLevel;
    public ValueBarUI enemyHpBar;
    public ValueBarUI enemyMagicBar;

    [Header("Player HUD")] 
    public ValueBarUI playerHpBar;
    public ValueBarUI playerMagicBar;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<PlayerSO>("PlayerEnterBattle", SetPlayerHUD);
        EventCenter.GetInstance().AddEventListener<EnemySO>("EnemyEnterBattle", SetEnemyHUD);
        
        EventCenter.GetInstance().AddEventListener<ValueBarInfo>("PlayerTakenDamage", SetPlayerHpUI);
        EventCenter.GetInstance().AddEventListener<ValueBarInfo>("EnemyTakenDamage", SetEnemyHpUI);
    }

    private void SetEnemyHpUI(ValueBarInfo hpInfo)
    {
        enemyHpBar.SetValueBar(hpInfo);
    }

    private void SetPlayerHpUI(ValueBarInfo hpInfo)
    {
        playerHpBar.SetValueBar(hpInfo);
    }

    private void SetEnemyMagicUI(ValueBarInfo magicInfo)
    {
        
    }

    private void SetPlayerMagicUI(ValueBarInfo magicInfo)
    {
        
    }
    
    private void SetEnemyHUD(EnemySO enemySo)
    {
        enemyNameText.text = enemySo.fighterName;
        enemyLevel.text = $"Lv.{enemySo.level}";
        enemyHpBar.SetValueBar(new ValueBarInfo(enemySo.maxHp,enemySo.maxHp));
        enemyMagicBar.SetValueBar(new ValueBarInfo(enemySo.initMagic, enemySo.initMagic));
    }

    private void SetPlayerHUD(PlayerSO playerSo)
    {
        playerHpBar.SetValueBar(new ValueBarInfo(playerSo.currentHp, playerSo.maxHp));
        playerMagicBar.SetValueBar(new ValueBarInfo(playerSo.initMagic, playerSo.initMagic));
    }

    

    public void UpdateRemainTime(float remainTime)
    {
        remainTimeText.text = remainTime.ToString(String.Format("F0"));
    }
    




}
