using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsKnife : ItemInfo
{

    private void Start()
    {
        SetItemCode(13);
    }
    public override void UseItem()
    {
        //눈물이 사라지고 눈물 대신 칼이 부메랑처럼 나간다.
        PlayerManager.instance.playerDamage *= 4f;
        PlayerManager.instance.playerShotDelay *= 2.5f;
        PlayerManager.instance.CheckedShotDelay();
    }
}
