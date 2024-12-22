using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMgr : MonoBehaviour
{
    public static BonusMgr Instance;

    CardProto CardGonnaChoose;
    private void Awake()
    {
        Instance = this;
    }
    private void LateUpdate()
    {
        //UpdateChosenCard();
    }

    public void Choose(CardProto card)
    {
        CardGonnaChoose = card;
    }
    public void Comfirm()
    {

    }
    public void Skip()
    {

    }
}
