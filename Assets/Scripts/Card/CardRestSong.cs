using UnityEngine;
public class CardRestSong : CardProto
{
    CardRestSongSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardRestSongSO;
        base.Awake();
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.LastTime, m_cardSO.EnergyRegenerateIncrePerc, m_cardSO.RecoverHpSpeed));
        }
    }
    public class MyBuff : Buff
    {
        float EnergyRegenerateIncrePerc;
        float RecoverHpSpeed;
        float HpRecoverTimer;
        public MyBuff(Sprite ui, float lastTime,float enerReg,float HpRec) : base(ui,lastTime)
        {
            EnergyRegenerateIncrePerc = enerReg;
            RecoverHpSpeed = HpRec;
        }
        protected override void HandleInitEffect(Transform target)
        {
            CardManager.Instance.EnergyRegenerateRate *= (1 + EnergyRegenerateIncrePerc);
            HpRecoverTimer = 1;
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
            HpRecoverTimer -= Time.deltaTime;
            if (HpRecoverTimer <= 0)
            {
                HpRecoverTimer = 1;
                target.GetComponent<ICanTakeDmg>().TakeDamage(-RecoverHpSpeed);
            }
        }
        protected override void HandleFinishEffect(Transform target)
        {
            CardManager.Instance.EnergyRegenerateRate /= (1 + EnergyRegenerateIncrePerc);
        }
    }
}
