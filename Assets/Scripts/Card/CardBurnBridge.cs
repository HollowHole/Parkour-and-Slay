using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 破釜沉舟
/// </summary>
public class CardBurnBridge : CardProto
{
    CardBurnBridgeSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardBurnBridgeSO;
        base.Awake();
    }
    public override bool IsUsable(List<CardProto> CurHandCards)
    {
        bool ret = base.IsUsable(CurHandCards);
        ret = ret && CurHandCards.Count == 1 && CurHandCards[0] == this;
        return ret;
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.LifeStealPerc,m_cardSO.DmgIncrePerc, m_cardSO.TakeDmgDecrePerc));
        }
    }
    public class MyBuff : Buff
    {
        Player player;
        float lifeStealPerc;
        float dmgIncrePerc;
        float takeDmgDecrePerc;
        public MyBuff(Sprite ui, float lifeSt,float dmgInc, float ta) : base("破釜沉舟", ui)
        {
            lifeStealPerc = lifeSt;
            dmgIncrePerc = dmgInc;
            takeDmgDecrePerc = ta;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            player = target.GetComponent<Player>();
            if (player != null)
            {
                player.DmgMagniOther += dmgIncrePerc / 100;
                player.TakenDmgMagni -= takeDmgDecrePerc / 100;
            }
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            if (player != null)
            {
                player.DmgMagniOther -= dmgIncrePerc / 100;
                player.TakenDmgMagni += takeDmgDecrePerc / 100;
            }
        }
    }
}
