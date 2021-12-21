using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct HpChangeInfo
{
    public int curHp;
    public int maxHp;
    public int changedHp;//if >0 recover hp, if <0 reduce hp
};

public class ValueBarUI : MonoBehaviour
{
    public bool isShowMaxNum;
    public Text valueBarText;
    
    private Slider m_slider;
    private void Start()
    {
        m_slider = GetComponent<Slider>();
    }


    public void SetValueBar(int curValue, int maxValue)
    {
        if (isShowMaxNum)
        {
            valueBarText.text = $"{curValue}/{maxValue}";
        }
        else
        {
            valueBarText.text = $"{curValue}";
        }

        m_slider.maxValue = maxValue;
        m_slider.value = curValue;
    }
}
