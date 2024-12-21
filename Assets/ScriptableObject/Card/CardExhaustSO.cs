using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "透支SO", menuName = "ScriptableObject/牌/透支SO", order = 1)]
public class CardExhaustSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("延迟时间")]
    public float Time;
    [Tooltip("速度影响值")]
    public float PunishValue;
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
}
