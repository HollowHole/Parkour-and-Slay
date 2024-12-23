
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ICanTakeDmg,ICanAffectSpeed,ICanShowBuffUI
{
    public const float BulletShootHeight = 0.8f;//子弹发射的高度（距离playerPosition)
    public const float PlayerInvincibilityFrameOnHit = 0.1f;
    public static Player Instance { get; private set; }
    [SerializeField]public PlayerIniData InitDataSO;
    // Start is called before the first frame update
    
    

    [HideInInspector]public float Speed { get;  private set; }
    float SpeedUpRate;
    bool jumpInput;
    float jumpHeight;
    private Vector3 velocity;
    float LRMoveSpeed;
    private bool isGrounded;

    private Rigidbody rb;
    private MeshCollider meshCollider;
    Transform buffUIZone;
    BuffMgr buffMgr;

    Camera cam;

    private float hp;
    private float MaxHp;
    private float InvincibalTimer;
    public bool isInvincibal => InvincibalTimer > 0;
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
                // PlayerDie();
            }
            else
            {
                hp = value>MaxHp?MaxHp: value;
            }
            OnHpChange?.Invoke(hp,MaxHp);
        }
    }

    private void Awake()
    {
        Instance = this;

        ReadInitData();

        meshCollider = GetComponentInChildren<MeshCollider>();
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        buffMgr = GetComponent<BuffMgr>();
    }
    private void Start()
    {
        buffUIZone = GameObject.Find("BuffUIZone").transform;

        LevelMgr.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void ReadInitData()
    {
        MaxHp = InitDataSO.IniMaxHp;
        hp = MaxHp;
        jumpHeight = InitDataSO.IniJumpHeight;
        LRMoveSpeed = InitDataSO.LRMoveSpeed;
        Speed = InitDataSO.IniSpeed;
        SpeedUpRate = InitDataSO.IniSpeedUpRate;
    }

    void OnLevelEnd()
    {
        buffMgr.ClearAllBuff();
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
    void PlayerDie()
    {
        //死亡动画，玩家死亡音效（如果有游戏失败音效，放GameOverMgr里
        transform.localScale = Vector3.zero;
    }

    private void HandleSpeedUp()
    {
        Speed += SpeedUpRate * Time.deltaTime;
        Speed = Math.Clamp(Speed, 0, InitDataSO.SpeedLimit);
    }

    private void Update()
    {
        if(InvincibalTimer > 0 )
            InvincibalTimer -= Time.deltaTime;

        HandleInput();
        buffMgr.HandleBuffEffect();
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
        float radius = meshCollider.bounds.extents.x * 0.9f;//避免侧面碰撞
        //float overLapCapsuleOffset = meshCollider.bounds.extents.y + 0.1f;//到玩家脚底的距离+0.1f
        //Vector3 pointBottom = transform.position  - transform.up * (overLapCapsuleOffset);
        //Vector3 pointTop = transform.position + transform.up * meshCollider.bounds.size.y/2 - transform.up * radius;
        Vector3 pointBottom = transform.position + transform.up * 0.2f;
        Vector3 pointTop = pointBottom + transform.up * meshCollider.bounds.extents.y;
        LayerMask ignoreLayer = ~LayerMask.GetMask("Player");

        Collider[] colliders = Physics.OverlapCapsule(pointBottom, pointTop, radius, ignoreLayer);
        Debug.DrawLine(pointBottom, pointTop,Color.black);

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
        velocity.z = -transform.position.z;
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
    
    void ICanTakeDmg.TakeDamage(float damage)
    {
        if (isInvincibal)
        {
            return;
        }
        Hp-=damage;
        if (damage > 0)
            InvincibalTimer = PlayerInvincibilityFrameOnHit;
    }

    public void AffectSpeed(float value)
    {
        Speed += value;
    }

    public GameObject ShowThisUI(Sprite sprite)
    {
        return BuffUIZone.Instance.AddBuffUI(sprite);
    }
}
