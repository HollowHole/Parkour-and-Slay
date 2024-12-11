using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDash : CardProto
{
    private CardDashSO m_cardSO;
    protected override void Awake()
    {
        base.Awake();
        m_cardSO = base.cardSO as CardDashSO;
    }
    public override void OnUse()
    {
        base.OnUse();
        FindObjectOfType<Player>().Speed += m_cardSO.Value;
    }
}
