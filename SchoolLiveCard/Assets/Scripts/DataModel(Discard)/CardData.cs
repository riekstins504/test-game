using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CardData
{
    protected CardType cardType;
    protected int cardID;
    protected string cardName;
    protected string introduction;
    public string getCardInfo()
    {
        return $"id:{cardID} type:{cardType} name:{cardName}";
    }
}
