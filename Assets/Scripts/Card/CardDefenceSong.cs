using UnityEngine;
/// <summary>
/// 防御歌声
/// </summary>
public class CardDefenceSong : CardProto
{
    CardDefenceSongSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardDefenceSongSO;
        base.Awake();
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.LastTime, m_cardSO.DefenceIncrePerc));
        }
    }
    public class MyBuff : Buff
    {
        Player player;
        float defIncrePerc;
        public MyBuff(Sprite ui, float lastTime, float defInc) : base("防御歌声", ui, lastTime)
        {
            defIncrePerc = defInc;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            player = target.GetComponent<Player>();
            if (player != null)
            {
                player.TakenDmgMagni -= defIncrePerc / 100;
            }
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            if (player != null)
            {
                player.TakenDmgMagni += defIncrePerc / 100;

            }
        }
    }
}
