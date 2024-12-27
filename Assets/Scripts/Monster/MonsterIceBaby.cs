using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIceBaby : RangedMonsterProto
{
    RangedMonsterProto targetMonster;
    IceBabyMonsterSO m_monsterSO;
    protected override void  Awake()
    {
        m_monsterSO = monsterSO as IceBabyMonsterSO;
        base.Awake();
        
        targetMonster = null;
    }
    protected override void HandleAttack()
    {
        if (targetMonster == null || Vector3.Distance(targetMonster.transform.position,transform.position) > m_monsterSO.AttackRange)
        {
            return;
        }
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
        else
        {
            AttackTimer = m_monsterSO.AttackInterval;
            SpawnBullets();
            ApplyBasicAttriAndBuffAffect2Bullet();
        }
    }
    protected override void HandleRotation()
    {
        if (targetMonster != null)
        {
            transform.LookAt(targetMonster.transform);
        }
        else
        {
            transform.LookAt(player.transform);
        }
    }
    private void Update()
    {
        if (targetMonster == null)
        {
            //Find Target
            Collider[] colliders = Physics.OverlapSphere(Center, m_monsterSO.AttackRange,LayerMask.GetMask("Monster"));
            if (colliders.Length == 0) { return; }
            targetMonster = colliders[Random.Range(0, colliders.Length)].GetComponentInParent<RangedMonsterProto>();
        }
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        target.GetComponent<BuffMgr>().AddBuff(new MyBuff(m_monsterSO.LastTime,m_monsterSO.DmgScalarBonus,m_monsterSO.AffectSpeedAbiScalarBonus));
    }
    public class MyBuff : Buff
    {
        float DmgScalarBonus;
        public float AffectSpeedAbiScalarBonus;
        public MyBuff(float lastTime,float _DmgScalarBonus,float AfSpScalarBonus) : base(null, lastTime)
        {
            DmgScalarBonus = _DmgScalarBonus;
            AffectSpeedAbiScalarBonus = AfSpScalarBonus;
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            target.GetComponent<RangedMonsterProto>().BulletDmg *= DmgScalarBonus;
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            target.GetComponent<RangedMonsterProto>().BulletDmg /= DmgScalarBonus;
        }
    }
}
