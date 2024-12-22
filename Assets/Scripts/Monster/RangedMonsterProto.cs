using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedMonsterProto : MonsterProto
{
    RangedMonsterProtoSO m_monsterSO;
    public Vector3 StayPoint;
    [SerializeField] private GameObject BulletPrefab;
    protected float AttackTimer;

    protected List<GameObject> myBullets = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        m_monsterSO = base.monsterSO as RangedMonsterProtoSO;
    }
    protected override void Start()
    {
        base.Start();
        AttackTimer = m_monsterSO.AttackInterval;
    }
    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }

    protected virtual void HandleAttack()
    {
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
    private void ApplyBasicAttriAndBuffAffect2Bullet()
    {
        foreach (GameObject bullet in myBullets)
        {
            BulletProto b = bullet.GetComponent<BulletProto>();
            Vector3 bulletV = transform.forward * m_monsterSO.BulletSpeed;
            b.ApplyBasicAttri(m_monsterSO.TargetTag, bulletV, m_monsterSO.BulletDamage);
            b.OnHitTarget += ApplyMyBuffOnHit;
        }
    }
    protected virtual void SpawnBullets()
    {
        myBullets.Clear();
        GameObject b = (Instantiate(BulletPrefab, GameObject.Find("AllBullets").transform, false));
        myBullets.Add(b);
        b.transform.position = transform.position;
    }

    protected override void HandleMovement()
    {
        float dist = transform.position.z - player.transform.position.z;
        Vector3 v = Vector3.zero;
        if (dist > m_monsterSO.AttackRange)
        {
            v = transform.forward * (m_monsterSO.Speed + player.Speed);
        }
        rb .velocity = v * Global.SpeedFactor;
    }
    protected virtual void ApplyMyBuffOnHit(Collider collider) { }
}
