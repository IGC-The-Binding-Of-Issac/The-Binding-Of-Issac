using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);
        SetItemString("돌연변이 거미",
                      "네 갈래 샷",
                      "습득 시 이마에 눈이 두 개 더 생긴다." +
                      "\n 눈물이 4갈래로 나간다.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.42f;
        PlayerManager.instance.SetHeadSkin(2);
        PlayerManager.instance.CheckedShotDelay();
    }
}