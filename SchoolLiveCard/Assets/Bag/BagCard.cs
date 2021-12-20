using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagCard : MonoBehaviour
{
    public CardSO data;
    public Text uiName;
    public Text uiDes;
    public Image uiImage;

    public void Init(CardSO data)
    {
        this.data = data;
        RefreshUI();
    }

    public void RefreshUI()
    {
        uiName.text = data.cardName;
        uiDes.text = data.introduction;
        uiImage.sprite = data.sp;
    }

}
