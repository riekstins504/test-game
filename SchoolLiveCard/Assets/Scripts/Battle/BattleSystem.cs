using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Unity.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//负责管理双方的对战，比如把一方的牌传给另一方
//控制每个回合，包括清点双方debuff，发牌。

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    [Header("卡牌预制体")]
    public GameObject attackCardPrefab;
    public GameObject magicCardPrefab;

    public BattleUI battleUI;
    
    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }

    private BattleState currentState;
    private bool isPlayerEndAction = false;
    private bool isEnemyEndAction = false;
    

    private static BattleSystem _instance;
    public static BattleSystem Instance
    {
        get => _instance;
    }

    public static GameObject currentPlayerCard;
    public static GameObject currentEnemyCard;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void EndPlayerAction()
    {
        isPlayerEndAction = true;
    }
    

    private void Start()
    {

        currentState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        //实例化Player和Enemy

        Player = Instantiate(playerPrefab).GetComponent<Player>();
        Enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        
        Player.LoadDataFromSO();
        Enemy.LoadDataFromSO();

        //更新UI
        //battleUI.SetEnemyUI(enemy.enemyConfig.fighterName, enemy.enemyConfig.level);
        EventCenter.GetInstance().EventTrigger<PlayerSO>("PlayerEnterBattle", Player.playerConfig);
        EventCenter.GetInstance().EventTrigger<EnemySO>("EnemyEnterBattle", Enemy.enemyConfig);
        
        yield return new WaitForSeconds(0.1f);
        currentState = BattleState.PLAYERTURN;

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Player Turn");

        
        //检测手牌是否大于3，如果大于，要求丢弃一定的牌
        if (Player.handsCard.Count > 3)
        {
            FoldPanelUI.isDone = false;
            battleUI.foldPanel.gameObject.SetActive(true);
            while (!FoldPanelUI.isDone)
            {
                yield return null;//挂起
            }
            EventCenter.GetInstance().EventTrigger("PlayerFoldCardFinish");
        }

        //抽牌
        List<GameObject> cardObjects = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            cardObjects.Add(PlayerDrawCard(GenerateCard));
        }
        //播放抽牌动画
        yield return StartCoroutine(battleUI.PlayerDrawCardAnimation(cardObjects));
        
        EventCenter.GetInstance().EventTrigger("PlayerDrawCardFinish");
        
        //Make card dragable
        SetCardDragable(true);

        //打牌
        currentPlayerCard = null;
        isPlayerEndAction = false;
        while (!isPlayerEndAction)//等待玩家结束回合
        {
            if (currentPlayerCard != null)//如果玩家打牌
            {
                //对这张牌做一些操作
                PlayerPlayCard();
                EventCenter.GetInstance().EventTrigger("PlayerPlayCardFinish");
            }

            yield return null;
        }

        //Make card undragable
        SetCardDragable(false);

        currentState = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");

        //抽牌
        for (int i = 0; i < 3; i++)
        {
            EnemyDrawCard(GenerateCard);
        }

        isEnemyEndAction = false;
        currentEnemyCard = null;
        //打牌
        while (!isEnemyEndAction)//等待敌人结束回合
        {
            //敌人AI
            if (Enemy.ChooseCardToPlay())//There are not cards in enemy's hand
            {
                isEnemyEndAction = true;
            }

            if (currentEnemyCard != null)
            {
                //对这张牌做一些操作
                float interval = 0.5f;
                SystemShowEnemyCurrentCard(interval);
                yield return new WaitForSeconds(interval + 1f);
                EnemyPlayCard();
            }
        }

        yield return new WaitForSeconds(2f);
        currentState = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private void EndBattle()
    {
        StopAllCoroutines();
        SetCardDragable(false);
        Debug.Log("战斗结束");
    }

    private GameObject GenerateCard(CardSO cardData)
    {
        GameObject cardObj;

        if (cardData is AttackCardSO)
        {
            cardObj = Instantiate(attackCardPrefab);
            AttackCard card = cardObj.GetComponent<AttackCard>();
            card.AttackCardSo = cardData as AttackCardSO;
            cardObj.GetComponent<AttackCardUI>()?.UpdateUI();
        }
        else if (cardData is MagicCardSO)
        {
            cardObj = Instantiate(magicCardPrefab);
            MagicCard card = cardObj.GetComponent<MagicCard>();
            card.magicCardSo = cardData as MagicCardSO;
            cardObj.GetComponent<MagicCardUI>()?.UpdateUI();
        }
        //else if 其他卡
        else
        {
            Debug.LogError("抽卡时，卡牌实例化出错");
            return null;
        }

        return cardObj;
    }

    private void SystemShowEnemyCurrentCard(float interval)
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        currentEnemyCard.transform.position = screenCenter + Vector2.up * 200f;
        currentEnemyCard.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        currentEnemyCard.transform.DOMove(screenCenter, interval)
            .SetEase(Ease.InOutQuint);
        currentEnemyCard.transform.DOScale(new Vector3(1.5f,1.5f,1), interval)
            .SetEase(Ease.OutQuint);
    }

    private GameObject PlayerDrawCard(Func<CardSO, GameObject> generateCardFunc)
    {
        CardSO cardData = Player.DrawOneCard();
        GameObject cardObj = generateCardFunc(cardData);
        cardObj.AddComponent<DragableCard>().enabled = false;
        Player.handsCard.Add(cardObj);
        //battleUI.playerHand.AddCard(cardObj);
        cardObj.transform.SetParent(battleUI.playerHand.transform);
        cardObj.transform.localScale = Vector3.one;
        return cardObj;
    }

    private void EnemyDrawCard(Func<CardSO, GameObject> generateCardFunc)
    {
        CardSO cardData = Enemy.DrawOneCard();
        GameObject cardObj = generateCardFunc(cardData);
        Enemy.handsCard.Add(cardObj);
        cardObj.transform.SetParent(battleUI.enemyHand.transform,false);
        //battleUI.enemyHand.AddCard(cardObj);
    }

    private void PlayerPlayCard()
    {
        Player.handsCard.Remove(currentPlayerCard);
        //battleUI.playerHand.RemoveCard(currentPlayerCard);
        

        switch (currentPlayerCard.tag)
        {
            case "AttackCard":
                Debug.Log("Player打出一张AttackCard");
                bool isEnemyDeath = currentPlayerCard.GetComponent<AttackCard>().DoAttack(Enemy);
                if (isEnemyDeath)
                {
                    currentState = BattleState.WON;
                    EndBattle();
                }
                break;
            case "MagicCard":
                Debug.Log("Player打出一张MagicCard");
                break;
            default:
                Debug.LogError("Player打出的牌，类型古怪");
                break;
        }

        Destroy(currentPlayerCard);
        currentPlayerCard = null;
    }

    private void EnemyPlayCard()
    {
        Enemy.handsCard.Remove(currentEnemyCard);

        switch (currentEnemyCard.tag)
        {
            case "AttackCard":
                Debug.Log("Enemy打出一张AttackCard");
                bool isPlayerDeath = currentEnemyCard.GetComponent<AttackCard>().DoAttack(Player);
                if (isPlayerDeath)
                {
                    currentState = BattleState.LOST;
                    EndBattle();
                }
                break;
            case "MagicCard":
                Debug.Log("Enemy打出一张MagicCard");
                break;
            default:
                Debug.LogError("Enemy打出的牌，类型古怪");
                break;
        }

        Destroy(currentEnemyCard);
        currentEnemyCard = null;
    }


    private void SetCardDragable(bool isDragable)
    {
        foreach (var card in Player.handsCard)
        {
            card.GetComponent<DragableCard>().enabled = isDragable;
        }
    }





}
