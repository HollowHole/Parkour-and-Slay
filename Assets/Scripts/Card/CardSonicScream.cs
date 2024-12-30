using UnityEngine;
public class CardSonicScream : CardProto
{
    CardSonicScreamSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardSonicScreamSO;
        base.Awake();
    }
    public override void OnUse()
    {
        base.OnUse();
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_cardSO.AffectRange, LayerMask.GetMask("Monster"));
        foreach (Collider collider in colliders)
        {
            BuffMgr monster = collider.GetComponentInParent<BuffMgr>();
            if (monster != null)
            {
                monster.AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.LastTime, m_cardSO.DmgDecrePerc, m_cardSO.TakeDmgIncrePerc));
            }
        }
    }
    protected override void SpawnBullets()
    {
        //Do Nothing
    }
    class MyBuff : Buff
    {
        float DmgDecrePerc;
        float TakeDmgIncrePerc;
        public MyBuff(Sprite ui, float lastTime, float _DmgDecrePerc, float _TakeDmgIncrePerc) : base(ui, lastTime)
        {
            DmgDecrePerc = _DmgDecrePerc / 100;
            TakeDmgIncrePerc = _TakeDmgIncrePerc / 100;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            MonsterProto targetMonster = target.GetComponent<MonsterProto>();
            targetMonster.DmgMagni -= DmgDecrePerc;
            targetMonster.TakenDmgMagni += TakeDmgIncrePerc;
        }
    }
}

