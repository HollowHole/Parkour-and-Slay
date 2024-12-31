using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "鬼影宝宝怪物SO", menuName = "ScriptableObject/怪物/鬼影宝宝怪物SO", order = 0)]
public class MonsterGhostBabySO : RangedMonsterProtoSO
{
    [Header("特殊效果")]
    public float ControlTime = 0.5f;
    public Sprite BuffSprite;
}
