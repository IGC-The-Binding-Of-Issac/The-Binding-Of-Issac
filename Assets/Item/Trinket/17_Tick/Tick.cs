using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    public override void GetItem()
    {
        PlayerManager.instance.playerMoveSpeed -= 0.2f;
        PlayerManager.instance.playerShotDelay -= 0.1f;
        PlayerManager.instance.playerRange -= 0.15f;
        PlayerManager.instance.playerDamage += 0.7f;
    }
}
