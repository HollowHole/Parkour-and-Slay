using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "闪电速度SO", menuName = "ScriptableObject/牌/闪电速度SO", order = 1)]
public class CardLighteningSpeedSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
}
