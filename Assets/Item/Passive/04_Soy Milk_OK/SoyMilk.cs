using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
    private void Start()
    {
        SetItemCode(4);
        SetItemString("두유",
                      "쉬 마려워..",
                      "습득 시 공격력 배율 * 0.2배" +
                      "\n공격 속도 * 5.5배");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerDamage *= 0.2f;
        PlayerManager.instance.playerShotDelay /= 5.5f;
        PlayerManager.instance.playerTearSize *= 0.4f;
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.CheckedShotDelay();
    }
}
