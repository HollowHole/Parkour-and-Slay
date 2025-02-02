using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 闪电速度
/// </summary>
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
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite));
        }
    }
    public class MyBuff : Buff
    {
        public MyBuff(Sprite ui) : base("闪电速度", ui)
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
