using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG;
using DG.Tweening;

public class DragableCard : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Vector3 originPos;
    private Vector3 offset;
    private RectTransform rt;
    private Vector3 originScale;
    private Vector3 draggingScale;

    private Card m_Card;

    
    private void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        originScale = rt.localScale;
        draggingScale = rt.localScale * 1.5f;

        m_Card = gameObject.GetComponent<Card>();
    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = rt.position;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera,
            out globalMousePos))
        {
            offset = globalMousePos - rt.position;
            rt.localScale = draggingScale;
            //Debug.Log($"Begin: offset{offset}, mouse{globalMousePos}");
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera,
            out globalMousePos))
        {
            rt.position = globalMousePos - offset;
            //Debug.Log($"rt.position{rt.position} mouse{globalMousePos} offset{offset}");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform playCardZone = BattleSystem.Instance.battleUI.PlayCardZoneTrans;
        Vector3 localMousePos = playCardZone.InverseTransformPoint(Input.mousePosition);
        if (playCardZone.rect.Contains(localMousePos))
        {
            //BattleSystem.Instance.SetCardPlayerPlay(this.gameObject);
            BattleSystem.currentPlayerCard = this.gameObject;
        }
        else
        {
            float interval = 0.2f;
            rt.DOMove(originPos,interval);
            rt.DOScale(originScale, interval);
        }
        
    }

}
