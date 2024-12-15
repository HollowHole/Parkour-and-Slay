using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "基础怪物SO", menuName = "ScriptableObject/怪物/基础怪物SO", order = 0)]
public class MonsterProtoSO : ScriptableObject
{
    public float Hp;
    public float Speed;
    public float Damage;
    public float AffectSpeedAbility;
}
