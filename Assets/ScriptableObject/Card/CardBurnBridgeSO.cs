using UnityEngine;
[CreateAssetMenu(fileName = "破釜沉舟SO", menuName = "ScriptableObject/牌/破釜沉舟SO", order = 1)]
public class CardBurnBridgeSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("buff图片")]
    public Sprite BuffSprite;
    [Tooltip("吸血比例")]
    public float LifeStealPerc = 20;
    [Tooltip("伤害增加百分比")]
    public float DmgIncrePerc = 20;
    [Tooltip("伤害减免百分比")]
    public float TakeDmgDecrePerc = 20;
}
