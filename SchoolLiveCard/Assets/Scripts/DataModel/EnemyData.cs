using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public int enemyID;
    public string enemyName;
    public int hp;
    public int magic;
    public int speed; //决定谁先手
    public string introduction;
    public List<int> cardBag;//只需保存CardID值
}
