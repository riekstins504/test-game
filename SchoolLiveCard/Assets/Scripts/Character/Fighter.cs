using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// public enum FighterType
// {
//     Player,
//     Enemy,
// }

public class Fighter : MonoBehaviour
{
    // public string fighterName;
    // public int hp;
    // public int magic;
    // public string introduction;


    private void Start()
    {
        
    }


    // public virtual List<CombatCard> DrawCard(int count)
    // {
    //     if (handsCard == null)
    //     {
    //         handsCard = new List<CombatCard>();
    //     }
    //     //抽卡
    //     List<CombatCard> newCards = new List<CombatCard>();
    //     for (int i = 0; i < count; i++)
    //     {
    //         int cardID = cardBag[Random.Range(0, cardBag.Count)];
    //         CombatCard combatCard = Instantiate(BattleSystem.Instance.combatCardPrefabs).GetComponent<CombatCard>();
    //         combatCard.data = CardLibrary.Instance.CardDict[cardID];
    //         newCards.Add(combatCard);
    //         handsCard.Add(combatCard);
    //     }
    //     return newCards;
    // }
    //
    // public virtual void PlayCard(CombatCard card)
    // {
    //     //Debug.Log($"{fighterName} play the card {card.data.getCardInfo()}");
    //     handsCard.Remove(card);
    // }

    // protected virtual void LoadSelfData()
    // {
    //     Debug.Log("LoadSelfData");
    // }
    //
    // protected virtual void UpdateSelfData()
    // {
    //     Debug.Log("UpdateSelfData");
    // }
    //
    // protected virtual void SaveSelfData()
    // {
    //     Debug.Log("SaveSelfData");
    // }

}

// public class DrawCardEventArgs : EventArgs
// {
//     public List<CombatCard> Cards { get; private set; }
//
//     public DrawCardEventArgs(List<CombatCard> _cards)
//     {
//         Cards = _cards;
//     }
// }

// public class PlayCardEventArgs : EventArgs
// {
//     public CombatCard card;
//     public PlayCardEventArgs(CombatCard _card)
//     {
//         card = _card;
//     }
// }

