using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DodgeJudger : MonoBehaviour
{
    public static DodgeJudger Instance;

    public Action OnDodgeSuccess;
    public Action OnDodgeFailure;
    void Awake()
    {
        Instance = this;
    }
    

    List<GameObject> DodgeSuccessRecord = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")||
            (other.CompareTag("Bullet") && other.GetComponent<BulletProto>().isHostile
                                        && other.GetComponent<BulletProto>().GetSource=="Monster"))
        {
            DodgeSuccessRecord.Add(other.gameObject);
        }
    }
    public void SuccessfullyHit(GameObject gameObject)
    {
        DodgeSuccessRecord.Remove(gameObject);
        OnDodgeFailure?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (DodgeSuccessRecord.Contains(other.gameObject))
        {
            OnDodgeSuccess?.Invoke();
        }
    }
}
