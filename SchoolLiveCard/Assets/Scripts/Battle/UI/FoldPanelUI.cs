using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using DG.Tweening;

public class FoldPanelUI : MonoBehaviour
{
    public GameObject content;
    public Button okBtn;
    public Text descriptionText;
    
    public static bool isDone;
    
    private int remainCount;//还需要齐多少张牌
    private Sequence sequence;

    //private List<GameObject> cards;
    [HideInInspector]
    public List<GameObject> selectCards;
    

    private void OnEnable()
    {
        okBtn.onClick.AddListener(OkAction);
        EventCenter.GetInstance().AddEventListener("FoldPanelSeleteOneCard", DoSelect);
        EventCenter.GetInstance().AddEventListener("FoldPanelUnseleteOneCard", DoUnSelect);
        
        remainCount = BattleSystem.Instance.Player.handsCard.Count - 3;
        UpdateDescriptionText();
        GetCardsFromHand();
    }

    private void OnDisable()
    {
        okBtn.onClick.RemoveListener(OkAction);
        EventCenter.GetInstance().RemoveEventListener("FoldPanelSeleteOneCard", DoSelect);
        EventCenter.GetInstance().RemoveEventListener("FoldPanelUnseleteOneCard", DoUnSelect);
        
        ReturnCardsToHand();
    }

    private void DoSelect()
    {
        remainCount--; 
        UpdateDescriptionText();
    }

    private void DoUnSelect()
    {
        remainCount++;
        UpdateDescriptionText();
    }

    private void UpdateDescriptionText()
    {
        descriptionText.text = $"至少还需要弃 {Math.Max(remainCount, 0)} 张牌";
    }


    private void GetCardsFromHand()
    {
        foreach (GameObject card in BattleSystem.Instance.Player.handsCard)
        {
            card.transform.SetParent(content.transform);
            card.AddComponent<Selectable>();
        }
    }
    
    private void ReturnCardsToHand()
    {
        foreach (GameObject card in BattleSystem.Instance.Player.handsCard)
        {
            card.transform.SetParent(BattleSystem.Instance.battleUI.playerHand.transform);
            Destroy(card.GetComponent<Selectable>());
        }
    }

    private void OkAction()
    {
        if (remainCount <= 0)
        {
            foreach (GameObject card in selectCards)
            {
                BattleSystem.Instance.Player.handsCard.Remove(card);
                Destroy(card);
            }
            selectCards.Clear();
            isDone = true;
            gameObject.SetActive(false);
        }
        else
        {
            descriptionText.DOComplete();
            descriptionText.DOColor(Color.red, 0.5f).SetEase(Ease.Flash, 6, 0);
        }
    }
    

    
}
