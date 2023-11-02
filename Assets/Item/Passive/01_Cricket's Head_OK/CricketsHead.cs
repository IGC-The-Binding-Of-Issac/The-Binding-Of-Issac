using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemInfo
{
    private void Start()
    {
        SetItemCode(1);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 0.5f;
        PlayerManager.instance.playerDamage *= 1.5f;
        PlayerManager.instance.playerTearSize *= 1.1f;
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.SetHeadSkin(3);
    }
}
