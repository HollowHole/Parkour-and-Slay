using UnityEngine;
[CreateAssetMenu(fileName = "音波尖叫SO", menuName = "ScriptableObject/牌/音波尖叫SO", order = 1)]
public class CardSonicScreamSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("buff图片")]
    public Sprite BuffSprite;
    [Tooltip("持续时间")]
    public float LastTime = 10;
    [Tooltip("攻击力下降百分比")]
    public float DmgDecrePerc = 25;
    [Tooltip("受伤增加百分比")]
    public float TakeDmgIncrePerc = 25;
    [Tooltip("判定范围（玩家中心）")]
    public float AffectRange = 100;
}
