using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(6, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerShotDelay -= 0.15f;
        Destroy(gameObject);
    }
}
