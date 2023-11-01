using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerCard : ActiveInfo
{
    void Awake()
    {
        SetActiveItem(4, 4);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMaxHp += 1;
    }

    public override void CheckedItem()
    {
        
    }
}
