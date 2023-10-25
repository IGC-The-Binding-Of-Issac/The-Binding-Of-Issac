using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : TrinketInfo
{
    private void Start()
    {
        SetTrinketItemCode(2);
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerShotDelay -= 0.5f;
        PlayerManager.instance.playerDamage -= 0.2f;
        PlayerManager.instance.playerMoveSpeed -= 0.34f;
    }
}
