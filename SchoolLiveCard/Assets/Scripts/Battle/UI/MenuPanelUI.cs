using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanelUI : MonoBehaviour
{
    public Button backgroundBtn;
    public Button exitBtn;

    public void Start()
    {
        backgroundBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        
        exitBtn.onClick.AddListener(() =>
        {
            BattleSystem.currentState = BattleState.CLOSE;
            EventCenter.GetInstance().EventTrigger("ExitBattleField");
        });
    }
}
