using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField]Transform HpBar;
    private void Start()
    {
        FindObjectOfType<Player>().OnHpChange += OnHpChange;
    }
    void OnHpChange(float hp, float MaxHp)
    {
        HpBar.localScale = new Vector3(hp / MaxHp, 1, 1);
    }
}
