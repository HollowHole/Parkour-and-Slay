using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlazingRadiance : CardProto
{
    CardBlazingRadianceSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardBlazingRadianceSO;
        base.Awake();
    }
    public override void OnUse()
    {
        base.OnUse();
        Damage += m_cardSO.DMGIncrement;
    }
}
