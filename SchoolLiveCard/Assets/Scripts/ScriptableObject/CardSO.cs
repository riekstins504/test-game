using System.Collections;
using System.Collections.Generic;using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

//[CreateAssetMenu(fileName = "CardConfigSO", menuName = "ScriptableObject/NewCardConfigSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public string introduction;
    public Sprite sp;
}



