using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProto : MonoBehaviour
{
    [SerializeField] protected BulletProtoSO bulletSO;
    Rigidbody rb;
    private string targetTag;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity  = new Vector3(0,0,bulletSO.Speed);

        targetTag = bulletSO.TargetTag;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            OnHitTarget(other);
        }
    }
    protected virtual void OnHitTarget(Collider target)
    {
        Debug.Log("hit function not overrided!");
    }
}
