using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardProtoSO : ScriptableObject
{
    [Tooltip("����")]
    public int EnergyCost = 1;
    [Tooltip("��ȴʱ��")]
    public float CD = 3f;
}
