using System;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 羽神
/// </summary>
public class CardFeatherGod : CardProto
{
    CardFeatherGodSO m_cardSO;
    protected override void Awake()
    {
        base.Awake();
        m_cardSO = cardSO as CardFeatherGodSO;
    }
    public override void OnUse()
    {
        base.OnUse();
        Player.Instance.GetComponent<BuffMgr>().AddBuff(new MyBuff(m_cardSO.BuffSprite, m_cardSO.ShootInterval, SpawnBullets));
    }
    public class MyBuff : Buff
    {
        UnityAction Shoot;
        float shootInterval;
        float shootTimer;
        public MyBuff(Sprite ui, float _shootInter,UnityAction SpawnBullet) : base("羽神", ui)
        {
            shootInterval = _shootInter;
            shootTimer = _shootInter;

            Shoot += SpawnBullet;
        }
        protected override void HandleLastingEffect(Transform target)
        {
            base.HandleLastingEffect(target);
            shootTimer-=Time.deltaTime;
            if (shootTimer < 0)
            {
                shootTimer = shootInterval;
                Shoot.Invoke();
            }
        }
    }
}
