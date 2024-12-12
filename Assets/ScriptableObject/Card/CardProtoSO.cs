using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "基础牌SO", menuName = "ScriptableObject/牌/基础牌SO", order = 0)]
public class CardProtoSO : ScriptableObject
{
    [Tooltip("费用")]
    public int EnergyCost = 1;
    [Tooltip("冷却时间")]
    public float CD = 3f;
    [Tooltip("生成对象")]
    public GameObject bullet;
}
