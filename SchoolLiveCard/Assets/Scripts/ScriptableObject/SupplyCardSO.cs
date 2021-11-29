using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SupplyCardSOConfig", menuName = "ScriptableObject/NewSupplyCardSO")]
public class SupplyCardSO : CardSO
{
    public int recoverMagicValue;
    public GameObject supplyCardPrefab;
}
