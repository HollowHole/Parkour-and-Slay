using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 热身运动
/// </summary>
public class CardWarmUp : CardProto
{
    CardWarmUpSO cardWarmUpSO;
    protected override void Awake()
    {
        base.Awake();
        cardWarmUpSO = (CardWarmUpSO)cardSO;
        
    }

    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(cardWarmUpSO.BuffSprite, cardWarmUpSO.ExtraSpeedValue));
        }
    }
    public class MyBuff : Buff
    {
        float extraSpeedValue;
        public MyBuff(Sprite ui,float _extraSpeedValue):base("热身运动", ui) 
        {
            extraSpeedValue = _extraSpeedValue;
        }
        protected override void HandleInitEffect(Transform target)
        {
            CardManager.Instance.OnUseSpeedUpCard += NextSpeedUpCardGainExtraValue;
        }
        void NextSpeedUpCardGainExtraValue(CardProto card)
        {
            card.SpeedUpValue += extraSpeedValue;
            Finish();
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            CardManager.Instance.OnUseSpeedUpCard -= NextSpeedUpCardGainExtraValue;
        }
    }
}
