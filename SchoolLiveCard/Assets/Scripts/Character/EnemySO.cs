using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigSO", menuName = "ScriptableObject/NewEnemyConfigSO")]
public class EnemySO : CharacterSO
{
    public Mesh enemyMesh;
    public string enemyName;
    public string introduction;
    public int level;
    public int maxHp;
    public int initMagic;
    public CardSO[] cardBag;
}
