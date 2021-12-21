using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TestSomething : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ShakeCamera);
    }

    // Update is called once per frame
    void ShakeCamera()
    {
        Camera.main.DOShakePosition(0.5f, 0.1f).SetEase(Ease.InFlash);
        
    }
}
