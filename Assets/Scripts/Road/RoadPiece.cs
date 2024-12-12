using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPiece : MonoBehaviour
{
    Player player;
    RoadBlockObjectPool pool;

    public Rigidbody rb;
    
    public Transform TrapPos;

    private bool isMoveable;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();

        rb = GetComponent<Rigidbody>();

        isMoveable = true;
    }
    private void Start()
    {
        pool = RoadBlockObjectPool.Instance;
        SwerveManager.instance.OnSwerveBegin += StopMoving;
        SwerveManager.instance.OnSwerveEnd += AllowMoving;
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
        
        Move();
    }
    private void Move()
    {
        if (!isMoveable)
        {
            return;
        }
        if(Mathf.Abs(transform.position.y) < 0.05f)
        {
            Vector3 newPos = transform.position;
            newPos.y = 0;
            transform.position = newPos;
        }

        float vy = 0, vz = 0;
        vy = -transform.position.y * player.Speed / 2;
        vz = -player.Speed;
        if (rb != null)
        {
            /*Transform tf = child.transform;
            tf.position = new Vector3(0,0,tf.position.z);
            Debug.Log(child.transform.position);*/
            rb.velocity = new Vector3(0, vy, vz) * Global.SpeedFactor;
        }
    }
    private void StopMoving()
    {
        isMoveable = false;
        rb.velocity = Vector3.zero;
    }
    private void AllowMoving()
    {
        isMoveable = true;
    }
    private void Update()
    {
        
    }
}
