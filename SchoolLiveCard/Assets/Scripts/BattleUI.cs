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
    public Text enemyHpText;
    public Text enemyMagicText;
    public Image enemyHpBar;
    public Image enemyMagicBar;

    [Header("Player HUD")] 
    public Text playerHpText;
    public Text playerMagicText;
    public Image playerHpBar;
    public Image playerMagicbar;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<PlayerSO>("PlayerEnterBattle", SetPlayerHUD);
        EventCenter.GetInstance().AddEventListener<EnemySO>("EnemyEnterBattle", SetEnemyHUD);
        
        EventCenter.GetInstance().AddEventListener<HpInfo>("PlayerTakenDamage", SetPlayerHp);
        EventCenter.GetInstance().AddEventListener<HpInfo>("EnemyTakenDamage", SetEnemyHp);
    }

    void Start()
    {


    }

    private void SetEnemyHUD(EnemySO enemySo)
    {
        enemyNameText.text = enemySo.fighterName;
        enemyLevel.text = $"Lv.{enemySo.level}";
        enemyHpText.text = $"{enemySo.maxHp}/{enemySo.maxHp}";
        enemyMagicText.text = enemySo.initMagic.ToString();
    }

    private void SetPlayerHUD(PlayerSO playerSo)
    {
        SetPlayerHp(new HpInfo(playerSo.currentHp, playerSo.maxHp));
        playerMagicText.text = playerSo.initMagic.ToString();
    }


    public void UpdateRemainTime(float remainTime)
    {
        remainTimeText.text = remainTime.ToString(String.Format("F0"));
    }

    private void SetEnemyUI(string _enemyName, int _level)
    {

    }

    private void SetPlayerHp(HpInfo hpInfo)
    {
        playerHpText.text = $"{hpInfo.curhp}/{hpInfo.maxhp}";
    }
    
    private void SetEnemyHp(HpInfo hpInfo)
    {
        enemyHpText.text = $"{hpInfo.curhp}/{hpInfo.maxhp}";
    }


}
