using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePill : ActiveInfo
{
    private void Start()
    {
        SetActiveItem(9, 0);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerTearSpeed -= 0.15f;
        Destroy(gameObject);
    }
}
