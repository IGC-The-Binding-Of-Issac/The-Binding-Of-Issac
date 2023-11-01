using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPill : ActiveInfo
{
    
    private void Start()
    {
        SetActiveItem(13, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerHp -= 1;
        Destroy(gameObject);
    }
}
