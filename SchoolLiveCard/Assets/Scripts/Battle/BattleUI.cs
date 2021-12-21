using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public Image redImage;

    [Header("Enemy HUD")] 
    public Text enemyNameText;
    public Text enemyLevel;
    public ValueBarUI enemyHpBar;
    public ValueBarUI enemyMagicBar;

    [Header("Player HUD")] 
    public ValueBarUI playerHpBar;
    public ValueBarUI playerMagicBar;
    public FoldPanelUI foldPanel;

    [Header("Damage UI")] 
    public Text damageValueText;
    public RectTransform playerDamageTextPos;
    public RectTransform enemyDamageTextPos;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<PlayerSO>("PlayerEnterBattle", SetPlayerHUD);
        EventCenter.GetInstance().AddEventListener<EnemySO>("EnemyEnterBattle", SetEnemyHUD);
        
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", ShakeCamera);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", SetPlayerHpUI);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", ShowPlayerDamageText);
        
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", SetEnemyHpUI);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", ShowEnemyDamageText);
    }

    
    private void ShowPlayerDamageText(HpChangeInfo info)
    {
        Text textCom = Instantiate(damageValueText,transform).GetComponent<Text>();
        textCom.text = info.changedHp.ToString();
        textCom.rectTransform.position = playerDamageTextPos.position;
        textCom.rectTransform.DOMoveY(playerDamageTextPos.position.y + 35f, 0.5f).SetEase(Ease.OutQuint)
            .OnStepComplete(() => { Destroy(textCom.gameObject);});
    }
    
    private void ShowEnemyDamageText(HpChangeInfo info)
    {
        Text textCom = Instantiate(damageValueText,transform).GetComponent<Text>();
        textCom.text = info.changedHp.ToString();
        textCom.rectTransform.position = enemyDamageTextPos.position;
        textCom.rectTransform.DOMoveY(enemyDamageTextPos.position.y + 35f, 0.5f).SetEase(Ease.OutQuint)
            .OnStepComplete(() => { Destroy(textCom.gameObject);});
    }

    private void ShakeCamera(HpChangeInfo arg0)
    {
        Camera.main.DOShakePosition(0.5f, 0.1f).SetEase(Ease.InFlash);
        redImage.color = new Color(1f, 0f, 0f, 0.35f);
        redImage.DOColor(Color.clear, 0.5f);
    }


    private void SetEnemyHpUI(HpChangeInfo hpInfo)
    {
        enemyHpBar.SetValueBar(hpInfo.curHp,hpInfo.maxHp);
    }

    private void SetPlayerHpUI(HpChangeInfo hpInfo)
    {
        playerHpBar.SetValueBar(hpInfo.curHp,hpInfo.maxHp);
    }

    private void SetEnemyMagicUI(HpChangeInfo magicInfo)
    {
        
    }

    private void SetPlayerMagicUI(HpChangeInfo magicInfo)
    {
        
    }
    
    private void SetEnemyHUD(EnemySO enemySo)
    {
        enemyNameText.text = enemySo.fighterName;
        enemyLevel.text = $"Lv.{enemySo.level}";
        HpChangeInfo info;
        enemyHpBar.SetValueBar(enemySo.maxHp,enemySo.maxHp);
        enemyMagicBar.SetValueBar(enemySo.initMagic, enemySo.initMagic);
    }

    private void SetPlayerHUD(PlayerSO playerSo)
    {
        playerHpBar.SetValueBar(playerSo.currentHp, playerSo.maxHp);
        playerMagicBar.SetValueBar(playerSo.initMagic, playerSo.initMagic);
    }

    

    public void UpdateRemainTime(float remainTime)
    {
        remainTimeText.text = remainTime.ToString(String.Format("F0"));
    }
    




}
