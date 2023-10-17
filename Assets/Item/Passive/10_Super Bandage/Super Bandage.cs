using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBandage : ItemInfo
{

    private void Start()
    {
        SetItemCode(10);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp-=2;
        PlayerManager.instance.playerHp-=2;
        PlayerManager.instance.playerShotDelay-=0.1f;
        //Ä³¸¯ÅÍÀÇ ´«¹°ÀÌ »¡°²°Ô º¯ÇÑ´Ù.
        
    }
}
