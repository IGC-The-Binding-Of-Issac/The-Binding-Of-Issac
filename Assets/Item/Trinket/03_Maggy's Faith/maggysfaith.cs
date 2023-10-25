using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maggysfaith : TrinketInfo
{
    private void Start()
    {
        SetTrinketItemCode(3);
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerTearSpeed += 0.12f;
        PlayerManager.instance.playerRange += 0.12f;
    }
}
