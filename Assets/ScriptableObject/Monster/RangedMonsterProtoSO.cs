using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "基础远程怪物SO", menuName = "ScriptableObject/怪物/基础远程怪物SO", order = 0)]
public class RangedMonsterProtoSO : MonsterProtoSO
{
    [Header("远程相关")]
    [Tooltip("攻击距离")]
    public float AttackRange;
    [Tooltip("攻击间隔")]
    public float AttackInterval = 1;
    [Tooltip("离散(角度)")]
    public float Scatter = 0;
    [Tooltip("子弹飞行速度")]
    public float BulletSpeed = 5;
    [Tooltip("子弹加速度")]
    public float BulletSpeedUpRate = 1;

    [Tooltip("攻击对象TAG")]
    public string TargetTag = "Player";
    

    [Tooltip("子弹伤害")]
    public float BulletDamage = 1 ;


    public RangedMonsterProtoSO()
    {
        Type = MonsterType.Ranged;
    }
}
