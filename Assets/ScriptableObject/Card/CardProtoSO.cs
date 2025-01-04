
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Consume };
public enum Rarity { Initial,Common,Rare,Legendary};

[CreateAssetMenu(fileName = "基础牌SO", menuName = "ScriptableObject/牌/基础牌SO", order = 0)]
public class CardProtoSO : ScriptableObject
{
    public List<CardType> CardTypes;
    public Rarity rarity = Rarity.Common;

    [Tooltip("卡牌名称")]
    public string CardName = "卡牌名称";
    [Tooltip("卡牌描述")]
    public string CardDesc = "卡牌描述";
    [Tooltip("卡牌牌面")]
    public Sprite CardFace;

    [Tooltip("费用")]
    public int EnergyCost = 1;
    [Tooltip("起甲")]
    public float GainArmor = 0;

    //[Tooltip("生成对象")]
    //public GameObject bullet;
    [Tooltip("提高速度")]
    public float SpeedUpValue = 0;

    [Tooltip("使用后抽牌")]
    public int DrawCardCnt = 0;

    [Header("子弹属性")]


    [Tooltip("穿透")]
    public bool isPierce = false;

    [Tooltip("初始飞行速度")]
    public float BulletSpeed = 5;

    [Tooltip("攻击对象TAG")]
    public string TargetTag = "Player";


    [Tooltip("伤害")]
    public float Damage = 0;


    [HideInInspector][Tooltip("冷却时间(废弃)")]
    public float CD = 3f;
}
