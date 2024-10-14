using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    [SerializeField]private List<Transform> Objects2Swerve = new List<Transform>();
    Camera cam;

    public float Speed;
    public float jumpHeight;
    private Vector3 velocity;
    public float LRMoveSpeed;
    [SerializeField]private bool isGrounded;
    private float capsuleRadius;

    private Global.SwerveDirection _swerveDirection;

    public bool IsSwerving = false;//改成Speed等于0(暂时等于)

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        cam=FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        capsuleRadius = capsuleCollider.radius;
    }
    private void FixedUpdate()
    {
        velocity = rb.velocity;
        GroundedCheck();
        HandleMove();
        HandleJumpAndFall();
        rb.velocity = velocity;
    }
    private void GroundedCheck()
    {
        float radius = capsuleRadius * 0.9f;//避免侧面碰撞
        float overLapCapsuleOffset = 1.1f;//到玩家脚底的距离+0.1f
        Vector3 pointBottom = transform.position + transform.up * radius - transform.up * (overLapCapsuleOffset);
        Vector3 pointTop = transform.position + transform.up * capsuleCollider.height / 2 - transform.up * radius;
        LayerMask ignoreLayer = ~LayerMask.GetMask("Player");

        Collider[] colliders = Physics.OverlapCapsule(pointBottom, pointTop, radius, ignoreLayer);
        Debug.DrawLine(pointBottom, pointTop,Color.green);

        /*foreach (Collider collider in colliders) { 
            Debug.Log(collider.gameObject.name);
        }*/
        if (colliders.Length != 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
    }
    void HandleMove()
    {
        float vx = 0;
        vx = Input.GetAxis("Horizontal") * LRMoveSpeed;
        velocity.x = vx;
    }
    void HandleJumpAndFall()
    {
        if (isGrounded)
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");
                velocity.y = Mathf.Sqrt(2 * Global.GRAVITY * jumpHeight);
            }
            if(velocity.y < -1)
            {
                velocity.y = -0.8f;
            }
        }
        else
        {
            //HandleFall
            velocity.y -= Global.GRAVITY * Time.deltaTime;
        }
    }
    public void SubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Add(tf);
    }
    public void UnsubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Remove(tf);
    }
    public void Swerve(Global.SwerveDirection swerveDirection,Vector3 swerveCenter)
    {
        
        //启动携程
        _swerveDirection = swerveDirection;
        
        StartCoroutine(SwerveAllObjects(swerveDirection,swerveCenter));
        
    }
    private IEnumerator SwerveAllObjects(Global.SwerveDirection swerveDirection, Vector3 swerveCenter)
    {
        Debug.Log("Swerve begin!");
        Debug.Log(swerveCenter);
        IsSwerving = true;
        float swerveAngleLeft = 90;
        Vector3 axis = transform.up;
        while (swerveAngleLeft>0)
        {
            float swerveAngle = 1000 * Time.deltaTime;
            swerveAngle = swerveAngle<swerveAngleLeft?swerveAngle:swerveAngleLeft;
            swerveAngleLeft -= swerveAngle;
            if (swerveDirection == Global.SwerveDirection.RIGHT) swerveAngle = -swerveAngle;

            foreach (Transform t in Objects2Swerve)
            {
                t.RotateAround(swerveCenter,axis, swerveAngle);
            }
            
            yield return null;
            
        }
        /*bool breakFlag = false;
        while (!breakFlag)
        {
            Vector3 offset = Vector3.zero;
            foreach(Transform t in Objects2Swerve)
            {
                Vector3 pos = t.position;

                *//*pos.x *=  (1 - Time.deltaTime);
                if (Mathf.Abs(t.position.x) < 0.15f)
                {
                    pos.x = 0;
                    breakFlag = true;
                }*//*
                pos.x = 0;
                breakFlag = true;

                offset = t.position - pos;
                t.position = pos;
            }
            //玩家的位置也要修改
            transform.position -= offset;
            yield return null;
        }*/

        IsSwerving = false;
        
        FindObjectOfType<RoadSpawn>().EnableSpawn();
        
    }

}
