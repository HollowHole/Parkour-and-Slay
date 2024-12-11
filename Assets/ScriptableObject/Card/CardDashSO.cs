using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "牌_冲刺SO", menuName = "ScriptableObject/牌_冲刺SO", order = 0)]
public class CardDashSO : CardProtoSO
{
    [Tooltip("加速数值")]
    public float Value;
}
