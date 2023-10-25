using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no : TrinketInfo
{
    public float beforeMoveSpeed;
    public float beforeShotDelay;

    private void Start()
    {
        SetTrinketItemCode(7);
    }

    public override void GetItem()
    {
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay; 
        PlayerManager.instance.playerMoveSpeed /= 2f;
        PlayerManager.instance.playerShotDelay *= 2f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed; 
        PlayerManager.instance.playerShotDelay = beforeShotDelay;
    }
}
