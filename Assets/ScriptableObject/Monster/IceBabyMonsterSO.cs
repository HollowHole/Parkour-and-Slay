using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "冰宝宝怪物SO", menuName = "ScriptableObject/怪物/冰宝宝怪物SO", order = 0)]
public class IceBabyMonsterSO : RangedMonsterProtoSO
{
    [Header("Buff效果")]
    public float LastTime = 2f;
    [Tooltip("攻击力加成百分比")]
    public float DmgIncrePerc = 30;
    [Tooltip("降速能力加成百分比")]
    public float AffeSpeIncrePerc = 30;
}
