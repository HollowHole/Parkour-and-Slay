using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBabyBullet : BulletProto
{
    private void FixedUpdate()
    {
        Vector3 v= rb.velocity;
        v.y = 0;
        v.x = 0;
        rb.velocity = v;
    }
}

