using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    float beforeDamage;
    float beforeMoveSpeed;
    float beforeShotDelay;
    private void Start()
    {
           SetTrinketItemCode(0);  
    }
    public override void GetItem()
    {
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerMoveSpeed -= 0.24f;
        PlayerManager.instance.playerHp -= 2;
        PlayerManager.instance.playerShotDelay += 0.2f;
        if(PlayerManager.instance.playerHp <= 0)
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerDamage = beforeDamage;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
        PlayerManager.instance.playerShotDelay = beforeShotDelay;
    }
}
