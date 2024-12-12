using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpUI : MonoBehaviour
{
    [SerializeField] Transform HpBar;
    MonsterProto monster;
    
    private void Start()
    {
        monster.OnHpChange += OnHpChange;
    }
    void OnHpChange(float hp, float MaxHp)
    {
        HpBar.localScale = new Vector3(hp / MaxHp, 1, 1);
    }
}
