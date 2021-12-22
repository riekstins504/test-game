using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string playerName;
    public int hp;
    public int magic;
    public int speed; //决定谁先手
    public List<int> cardBag;//只需保存CardID值
}
