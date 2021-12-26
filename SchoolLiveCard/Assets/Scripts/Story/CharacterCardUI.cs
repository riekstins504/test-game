using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class CharacterCardUI : MonoBehaviour,IPointerDownHandler
{
    public CharacterSO CurrentCharacter { private set; get; }

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


    private void Start()
    {
        //初始化Component
        rt = GetComponent<RectTransform>();
        originPos = rt.anchoredPosition;
        originColor = backgroundImage.color;

        Debug.Log("CharacterUI start");
    }

    private void OnEnable()
    {
        closeBtn.onClick.AddListener(() =>{ StartCoroutine(DoSkipAnimation()); });
        communicateBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
        EventCenter.GetInstance().AddEventListener<CharacterCardUI>("CharacterCardSelected",SelectedAction);
    }

    private void OnDisable()
    {
        communicateBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        EventCenter.GetInstance().RemoveEventListener<CharacterCardUI>("CharacterCardSelected",SelectedAction);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        EventCenter.GetInstance().EventTrigger<CharacterCardUI>("CharacterCardSelected",this);
    }
    
    public void SetCharacterUI(CharacterSO so)
    {
        CurrentCharacter = so;
        if (so == null)
        {
            Debug.LogWarning("传入的Character 配置为空");
            return;
        }
        
        if (so is EnemySO)
        {
            EnemySO enemySo = so as EnemySO;
            characterName.text = enemySo.enemyName;
            introduction.text = enemySo.introduction;
            communicateBtn.GetComponentInChildren<Text>().text = "战斗";
            communicateBtn.onClick.RemoveAllListeners();
            communicateBtn.onClick.AddListener(() =>
            {
                EventCenter.GetInstance().EventTrigger<EnemySO>("EnterBattleField", enemySo);

            });
        }
        else if(so is ShopSO)
        {
            ShopSO shopSO = so as ShopSO;
            characterName.text = shopSO.shopName;
            introduction.text = shopSO.introduction;
            communicateBtn.GetComponentInChildren<Text>().text = "看看";
            communicateBtn.onClick.RemoveAllListeners();
            communicateBtn.onClick.AddListener(() =>
            {
                EventCenter.GetInstance().EventTrigger<ShopSO>("EnterShop", shopSO);
            });
        }
    }

    public IEnumerator DoSkipAnimation()
    {
        yield return StartCoroutine(DoLeaveAnimation());
        CurrentCharacter = GameManager.Instance.storyFlow.NextCharacter(CurrentCharacter);
        if (CurrentCharacter != null)
        {
            SetCharacterUI(CurrentCharacter);
            yield return StartCoroutine(DoComeBackAnimation());
        }
    }

    public IEnumerator DoDestroyAnimation()
    {
        float interval = 1f;
        backgroundImage.DOBlendableColor(Color.red, interval);
        backgroundImage.DOBlendableColor(Color.clear,interval);
        rt.DOScale(new Vector3(0.3f, 0.3f, 0f), interval);
        yield return new WaitForSeconds(interval);
        
        CurrentCharacter = GameManager.Instance.storyFlow.NextCharacter(CurrentCharacter);
        if (CurrentCharacter != null)
        {
            backgroundImage.color = Color.black;
            rt.localScale = new Vector3(1f, 1f, 1f);
            SetCharacterUI(CurrentCharacter);
            yield return StartCoroutine(DoComeBackAnimation());
        }
    }
    
    public IEnumerator DoLeaveAnimation()
    {
        rt.anchoredPosition = originPos;
        closeBtn.enabled = false;
        DoSelect(false);
        rt.DOAnchorPosY(originPos.y - Screen.height*2f, 0.5f).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(1f);
    }
    
    public IEnumerator DoComeBackAnimation()
    {
        rt.anchoredPosition = originPos - new Vector2(0, Screen.height * 2f);
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
            if (!(CurrentCharacter is EnemySO))
            {
                closeBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            backgroundImage.color = originColor;
            communicateBtn.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        }
    }


}
