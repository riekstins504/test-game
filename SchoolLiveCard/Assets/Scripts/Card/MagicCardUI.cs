using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCardUI : CardUI
{
    public Text cost;

    public override void UpdateUI()
    {
        MagicCardSO cardConfig = GetComponent<MagicCard>().magicCardSo;
        cardName.text = cardConfig.cardName;
        cost.text = cardConfig.cost.ToString();
        cardIntroduction.text = cardConfig.introduction;
    }
}
