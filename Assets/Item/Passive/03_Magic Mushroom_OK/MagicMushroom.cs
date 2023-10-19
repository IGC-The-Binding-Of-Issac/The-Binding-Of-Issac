using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : ItemInfo
{
    public GameObject playerObj;
    private void Start()
    {
        SetItemCode(3);
    }

    public override void UseItem()
    {
        playerObj = GameManager.instance.playerObject;

        PlayerManager.instance.playerMaxHp+=2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        PlayerManager.instance.playerMoveSpeed += 0.3f;
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerDamage *= 1.5f;
        PlayerManager.instance.playerRange += 2.5f;
        PlayerManager.instance.playerTearSize *= 1.1f;
        PlayerManager.instance.playerSize *= 1.1f;
        PlayerManager.instance.ChgTearSize();

        // 해당 기능 오류납니다 수정바람
        // PlayerManager.instance.ChgPlayerSize(); 
    }
}
