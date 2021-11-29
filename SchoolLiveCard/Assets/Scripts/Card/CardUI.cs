using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class  CardUI : MonoBehaviour
{
    public Text cardName;
    public Text cardIntroduction;
    
    public Sprite sp;
    

    public virtual void UpdateUI()
    {
        // CardSO cardConfig = GetComponent<Card>().cardConfig;
        // cardName.text = cardConfig.cardName;
        // //cost.text = cardConfig.cost.ToString();
        // cardIntroduction.text = cardConfig.introduction;
    }
}
