using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BuffMgr),typeof(Rigidbody))]
public class MonsterProto : MonoBehaviour,ICanTakeDmg
{
    [SerializeField]protected MonsterProtoSO monsterSO;
    protected Player player;
    protected Rigidbody rb;
    protected BuffMgr buffMgr;

    protected float speed;
    public float AffectSpeedAbi {  get; set; }
    public float CollideDmg {  get; set; }
    private float hp;
    private float MaxHp;
    public Action<float, float> OnHpChange;

     public float DmgMagni {  get; set; }
    public float AffeSpeMagni {  get; set; }
     public float TakenDmgMagni{ get; set; }

    [HideInInspector]public Vector3 Center =>center + transform.position;
    Vector3 center;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < 0)
            {   hp = 0;
                //die and ?//TODO drops
                MonsterSpawner.Instance.OnMonsterDisappear(this);
                Destroy(gameObject);
                
            }
            else
            {
                //hp = value > MaxHp ? MaxHp : value;
                hp = value;
            }
            OnHpChange?.Invoke(hp, MaxHp);
        }
    }

    protected virtual void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        buffMgr = GetComponent<BuffMgr>();
        center = GetComponentInChildren<Collider>().bounds.center;
        // rb = GetComponentInChildren<Rigidbody>();
        ReadSO();

        DmgMagni = 1;
        AffeSpeMagni = 1;
        TakenDmgMagni = 1;
    }
    protected virtual void Start()
    {
        player = Player.Instance;
    }

    protected virtual void ReadSO()
    {
        hp = MaxHp = monsterSO.Hp;
        speed = monsterSO.Speed;
        AffectSpeedAbi = monsterSO.AffectSpeedAbility;
        CollideDmg = monsterSO.CollideDamage;
    }
    protected virtual void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleDisappear();
        HandleRepulseMonster();
        // Debug.Log(gameObject.name + " has velocity " + rb.velocity);
    }
    private void HandleRepulseMonster()
    {
        Collider myCollider = GetComponentInChildren<Collider>();
        Bounds bound = myCollider.bounds;
        Vector3 center = bound.center;
        Collider[] colliders = Physics.OverlapBox(center, bound.extents);
        foreach (Collider collider in colliders)
        {
            if (collider == myCollider) continue;
            if (collider.CompareTag("Monster"))
            {
                Bounds colBnd = collider.bounds;
                Vector3 repulsive = colBnd.center - bound.center;//Direction
                repulsive = 1 / repulsive.magnitude * repulsive.normalized;
                repulsive *= 40;

                Rigidbody otherRB = collider.GetComponentInParent<Rigidbody>();
                if(otherRB != null) otherRB.AddForce(repulsive);
            }
        }
    }

    protected virtual void HandleDisappear()
    {
        if (transform.position.z < -5f|| transform.position.y < -100f)
        {
            MonsterSpawner.Instance.OnMonsterDisappear(this);
            Destroy(gameObject);
        }
    }

    protected virtual void HandleMovement()
    {
        Vector3 v = (player.transform.position - transform.position).normalized;
        v.y = 0;
        v *= speed;
        v -= new Vector3(0, 0, player.Speed);
        v.x = Math.Clamp(v.x, -0.04f, 0.04f);
        rb.velocity = v * Global.SpeedFactor;
        //Debug.Log(gameObject.name + " has velocity " + rb.velocity);

        speed += monsterSO.SpeedUpRate * Time.deltaTime;
        speed = Mathf.Clamp(speed,0,monsterSO.MaxSpeedLimit);
        
    }
    protected virtual void HandleRotation()
    {
        
        transform.LookAt(transform.position - Center + player.transform.position + new Vector3(0, Player.BulletShootHeight, 0));
    }
    public MonsterProtoSO GetSO()
    {
        return monsterSO;
    }
    public void TakeDamage(float damage)
    {
        if(damage > 0)
            damage *= TakenDmgMagni;
        Hp -= damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<ICanTakeDmg>().TakeDamage(CollideDmg * DmgMagni);
            player.GetComponent<ICanAffectSpeed>().AffectSpeed(AffectSpeedAbi * AffeSpeMagni);
        }
    }
}
