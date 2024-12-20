using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoppingLeftAndRight : CardProto
{
    CardHoppingLeftAndRightSO m_cardSO;
    protected override void Awake()
    {
        base.Awake();
        m_cardSO = (CardHoppingLeftAndRightSO)cardSO;

    }
    public override void OnUse()
    {
        base.OnUse();
    }
    protected override void ApplyMyBuffOnHit(Collider collider)
    {

        collider.GetComponent<BuffMgr>().AddBuff(new MyBuff(m_cardSO.DodgeSuccBonus,m_cardSO.DodgeFailPunish));
    }
    public class MyBuff : Buff
    {
        float bonusValue;
        float punishValue;
        public MyBuff(float bonus,float punish)
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
        public override void CountDown()
        {
            //DoNothing;
        }
    }
}
