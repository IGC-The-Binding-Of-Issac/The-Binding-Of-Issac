using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWorm : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropRange;
    float beforeDropShotDelay;

    private void Start()
    {
        SetTrinketItemCode(5);
        SetTrinketString("Ã¤Âï ¹ú·¹",
                         "ÈªÄ¡!",
                         "½Àµæ ½Ã »ç°Å¸® + 1.2" +
                         "\n°ø°Ý ¼Óµµ - 0.3");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerRange += 1.2f;
        PlayerManager.instance.playerShotDelay += 0.3f;
        PlayerManager.instance.CheckedShotDelay();
    }

    public override void DropTrinket()
    {
        beforeDropRange = PlayerManager.instance.playerRange;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerRange = beforeDropRange - 1.2f; 
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay - 0.3f;
        PlayerManager.instance.CheckedShotDelay();
    }
}
