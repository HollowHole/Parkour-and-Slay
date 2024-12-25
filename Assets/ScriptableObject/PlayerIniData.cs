using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "玩家初始数据SO", menuName = "ScriptableObject/玩家初始数据SO", order = 0)]
public class PlayerIniData : ScriptableObject
{
    [Tooltip("左右移动速度")]
    public float LRMoveSpeed = 2;
    [Tooltip("初始速度")]
    public float IniSpeed = 10;
    [Tooltip("初始加速度")]
    public float IniSpeedUpRate = 0.5f;
    [Tooltip("初始跳跃高度")]
    public float IniJumpHeight = 2;
    
    [Tooltip("最大速度限制")]
    public float SpeedLimit = 20;
    [Tooltip("初始最大血量")]
    public float IniMaxHp = 100;

}
