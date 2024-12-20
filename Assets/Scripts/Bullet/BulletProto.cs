using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletProto : MonoBehaviour
{
    Rigidbody rb;

    string sourceTag;
    string targetTag;
    float initSpeed;
    bool isPierce;
    float damage;

    public Action<Collider> OnHitTarget;

    public bool isHostile => targetTag == "Player";
    public string GetSource => sourceTag;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        

        OnHitTarget += BasicHitEffect;
    }
    private void Start()
    {
        rb.velocity = new Vector3(0, 0, initSpeed);
    }
    protected virtual void Update()
    {
        Debug.Log(rb.velocity);
    }
    public void ApplyBasicAttri(string tt,float s,float D,bool isP = false,string st = "Player")
    {
        targetTag = tt;
        initSpeed = s;
        isPierce = isP;
        damage = D;
        sourceTag = st;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            OnHitTarget(other);

            if (!isPierce)
                Destroy(gameObject);
        }
    }
    void BasicHitEffect(Collider target)
    {
        target.GetComponent<ICanTakeDmg>().TakeDamage(damage);
        if (isHostile)
        {
            DodgeJudger.Instance.SuccessfullyHit(gameObject);
        }
    }
}
