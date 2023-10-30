using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFingers : ActiveInfo
{
    
    void Start()
    {
        SetActiveItem(2, 0);    
    }

    public override void UseActiveItem()
    {
        if(ItemManager.instance.coinCount > 0)
        {
        ItemManager.instance.coinCount--;
        PlayerManager.instance.playerDamage += 0.13f;
        }
    }
}
