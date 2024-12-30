using System;
using System.Collections;
using UnityEngine;
public class CardTwinLightBall : CardProto
{
    public override void OnUse()
    {
        base.OnUse();
        StartCoroutine( SpawnBulletsAgainSoon());
    }

    IEnumerator SpawnBulletsAgainSoon()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnBullets();
    }
}
