using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFingers : ActiveInfo
{
    
    void Awake()
    {
        SetActiveItem(1, 0);    
    }

    public override void UseActive()
    {
        if(ItemManager.instance.coinCount > 0)
        {
        ItemManager.instance.coinCount--;
        PlayerManager.instance.playerDamage += 0.13f;
        }
    }
}
