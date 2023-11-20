using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyTail : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(15);
        SetItemString("±¸ÇÇÀÇ ²¿¸®",
                      "Àß¸° ±¸ÇÇ ²¿¸®",
                      "½Àµæ ½Ã ÃÖ´ë Ã¼·Â 1Ä­À¸·Î °íÁ¤"
                    + "\n¸ñ¼û + 9");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        UIManager.instance.DelHeart();
        PlayerManager.instance.deathCount = 9;
    }
}
