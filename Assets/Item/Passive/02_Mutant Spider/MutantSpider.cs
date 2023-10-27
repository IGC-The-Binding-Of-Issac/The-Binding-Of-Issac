using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);

    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.42f;
        //이마에 눈이 두 개 더 생긴다.
        //캐릭터의 모든 눈에서 눈물을 발사한다. [4발]

    }
}