using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class GhostMageBullet : BulletProto
{
    bool hitPlayer = false;
    protected override void Awake()
    {
        base.Awake();
        
        OnHitTarget += (target) => { target.GetComponent<Player>().Moveable--;hitPlayer = true; Debug.Log("Hit"); };
        

        Vector3 targetScale = transform.localScale;
        targetScale.z = 0.01f;
        transform.localScale = targetScale;
    }
    protected override void Start()
    {
        base.Start();
        isPierce = true;
        rb.velocity = new Vector3(0, 0, Global.SpeedFactor * initSpeed.z / 2);
    }
    protected override void HandleDisappear()
    {
        //won't disappear
    }
    private void FixedUpdate()
    {
        Vector3 targetScale = transform.localScale;
        targetScale.z += Mathf.Abs(initSpeed.z) * Time.deltaTime * Global.SpeedFactor;
        transform.localScale = targetScale;
    }
    private void OnDestroy()
    {
        if (hitPlayer)
        {
            Player.Instance.Moveable++;
        }
    }

}
