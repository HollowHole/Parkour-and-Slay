using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoppingLeftAndRight : CardProto
{
    CardHoppingLeftAndRightSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = (CardHoppingLeftAndRightSO)cardSO;
        base.Awake();
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.DodgeSuccBonus, m_cardSO.DodgeFailPunish));
        }
    }
    public class MyBuff : Buff
    {
        float bonusValue;
        float punishValue;
        public MyBuff(Sprite sprite,float bonus,float punish):base("左右横跳", sprite) 
        {
            bonusValue = bonus;
            punishValue = punish;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            DodgeJudger.Instance.OnDodgeSuccess += DodgeBonus;
            DodgeJudger.Instance.OnDodgeFailure += DodgeFPunish;
        }
        void DodgeBonus()
        {
            Player.Instance.GetComponent<ICanAffectSpeed>().AffectSpeed(bonusValue);
        }
        void DodgeFPunish()
        {
            Player.Instance.GetComponent<ICanAffectSpeed>().AffectSpeed(punishValue);
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            DodgeJudger.Instance.OnDodgeSuccess -= DodgeBonus;
            DodgeJudger.Instance.OnDodgeFailure -= DodgeFPunish;

        }
    }
}
