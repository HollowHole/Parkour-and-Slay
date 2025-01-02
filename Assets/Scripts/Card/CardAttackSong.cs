using UnityEngine;
/// <summary>
/// 进攻歌声
/// </summary>
public class CardAttackSong : CardProto
{
    CardAttackSongSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardAttackSongSO;
        base.Awake();
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.LastTime, m_cardSO.DmgIncrePerc));
        }
    }
    public class MyBuff : Buff
    {
        Player player;
        float dmgIncrePerc;
        public MyBuff(Sprite ui, float lastTime, float dmgInc) : base("进攻歌声", ui, lastTime)
        {
            dmgIncrePerc = dmgInc;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base .HandleInitEffect(target);
            player = target.GetComponent<Player>();
            if (player != null)
            {
                player.DmgMagniOther += dmgIncrePerc / 100;
            }
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            if (player != null)
            {
                player.DmgMagniOther -= dmgIncrePerc / 100;
            }
        }
    }
}
