using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIceShieldWarrior : RangedMonsterProto
{
    MonsterIceShieldWarriorSO m_monsterSO;
    IceShield myShield;

    float DizzyTimer;
    bool isInDizzy => DizzyTimer > 0;

    protected override void Awake()
    {
        m_monsterSO = base.monsterSO as MonsterIceShieldWarriorSO;
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        myShield = GetComponentInChildren<IceShield>();
        myShield.OnBroken += OnShieldBroken;
        myShield.ApplyShieldAttri(m_monsterSO.ShieldHp, m_monsterSO.DmgBonus2PassedBullet, m_monsterSO.AffeSpeBonus2PassedBullet);
        myShield.Recover();
    }
    protected override void HandleMovement()
    {
        base.HandleMovement();
        Vector3 v = rb.velocity;

        //v.x = -transform.position.x;

        if (isInDizzy)
            v = Vector3.zero;

        v.y -= Global.GRAVITY;

        rb.velocity = v;

    }
    protected override void HandleAttack()
    {
        //Cant attack
    }
    private void Update()
    {
        if (DizzyTimer > 0)
            DizzyTimer -= Time.deltaTime;
        else if(myShield.Hp == 0)
        {
            //Pick Up Shield Again
            myShield.Recover();
        }
    }
    void OnShieldBroken()
    {
        DizzyTimer = m_monsterSO.DizzyTimeOnShieldBroken;
    }

    public override bool MeetSpawnReq(List<MonsterProto> monsters)
    {
        foreach (MonsterProto monster in monsters)
        {
            if (monster.GetSO() == m_monsterSO)
            {
                return false;
            }
        }
        return true;
    }
}
