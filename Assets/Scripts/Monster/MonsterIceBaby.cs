using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterIceBaby : RangedMonsterProto
{
    MonsterProto targetMonster;
    IceBabyMonsterSO m_monsterSO;
    protected override void  Awake()
    {
        m_monsterSO = monsterSO as IceBabyMonsterSO;
        base.Awake();
        
        targetMonster = null;
    }
    protected override void HandleAttack()
    {
        
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
            return;
        }
        //可以改成发射完寻找下一个目标，缓满旋转过去
        Collider[] colliders = Physics.OverlapSphere(Center, m_monsterSO.AttackRange, LayerMask.GetMask("Monster"));

        MonsterProto[] AllMonsters = colliders.Select(collider => collider.GetComponentInParent<MonsterProto>())
                                                .Where(monster => monster != null && monster != this) // 确保不为 null
                                                .ToArray();

        if (AllMonsters.Length <= 0) { return; }

        targetMonster = AllMonsters.OrderBy(monster => monster.Hp)
                                   .First();

        HandleRotation();

        AttackTimer = m_monsterSO.AttackInterval;
        SpawnBullets();
        
    }
    protected override void HandleRotation()
    {
        if (transform.position.z - m_monsterSO.AttackRange > 0)
        {
            transform.LookAt(player.transform);
        }
        else if (targetMonster != null)
        {
            transform.LookAt(transform.position-Center+targetMonster.Center);
        }
    }

    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_monsterSO.LastTime, m_monsterSO.DmgIncrePerc, m_monsterSO.AffeSpeIncrePerc));
        }
    }
    public class MyBuff : Buff
    {
        float DmgIncrePerc;
        public float AffeSpeIncrePerc;
        public MyBuff(float lastTime,float _DmgIncrePerc, float _AffeSpeIncrePerc) : base(null, lastTime)
        {
            DmgIncrePerc = _DmgIncrePerc / 100;
            AffeSpeIncrePerc = _AffeSpeIncrePerc / 100;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            MonsterProto monster = target.GetComponent<MonsterProto>();

            monster.DmgMagni += DmgIncrePerc;
            monster.AffeSpeMagni += AffeSpeIncrePerc;
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            MonsterProto monster = target.GetComponent<MonsterProto>();

            monster.DmgMagni -= DmgIncrePerc;
            monster.AffeSpeMagni -= AffeSpeIncrePerc;
        }
    }
}
