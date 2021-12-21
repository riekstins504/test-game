using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DamageEffectUI : MonoBehaviour
{
    public Image redForeground;
    public Text damageValueTextPrefab;
    public RectTransform playerDamageTextPos;
    public RectTransform enemyDamageTextPos;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", ShakeCamera);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("PlayerTakenDamage", ShowPlayerDamageText);
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", ShowEnemyDamageText);
    }

    private void ShowPlayerDamageText(HpChangeInfo info)
    {
        Text textCom = Instantiate(damageValueTextPrefab,transform).GetComponent<Text>();
        textCom.text = info.changedHp.ToString();
        textCom.rectTransform.position = playerDamageTextPos.position;
        textCom.rectTransform.DOMoveY(playerDamageTextPos.position.y + 35f, 0.5f).SetEase(Ease.OutQuint)
            .OnStepComplete(() => { Destroy(textCom.gameObject);});
    }
    
    private void ShowEnemyDamageText(HpChangeInfo info)
    {
        Text textCom = Instantiate(damageValueTextPrefab,transform).GetComponent<Text>();
        textCom.text = info.changedHp.ToString();
        textCom.rectTransform.position = enemyDamageTextPos.position;
        textCom.rectTransform.DOMoveY(enemyDamageTextPos.position.y + 35f, 0.5f).SetEase(Ease.OutQuint)
            .OnStepComplete(() => { Destroy(textCom.gameObject);});
    }

    private void ShakeCamera(HpChangeInfo arg0)
    {
        Camera.main.DOShakePosition(0.5f, 0.1f).SetEase(Ease.InFlash);
        redForeground.color = new Color(1f, 0f, 0f, 0.35f);
        redForeground.DOColor(Color.clear, 0.5f);
    }
}
