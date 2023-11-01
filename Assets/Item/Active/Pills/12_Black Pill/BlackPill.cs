using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(12, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 0.08f;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        Destroy(gameObject);
    }
}
