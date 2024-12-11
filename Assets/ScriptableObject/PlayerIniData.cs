using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "��ҳ�ʼ����SO", menuName = "ScriptableObject/��ҳ�ʼ����SO", order = 0)]
public class PlayerIniData : ScriptableObject
{
    [Tooltip("�����ƶ��ٶ�")]
    public float LRMoveSpeed = 2;
    [Tooltip("��ʼ�ٶ�")]
    public float IniSpeed = 10;
    [Tooltip("��ʼ���ٶ�")]
    public float IniSpeedUpRate = 0.5f;
    [Tooltip("��ʼ��Ծ�߶�")]
    public float IniJumpHeight = 2;
    [Tooltip("��ʼ���Ѫ��")]
    public float IniMaxHp = 100;
    [Tooltip("����ٶ�����")]
    public float SpeedLimit = 20;
    
}
