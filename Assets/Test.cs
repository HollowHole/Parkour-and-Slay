using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    [RequireComponent(typeof(Rigidbody))]
    public class Test : MonoBehaviour
    {
        Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            rb.velocity = Vector3.zero;
            Debug.Log(rb.velocity);
        }
    }
}
