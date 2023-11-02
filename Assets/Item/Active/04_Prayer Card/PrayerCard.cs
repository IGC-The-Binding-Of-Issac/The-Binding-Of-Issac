using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerCard : ActiveInfo
{
    void Awake()
    {
        SetActiveItem(4, 4);
        SetActiveString("기도자 카드",
            "재사용가능한 영원",
            "사용 시 최대 체력 + 1칸");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
        PlayerManager.instance.playerMaxHp += 1;
        canUse = false;
        Invoke("SetCanUse", 1f);
        }
    }
}
