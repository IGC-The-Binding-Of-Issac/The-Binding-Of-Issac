using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDamage;
    float beforeMoveSpeed;
    float beforeShotDelay;

    private void Start()
    {
            SetTrinketItemCode(0);
            SetTrinketString("진드기",
                             "으, 징그러워",
                             "습득 시 공격력 + 0.3" 
                           + "\n이동속도 - 0.24" 
                           + "\n현재 체력 - 2칸" 
                           + "\n공격 속도 - 0.2");
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerMoveSpeed -= 0.24f;

        PlayerManager.instance.playerHp -= 2;
        PlayerManager.instance.CheckedPlayerHP();
        UIManager.instance.SetPlayerCurrentHP();

        PlayerManager.instance.playerShotDelay += 0.2f;
        PlayerManager.instance.CheckedShotDelay();

        if(PlayerManager.instance.playerHp <= 0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
        }
    }

    public override void DropTrinket()
    {
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerDamage = beforeDamage - 0.3f;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed + 0.24f;
        PlayerManager.instance.playerShotDelay = beforeShotDelay - 0.2f;
    }
}
