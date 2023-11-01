using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRedPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(10, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage -= 0.15f;
        Destroy(gameObject);
    }
}
