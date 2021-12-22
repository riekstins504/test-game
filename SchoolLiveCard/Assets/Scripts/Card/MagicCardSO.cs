using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicCardConfig", menuName = "ScriptableObject/NewMagicCard")]
public class MagicCardSO : CardSO
{
    private CardType type = CardType.MagicCard;
    public CardType Type
    {
        get => type;
    }

    public int cost;
    public int damage;
    public GameObject magicCardPrefab;
}
