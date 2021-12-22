using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMono : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<HpChangeInfo>("EnemyTakenDamage", TakenAttackAnimation);
    }

    private void TakenAttackAnimation(HpChangeInfo arg0)
    {
        transform.DOShakePosition(0.3f, 0.1f).SetEase(Ease.InFlash);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
