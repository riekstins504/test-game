using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{

    public Canvas wanderCanvas;
    
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        //EventCenter.GetInstance().AddEventListener("LoadBattleField",SwitchToBattleUI);
    }

    private void SwitchToBattleUI()
    {
        //wanderCanvas.enabled = false;
    }
}
