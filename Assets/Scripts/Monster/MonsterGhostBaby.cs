using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGhostBaby : RangedMonsterProto
{
    MonsterGhostBabySO m_monsterSO;
    protected override void Awake()
    {
        base.Awake();
        m_monsterSO = base.monsterSO as MonsterGhostBabySO;
    }
    protected override void HandleMovement()//添加重力
    {
        base.HandleMovement();
        Vector3 v = rb.velocity;
        v.y -= Global.GRAVITY;
        rb.velocity = v;
        
    }
    protected override void SpawnBullets()
    {
        base.SpawnBullets();

        //子弹放到脚底
        Bounds bound = GetComponentInChildren<Collider>().bounds;//脚底
        float newY = bound.center.y - bound.extents.y;
        foreach (GameObject b in myBullets)
        {
            Vector3 pos = b.transform.position;
            pos.y = newY;
            b.transform.position = pos;
        }
    }
    protected override void ApplyMyBuffOnHit(Transform target)
    {
        base.ApplyMyBuffOnHit(target);
        
        BuffMgr buffMgr = target.GetComponent<BuffMgr>();
        if (buffMgr != null)
        {
            buffMgr.AddBuff(new MyBuff(m_monsterSO.BuffSprite, m_monsterSO.ControlTime));
        }
    }
    public class MyBuff : Buff
    {
        Player player;
        public MyBuff(Sprite sprite, float lastTime) : base(sprite,lastTime)
        {
        }
        protected override void HandleInitEffect(Transform target)
        {
            base.HandleInitEffect(target);
            player = target.GetComponent<Player>();
            if(player != null)
            {
                player.Moveable--;
                Debug.Log("cant move!");
            }
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
        }
        protected override void HandleFinishEffect(Transform target)
        {
            base.HandleFinishEffect(target);
            if(player != null)
            {
                player.Moveable++;
                Debug.Log("Moveable Again");
            }
        }
    }
}
