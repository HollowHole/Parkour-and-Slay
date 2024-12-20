using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProto : MonoBehaviour,ICanTakeDmg
{
    [SerializeField]protected MonsterProtoSO monsterSO;
    Player player;
    Rigidbody rb;

    protected float speed;
    private float hp;
    private float MaxHp;
    public Action<float, float> OnHpChange;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < 0)
            {
                hp = 0;
            }
            else
            {
                hp = value > MaxHp ? MaxHp : value;
            }
            OnHpChange?.Invoke(hp, MaxHp);
        }
    }

    protected virtual void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        ReadSO();
        
    }
    protected virtual void Start()
    {
        player = Player.Instance;
    }

    protected virtual void ReadSO()
    {
        hp = MaxHp = monsterSO.Hp;
        speed = monsterSO.Speed;
    }
    protected virtual void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    protected virtual void HandleMovement()
    {
        Vector3 v = (player.transform.position - transform.position).normalized;
        v.y = 0;
        v *= speed;
        v -= new Vector3(0, 0, player.Speed);
        rb.velocity = v * Global.SpeedFactor;

        speed += monsterSO.SpeedUpRate * Time.deltaTime;
        speed = Mathf.Clamp(speed,0,monsterSO.MaxSpeedLimit);
        
    }
    private void HandleRotation()
    {
        transform.LookAt(player.transform.position);
    }
    public MonsterProtoSO GetSO()
    {
        return monsterSO;
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<ICanTakeDmg>().TakeDamage(monsterSO.CollideDamage);
            player.GetComponent<ICanAffectSpeed>().AffectSpeed(monsterSO.AffectSpeedAbility);
        }
    }
}
