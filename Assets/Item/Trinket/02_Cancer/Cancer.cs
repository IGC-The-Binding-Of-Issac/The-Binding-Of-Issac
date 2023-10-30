using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : TrinketInfo
{
    float beforeShotDelay;
    float beforeDamage;
    float beforeMoveSpeed;
    private void Start()
    {
        SetTrinketItemCode(2);
    }

    public override void GetItem()
    {
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        PlayerManager.instance.playerShotDelay -= 0.3f;
        PlayerManager.instance.playerDamage -= 0.2f;
        PlayerManager.instance.playerMoveSpeed -= 0.34f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerShotDelay = beforeShotDelay;
        PlayerManager.instance.playerDamage = beforeDamage;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
    }
}
