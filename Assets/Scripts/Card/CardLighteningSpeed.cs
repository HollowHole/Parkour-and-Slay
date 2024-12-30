using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLighteningSpeed : CardProto
{
    CardLighteningSpeedSO m_cardSO;
    protected override void Awake()
    {
        base.Awake();
        m_cardSO = cardSO as CardLighteningSpeedSO;
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        target.GetComponent<BuffMgr>().AddBuff(new MyBuff(m_cardSO.BuffSprite));
    }
    public class MyBuff : Buff
    {
        public MyBuff(Sprite ui) : base(ui)
        {
        }
        float MyDamageBonus(float damage)
        {
            float oriPlayerSpeedBonus = Player.Instance.Speed / 100;
            return damage/ oriPlayerSpeedBonus * Mathf.Pow(2,oriPlayerSpeedBonus);
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            target.GetComponent<Player>().CalcFinalDmg += MyDamageBonus;
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            target.GetComponent<Player>().CalcFinalDmg -= MyDamageBonus;
        }
    }
}
