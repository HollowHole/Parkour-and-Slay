using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageBullet : BulletProto
{
    private void FixedUpdate()
    {
        Vector3 v = rb.velocity;
        v.y -= Global.GRAVITY * Time.deltaTime;
        rb.velocity = v;
    }
}

