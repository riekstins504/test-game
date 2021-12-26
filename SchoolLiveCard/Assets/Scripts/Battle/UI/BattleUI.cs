using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform playCardZoneTrans;
    public RectTransform PlayCardZoneTrans
    {
        get => playCardZoneTrans;
    }

    [SerializeField]
    private Text remainTimeText;

    public PlayerHandUI playerHand;
    public GameObject enemyHand;

    [Header("弃牌面板")]
    public FoldPanelUI foldPanel;

    [Header("结算面板")] 
    public EndPanelUI endPanelUI;

    [Header("菜单面板")] 
    public Button menuBtn;
    public MenuPanelUI menuPanelUI;

    private void OnEnable()
    {
        menuBtn.onClick.AddListener(()=>{menuPanelUI.gameObject.SetActive(true);});
    }

    private void OnDisable()
    {
        menuBtn.onClick.RemoveAllListeners();
    }

    public IEnumerator PlayerDrawCardAnimation(List<GameObject> cardObjs)
    {
        float interval = 0.3f;
        int n = cardObjs.Count;
        float gap = Screen.width / (n-1) + 100f;
        for (int i = 0; i < n; i++)
        {
            RectTransform rt = cardObjs[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(800, 200);
            rt.DOAnchorPosX(-gap + i * gap, interval).SetEase(Ease.InQuint);
            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(0.5f);
    }

    
    public void UpdateRemainTime(float remainTime)
    {
        remainTimeText.text = remainTime.ToString(String.Format("F0"));
    }
    
}
