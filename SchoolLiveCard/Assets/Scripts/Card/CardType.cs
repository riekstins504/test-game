using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    AttackCard,//攻击卡，使对方造成伤害
    SupplyCard,//补给卡，如补血或补魔
    EquipmentCard,//装备卡，给予自身被动效果
    MagicCard,//魔法卡，消耗魔力
    CurseCard,//诅咒卡，给予对方debuff，如降低对方攻防，或者使对方在一定回合无法出牌
}