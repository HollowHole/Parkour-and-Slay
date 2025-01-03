using System;
using Unity.VisualScripting;
using UnityEngine;
public class IceShield : MonoBehaviour, ICanTakeDmg
{
    Vector3 originScale;
    float fullHp;
    float hp;
    float DmgBonus;
    float AffeSpeBonus;
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
                //die and ?//TODO drops
                OnBroken?.Invoke();
                gameObject.SetActive(false);
            }
            else
            {
                hp = value;
            }
            
        }
    }
    public Action OnBroken;
    private void Awake()
    {
        originScale = transform.localScale;
        
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }
    public void ApplyShieldAttri(float hp,float _dmgBonus,float _afspBonus)
    {
        fullHp = hp;
        DmgBonus = _dmgBonus;
        AffeSpeBonus = _afspBonus;
    }
    public void Recover()
    {
        gameObject.SetActive (true);
        Hp = fullHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        BulletProto bullet = other.GetComponent<BulletProto>();
        if (bullet == null || bullet.GetSource != "Monster")
        {
            return;
        }
            
        bullet.ApplyAffeSpeBonus(AffeSpeBonus);
        bullet.ApplyDmgBonus(DmgBonus);
        
    }
}
