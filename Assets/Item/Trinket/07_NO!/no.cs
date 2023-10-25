using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no : TrinketInfo
{

    float beforeMoveSpeed;
    float beforeShotDelay;
    private void Start()
    {
        SetTrinketItemCode(7);
    }

    public override void GetItem()
    {
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        Debug.Log("기존 스피드 : " + beforeMoveSpeed);
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerMoveSpeed /= 2f;
        PlayerManager.instance.playerShotDelay *= 2f;
    }
}
