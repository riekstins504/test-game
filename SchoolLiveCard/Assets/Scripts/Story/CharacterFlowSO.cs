using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterFlowSO", menuName = "ScriptableObject/NewCharacterFlowConfig")]
public class CharacterFlowSO : ScriptableObject
{
    public List<CharacterSO> charactersFlow;
    
    public List<CharacterSO> cache;

    public int curIndex = 0;

    private static int cacheIndex = 0;

    private void OnEnable()
    {
        cacheIndex = 0;
        Debug.Log($"current cache index: {cacheIndex}");
        Debug.Log($"Flow index: {curIndex}");
    }

    public CharacterSO LoadFromCache()
    {
        //if(cache == null) //不能直接这样判断，ScriptableObject由Unity引擎初始化，所以它并不会为null
        if (cache.Count == 0)//若容器为空，则初始化
        {
            cache = new List<CharacterSO>();
            for (int i = 0; i < 3 && i < charactersFlow.Count - 1; i++)
            {
                cache.Add(charactersFlow[i]);//把前三个加入
            }
            curIndex = cache.Count - 1;
            //cacheIndex = 0;
        }
        
        //根据cacheIndex，从Cache容器中返回对象
        if (cacheIndex > cache.Count - 1)
        {
            return null;
        }
        CharacterSO curCharacter = cache[cacheIndex];
        Debug.Log($"current cache index: {cacheIndex}");
        cacheIndex++;
        Debug.Log(curCharacter.name);
        return curCharacter;
    }

    public CharacterSO NextCharacter(CharacterSO curCharacter)
    {
        if (curIndex < charactersFlow.Count - 1)
        {
            cache.Remove(curCharacter);
            curIndex++;
            cache.Add(charactersFlow[curIndex]);
            Debug.Log($"Flow index: {curIndex}");
            return charactersFlow[curIndex];
        }
        else
        {
            return null;
        }
    }
}
