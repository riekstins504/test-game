using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ValueBarInfo
{
    public int curValue;
    public int maxValue;

    public ValueBarInfo(int _cur, int _max)
    {
        curValue = _cur;
        maxValue = _max;
    }
}

public class ValueBarUI : MonoBehaviour
{
    public bool isShowMaxNum;
    public Text valueBarText;
    
    private Slider m_slider;
    private void Start()
    {
        m_slider = GetComponent<Slider>();
    }


    public void SetValueBar(ValueBarInfo info)
    {
        if (isShowMaxNum)
        {
            valueBarText.text = $"{info.curValue}/{info.maxValue}";
        }
        else
        {
            valueBarText.text = $"{info.curValue}";
        }

        m_slider.maxValue = info.maxValue;
        m_slider.value = info.curValue;
    }
}
