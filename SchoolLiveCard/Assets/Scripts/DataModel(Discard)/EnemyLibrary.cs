using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLibrary
{
    private static EnemyLibrary instance = new EnemyLibrary();//饿汉单例
    public static EnemyLibrary Instance
    {
        get => instance;
    }

    private Dictionary<int, EnemyData> enemyDict;
    public Dictionary<int, EnemyData> EnemyDict
    {
        get => enemyDict;
    }

    public EnemyLibrary()
    {
        enemyDict = new Dictionary<int, EnemyData>();
        //构造函数中从文件中加载卡牌库
        LoadTestEnemies();
        //LoadEnemiesFromFile();
    }

    private void LoadEnemiesFromFile()
    {
        //以ID作为Dictionary的Key，若Key重复，说明ID重复，需要报错
    }
    
    private void LoadTestEnemies()
    {
        EnemyData enemy001 = new EnemyData();
        enemy001.hp = 100;
        enemy001.magic = 10;
        enemy001.enemyName = "怪物001";
        enemy001.enemyID = 1;
        enemy001.speed = 1;
        enemy001.introduction = "测试用怪物";
        enemy001.cardBag = new List<int>() {1, 2, 3};
        enemyDict.Add(enemy001.enemyID, enemy001);
        Debug.Log("怪物001创建完毕");
    }
}
