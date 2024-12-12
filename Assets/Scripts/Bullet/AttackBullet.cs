using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : BulletProto
{
    protected override void OnHitTarget(Collider target)
    {
        target.GetComponent<ICanTakeDmg>().TakeDamage(bulletSO.Value);
        Destroy(gameObject);
    }
}
