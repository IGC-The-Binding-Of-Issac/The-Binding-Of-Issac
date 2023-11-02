using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : ItemInfo
{
    public GameObject playerObj;
    private void Start()
    {
        SetItemCode(3);
        SetItemString("요술 버섯",
            "모두 좋은!",
            "습득 시 최대 체력 + 1칸" +
            "\n 체력을 모두 회복" +
            "\n 이동속도 + 0.3" +
            "\n 공격력 + 0.3" +
            "\n 공격력 * 1.5배" +
            "\n 사거리 + 2.5" +
            "\n 눈물 크기 * 1.1배" +
            "\n 플레이어 크기 * 1.1배");
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
        PlayerManager.instance.ChgPlayerSize();
    }
}
