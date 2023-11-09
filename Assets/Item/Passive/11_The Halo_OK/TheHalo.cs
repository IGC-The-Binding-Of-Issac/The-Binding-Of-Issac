using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemInfo
{
    public Sprite HaloImg;
    private void Start()
    {
        SetItemCode(11);
        SetItemString("광륜",
                      "천사",
                      "습득 시 최대 체력 + 1칸"
                    + "공격속도 + 0.07"
                    + "공격력 - 0.3");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 2;
        PlayerManager.instance.playerShotDelay -= 0.07f;
        PlayerManager.instance.playerDamage -= 0.3f;
        PlayerManager.instance.CheckedShotDelay();
        GameManager.instance.playerObject.GetComponent<PlayerController>().HeadItem.GetComponent<SpriteRenderer>().sprite = HaloImg;
        //캐릭터가 하늘을 날 수 있게 된다.

    }
}
