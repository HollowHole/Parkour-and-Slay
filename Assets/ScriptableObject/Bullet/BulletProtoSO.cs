using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "基础子弹SO", menuName = "ScriptableObject/子弹/基础子弹SO", order = 0)]
public class BulletProtoSO : ScriptableObject
{
    [Tooltip("初始飞行速度")]
    public float Speed = 5;

    [Tooltip("数值")]
    public float Value;

    [Tooltip("攻击对象TAG")]
    public string TargetTag;
    
    //初始飞行方向
}
