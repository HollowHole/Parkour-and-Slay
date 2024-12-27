using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "冰宝宝怪物SO", menuName = "ScriptableObject/怪物/冰宝宝怪物SO", order = 0)]
public class IceBabyMonsterSO : RangedMonsterProtoSO
{
    [Header("Buff效果")]
    public float LastTime = 2f;
    [Tooltip("伤害倍率加成")]
    public float DmgScalarBonus = 1.3f;
    [Tooltip("降速能力倍率加成")]
    public float AffectSpeedAbiScalarBonus = 1.3f;
}
