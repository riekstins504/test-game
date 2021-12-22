using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCardConfig", menuName = "ScriptableObject/NewAttackCard")]
public class AttackCardSO : CardSO
{
    private CardType type = CardType.AttackCard;
    public CardType Type
    {
        get => type;
    }
    public int damage;
    public GameObject attackCardPrefab;
}
