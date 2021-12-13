using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selectable : MonoBehaviour,IPointerDownHandler
{
    private Image image;
    private Outline outline;

    private bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        image = GetComponent<Image>();
        outline = gameObject.AddComponent<Outline>();
        outline.effectColor = Color.white;
        outline.effectDistance = new Vector2(0f, 0f);
    }
    
    private void OnDestroy()
    {
        if (outline != null)
        {
            Destroy(outline);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isSelected)
        {
            Debug.Log("Select a card");
            BattleSystem.Instance.battleUI.foldPanel.selectCards.Add(gameObject);
            isSelected = true;
            outline.effectDistance = new Vector2(10f, 10f);
            EventCenter.GetInstance().EventTrigger("FoldPanelSeleteOneCard");
        }
        else
        {
            Debug.Log("Unselect a card");
            BattleSystem.Instance.battleUI.foldPanel.selectCards.Remove(gameObject);
            isSelected = false;
            outline.effectDistance = new Vector2(0f, 0f);
            EventCenter.GetInstance().EventTrigger("FoldPanelUnseleteOneCard");
        }
    }
    
    
}
