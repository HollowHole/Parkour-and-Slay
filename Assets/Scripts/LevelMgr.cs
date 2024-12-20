using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public static LevelMgr Instance;

    int levelCnt;
    public Action OnLevelUp;
    private void Awake()
    {
        Instance = this;
        levelCnt = 0;
    }
    public void LevelUp()
    {
        OnLevelUp?.Invoke();
    }
}
