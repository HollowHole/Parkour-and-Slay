using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "左右横跳SO", menuName = "ScriptableObject/牌/左右横跳SO", order = 1)]
public class CardHoppingLeftAndRightSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("闪避成功奖励")]
    public float DodgeSuccBonus;
    [Tooltip("闪避失败惩罚")]
    public float DodgeFailPunish;
    [Tooltip("BuffUISprite")]
    public Sprite BuffSprite;
}
