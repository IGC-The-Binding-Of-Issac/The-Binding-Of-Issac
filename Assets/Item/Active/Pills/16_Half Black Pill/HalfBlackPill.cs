using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(16, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerHp -= 1;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        PlayerManager.instance.playerDamage += 0.15f;
        Destroy(gameObject);
    }
}
