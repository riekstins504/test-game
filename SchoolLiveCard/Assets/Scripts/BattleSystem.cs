using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
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
    
    private Player player;
    private Enemy enemy;
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


    // public void SetCardPlayerPlay(GameObject card)
    // {
    //     currentPlayerCard = card;
    // }


    private void Start()
    {
        battleUI.endRoundBtn.onClick.AddListener(delegate { isPlayerEndAction = true; });
        
        currentState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        //实例化Player和Enemy
        player = Instantiate(playerPrefab).GetComponent<Player>();
        enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        
        player.LoadDataFromSO();
        enemy.LoadDataFromSO();

        //更新UI
        //battleUI.SetEnemyUI(enemy.enemyConfig.fighterName, enemy.enemyConfig.level);
        EventCenter.GetInstance().EventTrigger<PlayerSO>("PlayerEnterBattle", player.playerConfig);
        EventCenter.GetInstance().EventTrigger<EnemySO>("EnemyEnterBattle", enemy.enemyConfig);
        
        yield return new WaitForSeconds(1f);
        currentState = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Player Turn");
        
        
        //抽牌
        for (int i = 0; i < 5; i++)
        {
            PlayerDrawCard(GenerateCard);
        }
        
        //Make card dragable
        SetCardDragable(true);
        
        //打牌
        currentPlayerCard = null;
        isPlayerEndAction = false;
        while(!isPlayerEndAction)//等待玩家结束回合
        {
            if (currentPlayerCard != null)//如果玩家打牌
            {
                //对这张牌做一些操作
                PlayerPlayCard();
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
        while(!isEnemyEndAction)//等待敌人结束回合
        {
            //敌人AI
            if (enemy.ChooseCardToPlay())//There are not cards in enemy's hand
            {
                isEnemyEndAction = true;
            }
            
            if (currentEnemyCard != null)
            {
                //对这张牌做一些操作
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
            cardObj =  Instantiate(attackCardPrefab);
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

    private void PlayerDrawCard(Func<CardSO,GameObject> generateCardFunc)
    {
        CardSO cardData = player.DrawOneCard();
        GameObject cardObj = generateCardFunc(cardData);
        cardObj.AddComponent<DragableCard>().enabled = false;
        player.handsCard.Add(cardObj);
        battleUI.playerHand.AddCard(cardObj);
    }

    private void EnemyDrawCard(Func<CardSO,GameObject> generateCardFunc)
    {
        CardSO cardData = enemy.DrawOneCard();
        GameObject cardObj = generateCardFunc(cardData);
        enemy.handsCard.Add(cardObj);
        //battleUI.enemyHand.AddCard(cardObj);
    }

    private void PlayerPlayCard()
    {
        player.handsCard.Remove(currentPlayerCard);
        battleUI.playerHand.RemoveCard(currentPlayerCard);
        
        switch (currentPlayerCard.tag)
        {
            case "AttackCard":
                Debug.Log("Player打出一张AttackCard");
                bool isEnemyDeath = currentPlayerCard.GetComponent<AttackCard>().DoAttack(enemy);
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
        enemy.handsCard.Remove(currentEnemyCard);
        //battleUI.enemyHand.RemoveCard(currentPlayerCard);
        
        switch (currentEnemyCard.tag)
        {
            case "AttackCard":
                Debug.Log("Enemy打出一张AttackCard");
                bool isPlayerDeath = currentEnemyCard.GetComponent<AttackCard>().DoAttack(player);
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
        foreach (var card in player.handsCard)
        {
            card.GetComponent<DragableCard>().enabled = isDragable;
        }
    }
        
    



}
