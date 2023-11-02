using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maggysfaith : TrinketInfo
{
    float beforeTearSpeed;
    float beforeRange;

    private void Start()
    {
        SetTrinketItemCode(3);
        SetTrinketString("∏≈±‚¿« πœ¿Ω",
            "πœ¿Ω¿« ¥Î∞°",
            "Ω¿µÊ Ω√ ¥´π∞ º”µµ + 0.12" +
            "\n ªÁ∞≈∏Æ + 0.12");
    }
    public override void GetItem()
    {
        beforeTearSpeed = PlayerManager.instance.playerTearSpeed;
        beforeRange = PlayerManager.instance.playerRange;
        PlayerManager.instance.playerTearSpeed += 0.12f;
        PlayerManager.instance.playerRange += 0.12f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerTearSpeed = beforeTearSpeed;
        PlayerManager.instance.playerRange = beforeRange;
    }
}
