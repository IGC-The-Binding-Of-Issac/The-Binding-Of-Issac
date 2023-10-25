using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindrange : TrinketInfo
{
    float beforeRange;
    float beforeDamage;
    float beforeMoveSpeed;
    private void Start()
    {
        SetTrinketItemCode(6);
    }

    public override void GetItem()
    {
        beforeRange = PlayerManager.instance.playerRange;
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        PlayerManager.instance.playerRange -= 0.99f;
        PlayerManager.instance.playerDamage += 3f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerRange = beforeRange;
        PlayerManager.instance.playerDamage = beforeDamage;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
    }
}
