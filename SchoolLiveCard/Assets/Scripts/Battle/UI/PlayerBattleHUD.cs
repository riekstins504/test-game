using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    [Header("Player HUD")] 
    public ValueBarUI playerHpBar;
    public ValueBarUI playerMagicBar;
    public Button endActionBtn;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<PlayerSO>("PlayerEnterBattle", InitPlayerHUD);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", SetPlayerHpUI);
    }

    private void Start()
    {
        endActionBtn.onClick.AddListener(BattleSystem.Instance.EndPlayerAction);
    }

    private void InitPlayerHUD(PlayerSO playerSo)
    {
        playerHpBar.SetValueBar(playerSo.currentHp, playerSo.maxHp);
        playerMagicBar.SetValueBar(playerSo.initMagic, playerSo.initMagic);
    }
    
    private void SetPlayerHpUI(HpChangeInfo hpInfo)
    {
        playerHpBar.SetValueBar(hpInfo.curHp,hpInfo.maxHp);
    }
    
    private void SetPlayerMagicUI(HpChangeInfo magicInfo)
    {
        
    }
}
