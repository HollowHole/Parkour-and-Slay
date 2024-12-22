using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public static LevelMgr Instance;

    int levelCnt;
    public Action OnLevelEnd;
    public Action OnLevelBegin;
    private void Awake()
    {
        Instance = this;
        levelCnt = 0;
    }
    private void Start()
    {
        MonsterSpawner.Instance.OnAllMonsterCleared += LevelEnd;
        SwerveManager.instance.OnSwerveZonePassed += LevelBegin;
    }
    public void LevelBegin()
    {
        levelCnt++;
        OnLevelBegin?.Invoke();
    }
    public void LevelEnd()
    {
        Debug.Log("Level End!");
        OnLevelEnd?.Invoke();
    }
}
