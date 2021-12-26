using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandUI : MonoBehaviour
{
    //private List<GameObject> cards;
    public Transform leftPoint;
    public Transform rightPoint;
    
    private void OnEnable()
    {
        EventCenter.GetInstance().AddEventListener("PlayerDrawCardFinish", UpdateHandUI);
        EventCenter.GetInstance().AddEventListener("PlayerFoldCardFinish", UpdateHandUI);
        EventCenter.GetInstance().AddEventListener("PlayerPlayCardFinish", UpdateHandUI);
    }

    private void OnDisable()
    {
        EventCenter.GetInstance().RemoveEventListener("PlayerDrawCardFinish", UpdateHandUI);
        EventCenter.GetInstance().RemoveEventListener("PlayerFoldCardFinish", UpdateHandUI);
        EventCenter.GetInstance().RemoveEventListener("PlayerPlayCardFinish", UpdateHandUI);
    }

    private void UpdateHandUI()
    {
        List<GameObject> cards = BattleSystem.Instance.Player.handsCard;
        // foreach (GameObject card in cards)
        // {
        //     card.transform.SetParent(transform);
        // }

        int count = cards.Count;
        if (count == 1)
        {
            Vector3 distance = rightPoint.position - leftPoint.position;
            cards[0].transform.position = leftPoint.position + distance / 2f;
            cards[0].transform.localScale = Vector3.one;
        }
        if (count > 1)
        {
            Vector3 distance = rightPoint.position - leftPoint.position;
            Vector3 intervalDistance = distance / (count - 1);
            for (int i = 0; i < count; i++)
            {
                cards[i].transform.position = leftPoint.position + i * intervalDistance;
                cards[i].transform.localScale = Vector3.one;
            }
        }
    }
    
    
}
