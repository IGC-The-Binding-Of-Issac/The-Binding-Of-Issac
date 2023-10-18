using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    private void Start()
    {
           SetTrinketItemCode(0);  
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerMoveSpeed -= 0.24f;
        PlayerManager.instance.playerHp -= 1;
        PlayerManager.instance.playerShotDelay += 0.2f;
    }
}
