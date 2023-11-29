using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWorm : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropRange;
    float beforeDropShotDelay;
    float beforeTearSpeed;

    public override void Start()
    {
        base.Start();
        SetTrinketItemCode(5);
        SetTrinketString("Ã¤Âï ¹ú·¹",
                         "ÈªÄ¡!",
                         "½Àµæ ½Ã »ç°Å¸® + 1.2" 
                       + "\n°ø°Ý ¼Óµµ - 0.3"
                       + "\n´«¹° ¼Óµµ + 2");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerRange += 1.2f;
        PlayerManager.instance.playerShotDelay += 0.3f;
        PlayerManager.instance.playerTearSpeed += 2;
        base.GetItem();
    }

    public override void DropTrinket()
    {
        beforeDropRange = PlayerManager.instance.playerRange;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;
        beforeTearSpeed = PlayerManager.instance.playerTearSpeed;

        PlayerManager.instance.playerRange = beforeDropRange - 1.2f; 
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay - 0.3f;
        PlayerManager.instance.playerTearSpeed = beforeTearSpeed - 2;
        PlayerManager.instance.CheckedStatus();
    }
}
