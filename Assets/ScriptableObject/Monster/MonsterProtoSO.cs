using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MonsterType { Melee,Ranged };

[CreateAssetMenu(fileName = "基础怪物SO", menuName = "ScriptableObject/怪物/基础怪物SO", order = 0)]
public class MonsterProtoSO : ScriptableObject
{
    public MonsterType Type = MonsterType.Melee;
    public float Hp;
    public float Speed;
    public float SpeedUpRate = 0;
    public float MaxSpeedLimit;
    [Tooltip("碰撞伤害")]
    public float CollideDamage;
    public float AffectSpeedAbility;
    [Tooltip("难度范围（最小）")]
    public float DifficultyRangeMin = 1;
    [Tooltip("难度范围（最大）")]
    public float DifficultyRangeMax = 5;
    [Tooltip("难度数值")]
    public float DifficultyValue = 0.2f;
}
