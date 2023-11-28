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
                      "습득 시 공격력 * 0.3" 
                    + "\n공격속도 * 2.5");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerDamage *= 0.3f;
        PlayerManager.instance.playerShotDelay /= 2.5f;
        PlayerManager.instance.playerTearSize *= 0.45f;
        PlayerManager.instance.ChgTearSize();
        base.UseItem();
    }
}
