using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProto : MonoBehaviour
{
    public float applyEffectTimeSpan;
    private float applyEffectTimer;
    private void OnTriggerStay(Collider other)
    {
        if (applyEffectTimer<=0 && other.CompareTag("Player")){
            Affect(other.gameObject);
            ResetTimer();
        }
    }
    void ResetTimer()
    {
        applyEffectTimer = applyEffectTimeSpan;
    }
    private void FixedUpdate()
    {
        if (applyEffectTimer > 0)
        {
            applyEffectTimer -= Time.deltaTime;
        }
    }
    protected virtual void Affect(GameObject gameObject)
    {
        Debug.Log("this trap's effect are not overrided!");
    }
}
