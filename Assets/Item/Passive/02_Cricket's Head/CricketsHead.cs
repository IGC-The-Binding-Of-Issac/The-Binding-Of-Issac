using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 0.5f;
        PlayerManager.instance.playerDamage *= 1.5f;
        //눈물의 크기가(Scale) 1.1배 커진다.
    }
}
