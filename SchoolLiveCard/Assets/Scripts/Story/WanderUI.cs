using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderUI : MonoBehaviour
{
    public CharacterCardUI[] characters = new CharacterCardUI[3];

     private IEnumerator Start()
     {
         for (int i = 0; i < 3; i++)
         {
             
             CharacterSO curCharacter = GameManager.Instance.storyFlow.cache[i];
             if (curCharacter != null)
             {
                 Debug.Log($"Card {i} 加载成功");
                 characters[i].SetCharacterUI(curCharacter);
                 StartCoroutine(characters[i].DoComeBackAnimation());
             }
             else
             {
                 Debug.Log($"Card {i} 在 Cache中为空，加载成功");
             }
         }
    
         if (BattleSystem.currentState == BattleState.WON)
         {
             for (int i = 0; i < 3; i++)
             {
                 if (GameManager.Instance.CurrentEnemyConfig == (characters[i].CurrentCharacter as EnemySO))
                 {
                     BattleSystem.currentState = BattleState.CLOSE;
                     yield return new WaitForSeconds(0.5f);
                     StartCoroutine(characters[i].DoDestroyAnimation());
                 }
             }
         }
    
     }
}
