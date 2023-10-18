using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatHoof : TrinketInfo
{
    // Start is called before the first frame update
    void Start()
    {
        SetTrinketItemCode(1);
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerMoveSpeed += 0.16f;
        PlayerManager.instance.playerShotDelay += 0.16f;

    }
}
