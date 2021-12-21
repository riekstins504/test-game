using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanelUI : MonoBehaviour
{
    public Button okBtn;
    public Text endText;

    public void Start()
    {
        okBtn.onClick.AddListener(() =>
        {
            EventCenter.GetInstance().EventTrigger("ExitBattleField");
        });
    }

    public void SetText(BattleState state)
    {
        if (state == BattleState.WON)
        {
            endText.text = "战斗胜利！";
        }
        else
        {
            endText.text = "战斗失败……";
        }
    }
}
