using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedMonsterProto : MonsterProto
{
    
    RangedMonsterProtoSO r_monsterSO;
    [SerializeField] private GameObject BulletPrefab;
    protected float AttackTimer;

    public float BulletDmg { get; set; }

    protected List<GameObject> myBullets = new List<GameObject>();
    protected override void Awake()
    {
        r_monsterSO = base.monsterSO as RangedMonsterProtoSO;
        base.Awake();
        
    }
    protected override void Start()
    {
        base.Start();
        AttackTimer = r_monsterSO.AttackInterval;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleAttack();
    }
    protected override void ReadSO()
    {
        base.ReadSO();
        BulletDmg = r_monsterSO.BulletDamage;
    }
    protected virtual void HandleAttack()
    {
        if(transform.position.z - player.transform.position.z > r_monsterSO.AttackRange)
        {
            return;
        }
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
        else
        {
            AttackTimer = r_monsterSO.AttackInterval;
            SpawnBullets();
            
        }
    }
    protected void ApplyBasicAttriAndBuffAffect2Bullet()
    {
        foreach (GameObject bullet in myBullets)
        {
            BulletProto b = bullet.GetComponent<BulletProto>();

            Vector3 bulletV = transform.forward * r_monsterSO.BulletSpeed;
            Vector3 randomAxis = UnityEngine.Random.onUnitSphere;
            Quaternion rotationQuaternion = Quaternion.AngleAxis(UnityEngine.Random.Range(0,r_monsterSO.Scatter), randomAxis);
            bulletV = rotationQuaternion * bulletV;

            b.ApplyBasicAttri(r_monsterSO.TargetTag, bulletV, BulletDmg * DmgMagni, AffectSpeedAbi * AffeSpeMagni, false, "Monster");
            b.OnHitTarget += ApplyMyBuffOnHit;
        }
    }
    protected virtual void SpawnBullets()
    {
        myBullets.Clear();
        GameObject b = (Instantiate(BulletPrefab, GameObject.Find("AllBullets").transform, false));
        myBullets.Add(b);
        Vector3 Ext=GetComponentInChildren<Collider>().bounds.extents;
        b.transform.position = Center + (Ext.z + 0.6f) * transform.forward;
        //b.transform.rotation = transform.rotation;
        ApplyBasicAttriAndBuffAffect2Bullet();
    }

    protected override void HandleMovement()
    {
        float dist = transform.position.z - r_monsterSO.AttackRange;
        Vector3 v = Vector3.zero;
        if (dist > 0)
        {
            v =  transform.forward * speed + new Vector3(0, 0, -1) * player.Speed;
        }
        rb.velocity = v * Global.SpeedFactor;

        //Vector3 v = signal * transform.forward * (r_monsterSO.Speed + player.Speed);
        //rb .velocity = ( v * Global.SpeedFactor);

        speed += monsterSO.SpeedUpRate * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, monsterSO.MaxSpeedLimit);
    }
    protected virtual void ApplyMyBuffOnHit(Transform target) { }
}
