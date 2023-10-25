using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatHoof : TrinketInfo
{
    float beforeMoveSpeed;
    float beforeShotDelay;
    void Start()
    {
        SetTrinketItemCode(1);
    }

    public override void GetItem()
    {
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerMoveSpeed += 0.16f;
        PlayerManager.instance.playerShotDelay += 0.16f;
    }
    public override void DropTrinket()
    {
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
        PlayerManager.instance.playerShotDelay = beforeShotDelay;
    }
}

