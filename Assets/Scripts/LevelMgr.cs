using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public static LevelMgr Instance;

    int levelCnt;
    public int CurLevelCnt => levelCnt;
    public Action OnLevelEnd;
    public Action OnLevelBegin;
    private void Awake()
    {
        Instance = this;
        levelCnt = 1;
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
        foreach(Transform b in GameObject.Find("AllBullets").transform)
        {
            Destroy(b.gameObject);
        }
    }
    
}
