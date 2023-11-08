using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dr_fetus : ItemInfo
{
    public GameObject attackBomb;
    private void Awake()
    {
        SetItemCode(16);
        SetItemString("태아 박사",
                      "폭탄 발사",
                      "습득 후 공격 시 폭탄을 발사한다."
                    + "공격속도 * 0.5");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.5f;
        PlayerManager.instance.tearObj = attackBomb;
    }
}
