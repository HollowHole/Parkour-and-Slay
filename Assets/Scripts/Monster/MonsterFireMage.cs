using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFireMage : RangedMonsterProto
{
    MonsterFireMageSO m_monsterSO;
    protected override void Awake()
    {
        base.Awake();
        m_monsterSO = base.monsterSO as MonsterFireMageSO;
    }
    protected override void SpawnBullets()
    {
        base.SpawnBullets();
        //修改子弹位置，同鬼影宝

        SetMyBulletInitialSpeed();  
        
    }
    void SetMyBulletInitialSpeed()
    {
        foreach (GameObject b in myBullets)
        {
            Vector3 DropPos = Vector3.zero;
            DropPos.x = Random.Range(-3f, 3);
            DropPos.z = Random.Range(-m_monsterSO.DropPosBiasZ, m_monsterSO.DropPosBiasZ);

            Vector3 horizontalDist = (DropPos - b.transform.position);
            horizontalDist.y = 0;
            Vector3 horizontalSpeed = horizontalDist.normalized * Random.Range(m_monsterSO.HorizontalSpeedMin, m_monsterSO.HorizontalSpeedMax) * Global.SpeedFactor;

            float dropTime = horizontalDist.magnitude / horizontalSpeed.magnitude;

            //v0​ = 0.5gt−Y/t​
            Vector3 verticalSpeed = Vector3.zero;
            verticalSpeed.y = 0.5f * Global.GRAVITY * dropTime - b.transform.position.y / dropTime;

            Vector3 targetSpeed = horizontalSpeed+verticalSpeed;
            
            b.GetComponent<BulletProto>().SetInitialSpeed(targetSpeed / Global.SpeedFactor);
        }
    }
    
}
