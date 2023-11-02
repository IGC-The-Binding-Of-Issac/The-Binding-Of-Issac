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
        SetTrinketString("암세포",
            "와, 암이다!",
            "습득 시 공격속도 + 0.3" +
            "\n 공격력 - 0.2" +
            "\n 이동 속도 - 0.34");
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
