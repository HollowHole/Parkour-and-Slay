using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWindBaby : RangedMonsterProto
{
    protected override void HandleMovement()
    {
        base.HandleMovement();
        Vector3 v = rb.velocity;
        v.x = -transform.position.x;
        rb.velocity = v;
    }
}
