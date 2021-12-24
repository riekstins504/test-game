using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterCardUI : MonoBehaviour,IPointerDownHandler
{
    private CharacterSO currentCharacter;

    public Text characterName;
    public Image characterImage;
    public Text introduction;
    public Button communicateBtn;
    public Button closeBtn;
    public Image backgroundImage;
    public Color onSelectedColor;

    private Color originColor;
    private RectTransform rt;
    private Vector2 originPos;

    private static int cacheIndex = 0; //仅在从缓存加载卡牌时使用，所以CharacterCardUI共享，反正单线程，不用管同步问题

    private IEnumerator Start()
    {
        //初始化Component
        rt = GetComponent<RectTransform>();
        originPos = rt.anchoredPosition;
        originColor = backgroundImage.color;
        closeBtn.onClick.AddListener(() =>{ StartCoroutine(NextCard()); });

        //从配置文件的Cache数组中加载之前的进度
        currentCharacter = GameManager.Instance.characterFlowConfig.LoadFromCache();
        //Debug.Log(cacheIndex);
        //cacheIndex++;
        if (currentCharacter != null)
        {
            rt.anchoredPosition += new Vector2(0, -Screen.height * 2f);
            InitUI(currentCharacter);
            yield return StartCoroutine(ComeBack());
        }
    }

    private void OnEnable()
    {
        communicateBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
        EventCenter.GetInstance().AddEventListener<CharacterCardUI>("CharacterCardSelected",SelectedAction);
    }

    private void OnDisable()
    {
        EventCenter.GetInstance().RemoveEventListener<CharacterCardUI>("CharacterCardSelected",SelectedAction);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        EventCenter.GetInstance().EventTrigger<CharacterCardUI>("CharacterCardSelected",this);
    }
    
    public void InitUI(CharacterSO so)
    {
        
        if (so is EnemySO)
        {
            EnemySO enemySo = so as EnemySO;
            characterName.text = enemySo.enemyName;
            introduction.text = enemySo.introduction;
            communicateBtn.GetComponentInChildren<Text>().text = "战斗";
        }
    }

    public IEnumerator NextCard()
    {
        yield return StartCoroutine(Leave());
        currentCharacter = GameManager.Instance.characterFlowConfig.NextCharacter(currentCharacter);
        if (currentCharacter != null)
        {
            InitUI(currentCharacter);
            yield return StartCoroutine(ComeBack());
        }
    }

    private IEnumerator Leave()
    {
        closeBtn.enabled = false;
        DoSelect(false);
        rt.DOAnchorPosY(originPos.y - Screen.height*2f, 0.5f).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(1f);
    }
    
    private IEnumerator ComeBack()
    {
        rt.DOAnchorPosY(originPos.y, 0.5f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.5f);
        closeBtn.enabled = true;
    }
    

    private void SelectedAction(CharacterCardUI characterCard)
    {
        if (characterCard == this)
        {
            DoSelect(true);
        }
        else
        {
            DoSelect(false);
        }
    }

    private void DoSelect(bool isToBeSelect)
    {
        if (isToBeSelect)
        {
            backgroundImage.color = onSelectedColor;
            communicateBtn.gameObject.SetActive(true);
            closeBtn.gameObject.SetActive(true);
        }
        else
        {
            backgroundImage.color = originColor;
            communicateBtn.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        }
    }


}
