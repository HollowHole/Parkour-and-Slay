using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;
    [SerializeField] float SpawnCD = 0.33f;
    float SpawnTimer;
    //difficulty
    [SerializeField] float difficultyBias = 0.3f;
    float difficultyAccum;
    float difficultyDemand = 0;
    const float difficultyIncreaseRate = 1;

    //monsters
    [Tooltip("所有在这里生成的怪物")]
    [SerializeField] List<MonsterProto> AllSpawnableMonsters;

    //int meleeMonsterCnt = 0;
    //[SerializeField] int meleeMonsterCntLimit = 3;
    [SerializeField] Transform MonsterSpawnPoint;
    [Tooltip("生成位置的X轴偏移范围")]
    [SerializeField] float spawnXBias = 3f;
    [SerializeField] float spawnPosMaxY = 2f;

    List<MonsterProto> SpawnedMonsterList;
    [SerializeField] int rangedMonsterCntLimit = 2;

    //List<MonsterProto> meleeMonsters2Spawn = new List<MonsterProto>();
    List<MonsterProto> rangedMonsters2Spawn = new List<MonsterProto>();
    //
    public Action OnAllMonsterCleared;
    private void Awake()
    {
        Instance = this;

        OnLevelBegin();
        SpawnedMonsterList = new List<MonsterProto>();
    }
    private void Start()
    {
        LevelMgr.Instance.OnLevelBegin += OnLevelBegin;
    }
    private void GenerateMonsterList()
    {
        difficultyAccum = 0;
        //移除 难度范围 不符合当前关卡 难度需求 的怪物
        List<MonsterProto> SpawnableMonsters = AllSpawnableMonsters.FindAll(monster => {
            MonsterProtoSO monsterSO = monster.GetSO();
            return monsterSO.DifficultyRangeMin <= difficultyDemand && monsterSO.DifficultyRangeMax >= difficultyDemand;
        });

        List<MonsterProto> SpawnBag = new List<MonsterProto>(SpawnableMonsters);

        while (Math.Abs(difficultyDemand - difficultyAccum) > difficultyBias )
        {   
            //等权随机刷怪
            int index = UnityEngine.Random.Range(0, SpawnBag.Count);
            MonsterProto monster = SpawnBag[index];
            SpawnBag.RemoveAt(index);

            MonsterProtoSO monsterSO = monster.GetSO();
            if (difficultyAccum + monsterSO.DifficultyValue > difficultyDemand + difficultyBias)
            {
                //刷怪失败
                
            }
            else
            {
                //成功
                //if (monsterSO.Type == MonsterType.Melee)
                //{
                //    meleeMonsters2Spawn.Add(monster);
                //}
                if(monsterSO.Type == MonsterType.Ranged) 
                {
                    rangedMonsters2Spawn.Add(monster);
                }
                difficultyAccum += monsterSO.DifficultyValue;
                //重置Bag
                SpawnBag = new List<MonsterProto>(SpawnableMonsters);

            }
        }
    }
    void OnLevelBegin()
    {
        difficultyDemand =LevelMgr.Instance.CurLevelCnt * difficultyIncreaseRate;
        rangedMonsterCntLimit = Mathf.FloorToInt(LevelMgr.Instance.CurLevelCnt * 1.34f + 0.66f);

        GenerateMonsterList();
    }
    public void OnMonsterDisappear(MonsterProto monster)
    {

        SpawnedMonsterList.Remove(monster);

        if ( rangedMonsters2Spawn.Count + SpawnedMonsterList.Count <= 0)
        {
            OnAllMonsterCleared?.Invoke();
        }
    }

    private void Update()
    {
        if(SpawnTimer>0)
        {
            SpawnTimer -= Time.deltaTime;
            return;
        }
        
        //Spawn Monster
        //if(meleeMonsterCnt < meleeMonsterCntLimit && meleeMonsters2Spawn.Count>0)
        //{
        //    SpawnMonster(MonsterType.Melee);
        //}
        if(SpawnedMonsterList.Count < rangedMonsterCntLimit && rangedMonsters2Spawn.Count>0)
        {
            SpawnMonster(MonsterType.Ranged);
        }
        
    }

    private bool SpawnMonster(MonsterType type)
    {
        SpawnTimer = SpawnCD;

        Vector3 spawnPoint = MonsterSpawnPoint.position;
        spawnPoint.x += UnityEngine.Random.Range(-spawnXBias, spawnXBias);
        spawnPoint.y = UnityEngine.Random.Range(0, spawnPosMaxY);
        if (Physics.OverlapSphere(spawnPoint, 0.3f, LayerMask.GetMask("Monster")).Length > 0) return false;//

        //if(type == MonsterType.Melee) {
        //    MonsterProto monster;
        //    meleeMonsterCnt++;
        //    monster = Instantiate(meleeMonsters2Spawn[meleeMonsters2Spawn.Count - 1],transform);
        //    meleeMonsters2Spawn.RemoveAt(meleeMonsters2Spawn.Count - 1);
        //    monster.transform.position = spawnPoint;
        //}
        if(type == MonsterType.Ranged) {
            int SpawnIndex = rangedMonsters2Spawn.Count;
            MonsterProto monster;
            while (--SpawnIndex >= 0 && !rangedMonsters2Spawn[SpawnIndex].MeetSpawnReq(SpawnedMonsterList)) ;

            if(SpawnIndex < 0)
                return false;
            monster = Instantiate(rangedMonsters2Spawn[SpawnIndex], transform);
            SpawnedMonsterList.Add(monster);
            rangedMonsters2Spawn.RemoveAt(SpawnIndex);
            monster.transform.position = spawnPoint;
        }
        return true;
    }

}
