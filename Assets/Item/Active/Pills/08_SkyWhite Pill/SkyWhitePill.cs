using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhitePill : ActiveInfo
{
    private void Start()
    {
        SetActiveItem(8, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerRange -= 0.15f;
        Destroy(gameObject);
    }
}
