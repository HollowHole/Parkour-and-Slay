using UnityEngine;
[CreateAssetMenu(fileName = "防御歌声SO", menuName = "ScriptableObject/牌/御歌声SO", order = 1)]
public class CardDefenceSongSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("buff图片")]
    public Sprite BuffSprite;
    [Tooltip("持续时间")]
    public float LastTime = 18;
    [Tooltip("防御力增加百分比")]
    public float DefenceIncrePerc = 25;
}
