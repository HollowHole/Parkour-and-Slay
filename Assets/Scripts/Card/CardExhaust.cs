using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExhaust : CardProto 
{
    CardExhaustSO m_cardSO;
    protected override void Awake()
    {
        base.Awake();
        m_cardSO = (CardExhaustSO)cardSO;
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.Time, m_cardSO.PunishValue));

        }
    }
    public class MyBuff : Buff
    {
        float punishValue;
        public MyBuff(Sprite ui,float delayTime,float _punishValue):base(ui,delayTime)
        {
            punishValue = _punishValue;
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            target.GetComponent<ICanAffectSpeed>().AffectSpeed(punishValue);
        }
    }
}
