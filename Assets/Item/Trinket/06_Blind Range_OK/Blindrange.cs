using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindrange : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropRange;
    float beforeDropDamage;
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;

    public override void Start()
    {
        base.Start();
        SetTrinketItemCode(6);
        SetTrinketString("눈 먼 분노",
                         "마음의 눈으로 보시오",
                         "습득 시 사거리 * 2.5"
                       + "\n공격력 + 3"
                       + "\n이동속도 - 1.2"
                       + "\n공격 속도 + 0.25");
    }

    public override void GetItem()
    {
        if (PlayerManager.instance.playerRange > 3) PlayerManager.instance.playerRange *= 0.25f;
        PlayerManager.instance.playerDamage += 3f;
        PlayerManager.instance.playerShotDelay -= 0.25f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
        base.GetItem(); 
    }

    public override void DropTrinket()
    {
        beforeDropRange = PlayerManager.instance.playerRange;
        beforeDropDamage = PlayerManager.instance.playerDamage;
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerRange = beforeDropRange /= 0.25f;
        PlayerManager.instance.playerDamage = beforeDropDamage -= 3f;
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed += 1.2f;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay += 0.25f;
        PlayerManager.instance.CheckedStatus();
    }
}
