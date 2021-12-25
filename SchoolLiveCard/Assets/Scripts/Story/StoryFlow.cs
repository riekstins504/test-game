using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFlow
{
    private CharacterFlowSO flowConfig;
    
    public CharacterSO[] cache = new CharacterSO[3];
    private List<CharacterSO> flow = new List<CharacterSO>();
    private int curFlowIndex;
    
    public StoryFlow(CharacterFlowSO _flowConfig)
    {
        flowConfig = _flowConfig;
        //浅拷贝
        foreach (var character in _flowConfig.flow)
        {
            flow.Add(character);
        }

        if (_flowConfig.cache.Length < 3)//新游戏，第一次加载
        {
            _flowConfig.cache = new CharacterSO[3];
            curFlowIndex = -1;   
            for (int i = 0; i < 3 && i < flow.Count; i++)
            {
                cache[i] = flow[i];
                _flowConfig.cache[i] = flow[i];
                curFlowIndex++;
            }
            SaveToConfigFile();
        }
        else//已经有记录，加载之前的Cache
        {
            for (int i = 0; i < 3; i++)
            {
                cache[i] = _flowConfig.cache[i];
            }
            curFlowIndex = flowConfig.curFlowIndex;
        }

    }

    public void SaveToConfigFile()
    {
        for (int i = 0; i < 3; i++)
        {
            flowConfig.cache[i] = cache[i];
        }

        flowConfig.curFlowIndex = curFlowIndex;
    }

    public CharacterSO NextCharacter(CharacterSO curCharacter)
    {
        CharacterSO nextCharacter = null;
        if (curFlowIndex + 1 < flow.Count)
        {
            nextCharacter = flow[curFlowIndex + 1];
        }
        
        for (int i = 0; i < 3; i++)
        {
            if (cache[i] == curCharacter)
            {
                cache[i] = nextCharacter;
                curFlowIndex++;
                SaveToConfigFile();
                return nextCharacter;
            }
        }
        Debug.LogWarning("传入的Character配置与Cache数组中的所有元素都不匹配");
        
        return null;
    }
    
    // public CharacterSO LoadFromCache()
    // {
    //     if (cache.Count == 0)//若容器为空，则初始化
    //     {
    //         for (int i = 0; i < 3 && i < flow.Count - 1; i++)
    //         {
    //             cache.Add(flow[i]);//把前三个加入
    //         }
    //         curFlowIndex = cache.Count - 1;
    //     }
    //     
    //     //根据cacheIndex，从Cache容器中返回对象
    //     if (cacheIndex > cache.Count - 1)
    //     {
    //         return null;
    //     }
    //     CharacterSO curCharacter = cache[cacheIndex];
    //     //Debug.Log($"current cache index: {cacheIndex}");
    //     cacheIndex++;
    //     Debug.Log(curCharacter.name);
    //     return curCharacter;
    // }

    // public CharacterSO NextCharacter(CharacterSO curCharacter)
    // {
    //     if (curIndex < charactersFlow.Count - 1)
    //     {
    //         cache.Remove(curCharacter);
    //         curIndex++;
    //         cache.Add(charactersFlow[curIndex]);
    //         Debug.Log($"Flow index: {curIndex}");
    //         return charactersFlow[curIndex];
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }

}
