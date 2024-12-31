using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "火法师怪物SO", menuName = "ScriptableObject/怪物/火法师怪物SO", order = 0)]
public class MonsterFireMageSO : RangedMonsterProtoSO
{
    [Header("火球抛物线属性")]
    [Tooltip("最大水平速度")]
    public float HorizontalSpeedMax;
    [Tooltip("最小水平速度")]
    public float HorizontalSpeedMin;
    [Tooltip("z轴落点偏移")]
    public float DropPosBiasZ;
}
