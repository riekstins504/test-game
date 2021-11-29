using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardUI : CardUI
{
    public override void UpdateUI()
    {
        AttackCardSO cardConfig = GetComponent<AttackCard>().AttackCardSo;
        cardName.text = cardConfig.cardName;
        //cost.text = cardConfig.cost.ToString();
        cardIntroduction.text = cardConfig.introduction;
    }
}
