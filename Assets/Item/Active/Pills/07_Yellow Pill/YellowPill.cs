using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPill : ActiveInfo
{
    private void Start()
    {
        SetActiveItem(7, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 0.15f;
        Destroy(gameObject);
    }
}
