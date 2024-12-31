using UnityEngine;
public class MonsterGhostMage : RangedMonsterProto
{
    protected override void HandleMovement()//添加重力
    {
        base.HandleMovement();
        Vector3 v = rb.velocity;
        v.y -= Global.GRAVITY;
        rb.velocity = v;

    }
    protected override void HandleRotation()
    {
        Vector3 viewPt = transform.position;
        viewPt.z = 0;
        transform.LookAt(viewPt);
    }
    protected override void SpawnBullets()
    {
        if (GetComponentInChildren<BulletProto>() != null)
            return;

        base.SpawnBullets();
        
        //子弹放到脚底，置为自己的子物体
        Bounds bound = GetComponentInChildren<Collider>().bounds;//脚底
        float newY = bound.center.y - bound.extents.y;
        foreach (GameObject b in myBullets)
        {
            Vector3 pos = b.transform.position;
            pos.y = newY;
            b.transform.position = pos;
            b.transform.SetParent(transform,true);
        }
    }
}
