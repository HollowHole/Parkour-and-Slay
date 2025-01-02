using UnityEngine;
[CreateAssetMenu(fileName = "进攻歌声SO", menuName = "ScriptableObject/牌/进攻歌声SO", order = 1)]
public class CardAttackSongSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("buff图片")]
    public Sprite BuffSprite;
    [Tooltip("持续时间")]
    public float LastTime = 18;
    [Tooltip("伤害增加百分比")]
    public float DmgIncrePerc = 25;
}
