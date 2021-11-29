using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardData : CardData
{
    protected int damage;
    protected int nextLevelCardID;//如果这张卡能强化，那么强化后的卡牌id为多少？
    public AttackCardData(int _cardid, string _name, int _damage)
    {
        cardID = _cardid;
        cardName = _name;
        damage = _damage;
    }
}
