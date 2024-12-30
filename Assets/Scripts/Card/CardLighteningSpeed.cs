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
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            Player.Instance.DmgMagniSpeed = () => Mathf.Pow(2, Player.Instance.Speed / 100);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            Player.Instance.DmgMagniSpeed = () => Player.Instance.Speed / 100;
        }
    }
}
