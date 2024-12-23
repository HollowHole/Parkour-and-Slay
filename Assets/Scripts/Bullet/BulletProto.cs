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
    Vector3 initSpeed;
    bool isPierce;
    float damage;

    public Action<Transform> OnHitTarget;

    public bool isHostile => targetTag == "Player";
    public string GetSource => sourceTag;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        

        OnHitTarget += BasicHitEffect;
    }
    private void Start()
    {
        rb.velocity = initSpeed;
    }
    protected virtual void Update()
    {
        HandleDisappear();
    }
    protected virtual void HandleDisappear()
    {
        if (transform.position.z < -5f)
        {
            Destroy(gameObject);
        }
        if(transform.position.z > 100f)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyBasicAttri(string tt,Vector3 s,float D,bool isP = false,string st = "Player")
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
            Transform target = other.transform;
            //用ICanTakeDmg定位
            ICanTakeDmg cpnt = other.GetComponent<ICanTakeDmg>();
            if (cpnt == null)
                target = target.parent;
            //cpnt.TakeDamage(damage);

            Debug.Log("Bullet Hit "+target.name);
            OnHitTarget(target);

            if (!isPierce)
                Destroy(gameObject);
        }
    }
    void BasicHitEffect(Transform target)
    {
        
        target.GetComponent<ICanTakeDmg>().TakeDamage(damage);
        if (isHostile)
        {
            DodgeJudger.Instance.SuccessfullyHit(gameObject);
        }
    }
}
