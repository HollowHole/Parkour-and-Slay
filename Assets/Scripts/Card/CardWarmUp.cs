using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWarmUp : CardProto
{
    CardWarmUpSO cardWarmUpSO;
    protected override void Awake()
    {
        base.Awake();
        cardWarmUpSO = (CardWarmUpSO)cardSO;

        
    }
    public override void OnUse()
    {
        base.OnUse();
        foreach (GameObject bullet in myBullets)
        {
            
            BulletProto b = bullet.GetComponent<BulletProto>();
            b.OnHitTarget += ApplyMyBuff;
        }
    }

    private void ApplyMyBuff(Collider collider)
    {
        
        collider.GetComponent<BuffMgr>().AddBuff(new MyBuff(cardWarmUpSO.ExtraSpeedValue));
    }
    public class MyBuff : Buff
    {
        float extraSpeedValue;
        public MyBuff(float _extraSpeedValue)
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
        public override void CountDown()
        {
            //DoNothing;
        }
    }
}
