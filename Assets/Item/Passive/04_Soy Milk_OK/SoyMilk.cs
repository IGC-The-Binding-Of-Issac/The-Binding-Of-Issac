using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(4);
        SetItemString("두유",
                      "쉬 마려워..",
                      "습득 시 공격력 * 0.2" 
                    + "\n공격속도 * 5.5");
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
