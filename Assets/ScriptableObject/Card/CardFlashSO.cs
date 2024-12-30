using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "闪现SO", menuName = "ScriptableObject/牌/闪现SO", order = 1)]
public class CardFlashSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
    [Tooltip("闪现距离")]
    public float FlashDistance = 1;
}
