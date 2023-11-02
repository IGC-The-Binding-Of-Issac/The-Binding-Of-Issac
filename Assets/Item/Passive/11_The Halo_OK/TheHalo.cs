using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemInfo
{
    private void Start()
    {
        SetItemCode(11);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 2;
        PlayerManager.instance.playerShotDelay -= 0.07f;
        PlayerManager.instance.playerDamage -= 0.3f;
        PlayerManager.instance.CheckedShotDelay();
        //캐릭터가 하늘을 날 수 있게 된다.

    }
}
