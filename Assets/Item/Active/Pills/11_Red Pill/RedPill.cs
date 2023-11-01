using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : ActiveInfo
{
    private void Start()
    {
        SetActiveItem(11, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerHp += 1;
        PlayerManager.instance.playerMaxHp += 1;
        Destroy(gameObject);
    }
}
