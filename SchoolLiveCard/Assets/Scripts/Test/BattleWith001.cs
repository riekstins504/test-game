using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleWith001 : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(StartBattleWith001);
    }

    private void StartBattleWith001()
    {
        Debug.Log("Click the Button");
        // Enemy.data = EnemyLibrary.Instance.EnemyDict[1];
        SceneManager.LoadScene(1, LoadSceneMode.Single);//Default by Single
    }
}
