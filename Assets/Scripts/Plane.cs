using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Rigidbody rb;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    //public void OnSpawn()
    //{
    //    player.SubscribeSwerve(transform);
    //}
    //public void OnDespawn()
    //{
    //    player.UnsubscribeSwerve(transform);
    //}
    void FixedUpdate()
    {
        float vy=0, vz=0;
        vy = -transform.position.y*player.Speed/4;
        vz = -player.Speed;
        Vector3 moveSpeed = new Vector3(0, vy, vz);


        if (rb != null)
        {
            /*Transform tf = child.transform;
            tf.position = new Vector3(0,0,tf.position.z);
            Debug.Log(child.transform.position);*/
            rb.velocity = moveSpeed;
        } 

    }
    private void Update()
    {
    }
}
