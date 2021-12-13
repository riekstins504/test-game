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
    
    private void Start()
    {
        okBtn.onClick.AddListener(OkAction);
        EventCenter.GetInstance().AddEventListener("FoldPanelSeleteOneCard", () => 
        { 
            remainCount--; 
            UpdateDescriptionText();
        });
        EventCenter.GetInstance().AddEventListener("FoldPanelUnseleteOneCard", () =>
        {
            remainCount++;
            UpdateDescriptionText();
        });

        //InitDoTweenSequence();
    }



    private void OnEnable()
    {
        remainCount = BattleSystem.Instance.Player.handsCard.Count - 3;
        UpdateDescriptionText();
        GetCardsFromHand();
    }

    private void OnDisable()
    {
        
        ReturnCardsToHand();
    }
    
    // public bool IsSeletedEnough()
    // {
    //     return remainCount <= 0;
    // }

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
            // descriptionText.DOColor(Color.red, 0.2f);
            // descriptionText.DOColor(Color.white, 0.2f);
            // descriptionText.DOColor(Color.red, 0.2f);
            // descriptionText.DOColor(Color.white, 0.2f);
            // descriptionText.DOColor(Color.red, 0.2f);
            //StartCoroutine(TwinkleText(descriptionText));
            //descriptionText.color = Color.white;
            descriptionText.DOComplete();
            descriptionText.DOColor(Color.red, 0.5f).SetEase(Ease.Flash, 6, 0);
        }
    }
    
    // private void InitDoTweenSequence()
    // {
    //     if (sequence == null)
    //     {
    //         sequence = DOTween.Sequence();
    //         sequence.Append(descriptionText.DOColor(Color.red, 0.2f));
    //         sequence.Append(descriptionText.DOColor(Color.white, 0.2f));
    //         sequence.SetLoops(3);
    //     }
    // }

    // IEnumerator TwinkleText(Text text)
    // {
    //     text.DOColor(Color.red, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    //     text.DOColor(Color.white, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    //     text.DOColor(Color.red, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    //     text.DOColor(Color.white, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    //     text.DOColor(Color.red, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    //     text.DOColor(Color.white, 0.2f);
    //     yield return new WaitForSeconds(0.2f);
    // }
    
}
