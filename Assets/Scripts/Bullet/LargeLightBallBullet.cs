using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class LargeLightBallBullet : BulletProto
{
    public float ExplodeRange = 3f;
    protected override void Awake()
    {
        base.Awake();
        OnHitTarget = MyHitEffect;
    }

    private void MyHitEffect(Transform transform)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplodeRange, LayerMask.GetMask("Monster"));
        foreach (Collider collider in colliders)
        {
            ICanTakeDmg monster = collider.GetComponentInParent<ICanTakeDmg>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
        }
    }
}
