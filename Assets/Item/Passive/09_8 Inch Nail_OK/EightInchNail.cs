using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightInchNail : ItemInfo
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetItemCode(9);
        SetItemString("8인치 대못",
                      "박살내 버려!",
                      "캐릭터가 눈물 대신 못을 발사한다." +
                      "\n 공격력 + 1.5");
    }

    public override void UseItem()
    {
        base.UseItem();
        PlayerManager.instance.playerDamage += 1.5f;
        if (!ItemManager.instance.PassiveItems[16])
        {
            PlayerManager.instance.SetTearSkin(2);
        }
    }
}
