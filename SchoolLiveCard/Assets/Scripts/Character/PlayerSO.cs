using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "ScriptableObject/NewPlayerConfigSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    public int level;
    public int currentHp;
    public int maxHp;
    public int initMagic;
    public CardSO[] cardBag;
    public int Gold;
}
