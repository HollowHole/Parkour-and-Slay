using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "冰盾战士怪物SO", menuName = "ScriptableObject/怪物/冰盾战士怪物SO", order = 0)]
public class MonsterIceShieldWarriorSO : RangedMonsterProtoSO
{
    [Header("特殊效果")]
    [Tooltip("盾牌血量")]
    public float ShieldHp = 50;
    [Tooltip("碎盾后眩晕时长")]
    public float DizzyTimeOnShieldBroken = 5f;
    [Tooltip("经过子弹获得伤害加成")]
    public float DmgBonus2PassedBullet = 20;
    [Tooltip("经过子弹获得降速效果加成")]
    public float AffeSpeBonus2PassedBullet = 20;

}
