using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMagic : CardProto
{
    public override void OnUse()
    {
        base.OnUse();
        Player player = Player.Instance;
        player.Speed = player.Hp = (player.Speed + player.Hp) / 2;
    }
}
