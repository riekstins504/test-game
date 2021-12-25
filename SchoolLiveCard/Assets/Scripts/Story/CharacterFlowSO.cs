using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterFlowSO", menuName = "ScriptableObject/NewCharacterFlowConfig")]
public class CharacterFlowSO : ScriptableObject
{
    public List<CharacterSO> flow;

    public CharacterSO[] cache;

    public int curFlowIndex;
    //public List<CharacterSO> cache;



    // private void OnEnable()
    // {
    //     cacheIndex = 0;
    //     Debug.Log($"current cache index: {cacheIndex}");
    //     Debug.Log($"Flow index: {curIndex}");
    // }


}
