using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeremonialRobes : ItemInfo
{


    private void Start()
    {
        SetItemCode(6);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 4;
        PlayerManager.instance.playerDamage += 1;

        //캐릭터 텍스처 변경
    }
}
