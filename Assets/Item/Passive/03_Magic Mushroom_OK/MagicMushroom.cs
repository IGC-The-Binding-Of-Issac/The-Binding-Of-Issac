using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : ItemInfo
{
    public GameObject playerObj;
    public override void Start()
    {
        base.Start();
        SetItemCode(3);
        SetItemString("요술 버섯",
                      "모두 좋은!",
                      "습득 시 최대 체력 + 1칸" 
                    + "\n체력을 모두 회복" 
                    + "\n이동속도 + 0.3" 
                    + "\n공격력 + 0.3"
                    + "\n공격력 * 1.5배" 
                    + "\n사거리 + 2.5" 
                    + "\n눈물 크기 * 1.1배" 
                    + "\n플레이어 크기 * 1.1배");
    }

    public override void UseItem()
    {
        playerObj = GameManager.instance.playerObject;

        PlayerManager.instance.playerMaxHp += 2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        UIManager.instance.AddHeart();
        UIManager.instance.SetPlayerCurrentHP();

        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerDamage *= 1.5f;
        PlayerManager.instance.playerMoveSpeed += 0.3f;
        PlayerManager.instance.playerRange += 2.5f;
        PlayerManager.instance.playerTearSize *= 1.15f;
        PlayerManager.instance.playerSize *= 1.2f;
        base.UseItem();
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.ChgPlayerSize();
    }
}
