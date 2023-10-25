using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindrange : TrinketInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetTrinketItemCode(6);
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerRange -= 0.99f;
        PlayerManager.instance.playerDamage += 3f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
    }
}
