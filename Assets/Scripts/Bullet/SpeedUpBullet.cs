using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBullet : BulletProto
{
    protected override void OnHitTarget(Collider target)
    {
        target.GetComponent<Player>().Speed += bulletSO.Value;
        Destroy(gameObject);
    }
}
