using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRetrieve : MonoBehaviour
{
    RoadBlockObjectPool pool;

    private void Start()
    {
        pool = RoadBlockObjectPool.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Try retrieve");
        if (other.CompareTag("RoadBlock"))
        {
            pool.Despawn(other.gameObject);
        }
       
    }
}
