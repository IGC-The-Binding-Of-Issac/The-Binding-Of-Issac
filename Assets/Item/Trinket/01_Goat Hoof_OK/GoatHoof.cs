using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatHoof : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;
    void Awake()
    {
        SetTrinketItemCode(1);
        SetTrinketString("염소 발굽",
                         "이동 속도 증가, 공격 속도 감소",
                         "습득 시 이동속도 + 0.16" +
                         "\n공격속도 - 0.16");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerMoveSpeed += 0.16f;
        PlayerManager.instance.playerShotDelay += 0.16f;
    }
    public override void DropTrinket()
    {
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed - 0.16f;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay - 0.16f;
    }
}

