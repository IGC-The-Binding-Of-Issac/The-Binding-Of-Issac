using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : TrinketInfo
{
    float beforeDropShotDelay;
    float beforeDropDamage;
    float beforeDropMoveSpeed;

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
        PlayerManager.instance.playerShotDelay -= 0.3f;
        PlayerManager.instance.playerDamage -= 0.2f;
        PlayerManager.instance.playerMoveSpeed -= 0.34f;
        PlayerManager.instance.CheckedShotDelay();
    }

    public override void DropTrinket()
    {
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;
        beforeDropDamage = PlayerManager.instance.playerDamage;
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay + 0.3f;
        PlayerManager.instance.playerDamage = beforeDropDamage +  0.2f;
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed + 0.34f;
    }
}
