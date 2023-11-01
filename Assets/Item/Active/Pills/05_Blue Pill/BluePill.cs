using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePill : ActiveInfo
{
    private void Start()
    {
        SetActiveItem(5, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMoveSpeed += 0.15f;
        Destroy(gameObject);
    }
}

