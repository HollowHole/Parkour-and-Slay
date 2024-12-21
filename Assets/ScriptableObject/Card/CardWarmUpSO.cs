using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "热身运动SO", menuName = "ScriptableObject/牌/热身运动SO", order = 1)]
public class CardWarmUpSO : CardProtoSO
{
    [Header("特殊效果")]
    public float ExtraSpeedValue = 10f;
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
}
