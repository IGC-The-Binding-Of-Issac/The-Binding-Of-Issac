using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemInfo
{
    public Sprite HaloImg;
    public override void Start()
    {
        base.Start();
        SetItemCode(11);
        SetItemString("광륜",
                      "천사",
                      "습득 시 최대 체력 + 1칸"
                    + "\n공격속도 + 0.07"
                    + "\n공격력 - 0.3");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 2;
        UIManager.instance.AddHeart();
        PlayerManager.instance.playerShotDelay -= 0.07f;
        PlayerManager.instance.playerDamage -= 0.3f;

        base.UseItem();
        //캐릭터 머리 위에 천사링 생성
        GameManager.instance.playerObject.GetComponent<PlayerController>().HeadItem.GetComponent<SpriteRenderer>().sprite = HaloImg;

    }
}
