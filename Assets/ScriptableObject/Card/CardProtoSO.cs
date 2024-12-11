using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardProtoSO : ScriptableObject
{
    [Tooltip("费用")]
    public int EnergyCost = 1;
    [Tooltip("冷却时间")]
    public float CD = 3f;
}
