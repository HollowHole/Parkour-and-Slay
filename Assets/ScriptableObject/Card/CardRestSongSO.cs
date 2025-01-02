using UnityEngine;
[CreateAssetMenu(fileName = "休息歌声SO", menuName = "ScriptableObject/牌/休息歌声SO", order = 1)]
public class CardRestSongSO : CardProtoSO
{
    [Header("特殊效果")]
    [Tooltip("buff图片")]
    public Sprite BuffSprite;
    [Tooltip("持续时间")]
    public float LastTime = 10;
    [Tooltip("能量回复速度增加百分比")]
    public float EnergyRegenerateIncrePerc = 50;
    [Tooltip("每秒回复血量")]
    public float RecoverHpSpeed = 1;
}
