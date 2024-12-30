using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "羽神SO", menuName = "ScriptableObject/牌/羽神SO", order = 1)]
public class CardFeatherGodSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("羽毛发射间隔时间")]
    public float ShootInterval = 2f;
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
}
