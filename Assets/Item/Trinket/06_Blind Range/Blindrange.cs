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
        SetTrinketString("눈 먼 분노",
            "마음의 눈으로 보시오",
            "습득 시 사거리 - 0.99"
            + "\n 공격력 + 3"
            + "\n 이동속도 - 1.2");
    }

    public override void GetItem()
    {
        beforeRange = PlayerManager.instance.playerRange;
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        if (PlayerManager.instance.playerRange > 3) PlayerManager.instance.playerRange *= 0.25f;
        PlayerManager.instance.playerDamage += 5f;
        PlayerManager.instance.playerShotDelay -= 0.25f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerRange = beforeRange;
        PlayerManager.instance.playerDamage = beforeDamage;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
    }
}
