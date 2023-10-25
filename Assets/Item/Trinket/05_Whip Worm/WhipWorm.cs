using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWorm : TrinketInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetTrinketItemCode(5);
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerRange += 2f;
        PlayerManager.instance.playerShotDelay += 0.3f;
    }
}
