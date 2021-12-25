using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get{ return _instance;}
    }

    [SerializeField]
    private CharacterFlowSO characterFlowConfig;
    public StoryFlow storyFlow;
    
    [SerializeField]
    private PlayerSO _playerConfig;
    private EnemySO _currentEnemyConfig;
    public PlayerSO PlayerConfig
    {
        get => _playerConfig;
    }
    public EnemySO CurrentEnemyConfig { 
        get=>_currentEnemyConfig;
        set => _currentEnemyConfig = value;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            //Debug.LogError("Get a second instance of GameManager class：" + this.GetType());
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    { 
        storyFlow = new StoryFlow(characterFlowConfig);
    }
    
}
