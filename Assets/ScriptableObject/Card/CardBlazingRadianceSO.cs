using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "光焰万丈SO", menuName = "ScriptableObject/牌/光焰万丈SO", order = 1)]
public class CardBlazingRadianceSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("伤害递增值")]
    public float DMGIncrement = 6;
}
