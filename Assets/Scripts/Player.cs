
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]public PlayerIniData InitDataSO;
    // Start is called before the first frame update
    
    

    [HideInInspector]public float Speed { get; set; }
    float SpeedUpRate;
    bool jumpInput;
    float jumpHeight;
    private Vector3 velocity;
    float LRMoveSpeed;
    private bool isGrounded;
    private float capsuleRadius;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    Camera cam;

    private float hp;
    private float MaxHp;
    public Action<float,float> OnHpChange;
    
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(value < 0)
            {
                hp = 0;
            }
            else
            {
                hp = value>MaxHp?MaxHp: value;
            }
            OnHpChange?.Invoke(value,MaxHp);
        }
    }

    private void Awake()
    {
        ReadInitData();

        capsuleCollider = GetComponent<CapsuleCollider>();
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        capsuleRadius = capsuleCollider.radius;
    }

    private void ReadInitData()
    {
        MaxHp = InitDataSO.IniMaxHp;
        jumpHeight = InitDataSO.IniJumpHeight;
        LRMoveSpeed = InitDataSO.LRMoveSpeed;
        Speed = InitDataSO.IniSpeed;
        SpeedUpRate = InitDataSO.IniSpeedUpRate;
    }


    private void FixedUpdate()
    {
        velocity = rb.velocity;
        GroundedCheck();
        HandleMove();
        HandleJumpAndFall();
        HandleSpeedUp();
        rb.velocity = velocity;
    }

    private void HandleSpeedUp()
    {
        Speed += SpeedUpRate * Time.deltaTime;
        Speed = Math.Clamp(Speed, 0, InitDataSO.SpeedLimit);
    }

    private void Update()
    {
        HandleInput();
        
    }
    private void HandleInput()
    {
        if (Input.GetButton("Jump"))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        
    }
    private void GroundedCheck()
    {
        float radius = capsuleRadius * 0.9f;//±ÜÃâ²àÃæÅö×²
        float overLapCapsuleOffset = 1.1f;//µ½Íæ¼Ò½Åµ×µÄ¾àÀë+0.1f
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
            if (jumpInput)
            {
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
    
    public void TakeDamage(float damage)
    {
        Hp-=damage;
    }
}
