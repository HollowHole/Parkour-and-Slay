using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;  
    //difficulty
    [SerializeField] float difficultyBias = 0.3f;
    float difficultyAccum;
    float difficultyDemand = 0;
    const float difficultyIncreaseRate = 1;

    //monsters
    [Tooltip("所有在这里生成的怪物")]
    [SerializeField] List<MonsterProto> AllSpawnableMonsters;

    int meleeMonsterCnt = 0;
    [SerializeField] int meleeMonsterCntLimit = 3;
    [SerializeField] Transform MonsterSpawnPoint;
    [Tooltip("生成位置的X轴偏移范围")]
    [SerializeField] float spawnXBias = 3f;

    int rangedMonsterCnt = 0;
    [SerializeField] int rangedMonsterCntLimit = 3;

    List<MonsterProto> meleeMonsters2Spawn = new List<MonsterProto>();
    List<MonsterProto> rangedMonsters2Spawn = new List<MonsterProto>();
    private void Awake()
    {
        Instance = this;
        difficultyDemand = 0 + difficultyIncreaseRate;
        GenerateMonsterList();
    }
    private void Start()
    {
        LevelMgr.Instance.OnLevelUp += OnLevelUp;
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
                if (monsterSO.Type == MonsterType.Melee)
                {
                    meleeMonsters2Spawn.Add(monster);
                }
                else if(monsterSO.Type == MonsterType.Ranged) 
                {
                    rangedMonsters2Spawn.Add(monster);
                }
                difficultyAccum += monsterSO.DifficultyValue;
                //重置Bag
                SpawnBag = new List<MonsterProto>(SpawnableMonsters);

            }
        }
    }
    void OnLevelUp()
    {
        difficultyDemand += difficultyIncreaseRate;
        GenerateMonsterList();
    }
    public void OnMonsterDisappear(MonsterProto monster)
    {

        if(monster.GetSO().Type == MonsterType.Melee)
            meleeMonsterCnt--;
        else
            rangedMonsterCnt--;
    }

    private void Update()
    {
        if (meleeMonsters2Spawn.Count + rangedMonsters2Spawn.Count <= 0)
        {
            LevelMgr.Instance.LevelUp();
            return;
        }
        if(meleeMonsterCnt < meleeMonsterCntLimit && meleeMonsters2Spawn.Count>0)
        {
            SpawnMonster(MonsterType.Melee);
        }
        if(rangedMonsterCnt < rangedMonsterCntLimit && rangedMonsters2Spawn.Count>0)
        {
            SpawnMonster(MonsterType.Ranged);
        }
        
    }

    private void SpawnMonster(MonsterType type)
    {
        Vector3 spawnPoint = MonsterSpawnPoint.position;
        spawnPoint.x += UnityEngine.Random.Range(-spawnXBias, spawnXBias);

        if(type == MonsterType.Melee) {
            MonsterProto monster;
            meleeMonsterCnt++;
            monster = Instantiate(meleeMonsters2Spawn[meleeMonsters2Spawn.Count - 1],transform);
            meleeMonsters2Spawn.RemoveAt(meleeMonsters2Spawn.Count - 1);
            monster.transform.position = spawnPoint;
        }
        else if(type == MonsterType.Ranged) {
            RangedMonsterProto monster;
            rangedMonsterCnt++;
            monster = Instantiate(rangedMonsters2Spawn[rangedMonsters2Spawn.Count - 1], transform) as RangedMonsterProto;
            rangedMonsters2Spawn.RemoveAt(rangedMonsters2Spawn.Count - 1);
            monster.transform.position = spawnPoint;
        }
    }

}
