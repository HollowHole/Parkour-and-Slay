using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletProto : MonoBehaviour
{
    Rigidbody rb;

    string targetTag;
    float initSpeed;
    bool isPierce;
    float damage;

    public Action<Collider> OnHitTarget;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        

        OnHitTarget += BasicHitEffect;
    }
    private void Start()
    {
        rb.velocity = new Vector3(0, 0, initSpeed);
    }
    public void ApplyBasicAttri(string t,float s,bool isP,float D)
    {
        targetTag = t;
        initSpeed = s;
        isPierce = isP;
        damage = D;
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
        
    }
}
