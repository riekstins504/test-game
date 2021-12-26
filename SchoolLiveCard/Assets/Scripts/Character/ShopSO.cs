using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopConfigSO", menuName = "ScriptableObject/NewShopConfigSO")]
public class ShopSO : CharacterSO
{
    public string shopName;
    public string introduction;

    public List<CardSO> goods;
}
