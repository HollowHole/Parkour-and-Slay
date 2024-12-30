using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProto : MonoBehaviour
{
    protected Rigidbody rb;

    string sourceTag;
    string targetTag;
    Vector3 initSpeed;
    bool isPierce;
    protected float damage;
    float affectSpeed;

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
        rb.velocity = initSpeed * Global.SpeedFactor;
    }
    protected virtual void Update()
    {
        HandleDisappear();
        
    }
    protected virtual void HandleDisappear()
    {
        if (transform.position.z < -5f || transform.position.z > 100f)
        {
            Destroy(gameObject);
        }
        if (Math.Abs(transform.position.y) > 100f)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyBasicAttri(string _targettag,Vector3 speed,float D,float _affectSpeed,bool isP = false,string _sourcetag = "Player")
    {
        targetTag = _targettag;
        initSpeed = speed;
        isPierce = isP;
        damage = D;
        sourceTag = _sourcetag;
        affectSpeed = _affectSpeed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Transform target = other.transform;
            Debug.Log("Bullet Hit " + target.name);
            //用ICanTakeDmg定位
            ICanTakeDmg cpnt = other.GetComponent<ICanTakeDmg>();
            if (cpnt == null)
                target = target.parent;
            //cpnt.TakeDamage(damage);

            
            OnHitTarget(target);

            if (!isPierce)
                Destroy(gameObject);
        }
    }
    void BasicHitEffect(Transform target)
    {
        
        target.GetComponent<ICanTakeDmg>().TakeDamage(damage);
        ICanAffectSpeed canAffectSpeed =  target.GetComponent<ICanAffectSpeed>();
        if (canAffectSpeed != null) canAffectSpeed.AffectSpeed(affectSpeed);
        if (isHostile)
        {
            DodgeJudger.Instance.SuccessfullyHit(gameObject);
        }
        else if(damage > 0) 
        {
            DmgDisplayOP.Instance.ShowDmgAt(damage, transform.position);
        }
    }
}
