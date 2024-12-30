using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField]Transform HpBar;
    [SerializeField] Transform ArmorBar;
    private void Start()
    {
        FindObjectOfType<Player>().OnHpChange += OnHpChange;
    }
    void OnHpChange(float hp, float MaxHp,float armor)
    {
        HpBar.localScale = new Vector3(hp / MaxHp, 1, 1);
        ArmorBar.localScale = new Vector3(armor / MaxHp, 1, 1);
    }
}
