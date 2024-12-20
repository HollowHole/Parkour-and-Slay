using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterProto : MonsterProto
{
    RangedMonsterProtoSO rangedMonsterSO;
    public Vector3 StayPoint;
    [SerializeField] private GameObject BulletPrefab;
    protected float AttackTimer;

    protected List<GameObject> myBullets = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        rangedMonsterSO = monsterSO as RangedMonsterProtoSO;
    }
    protected override void Start()
    {
        base.Start();
        AttackTimer = rangedMonsterSO.AttackInterval;
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
            AttackTimer = rangedMonsterSO.AttackInterval;
            SpawnBullets();
            ApplyBasicAttriAndBuffAffect2Bullet();
        }
    }
    private void ApplyBasicAttriAndBuffAffect2Bullet()
    {
        foreach (GameObject bullet in myBullets)
        {
            BulletProto b = bullet.GetComponent<BulletProto>();
            b.ApplyBasicAttri(rangedMonsterSO.TargetTag, rangedMonsterSO.BulletSpeed, rangedMonsterSO.BulletDamage);
            b.OnHitTarget += ApplyMyBuffOnHit;
        }
    }
    protected virtual void SpawnBullets()
    {
        myBullets.Clear();
        myBullets.Add(Instantiate(BulletPrefab, transform, false));
    }

    protected override void HandleMovement()
    {
        //
    }
    protected virtual void ApplyMyBuffOnHit(Collider collider) { }
}
