using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;

    private void Start()
    {
        SetTrinketItemCode(7);
        SetTrinketString("¾ÈµÅ!",
                         "¸ØÃç!",
                         "½Àµæ ½Ã ÀÌµ¿¼Óµµ * 0.5" +
                         "\n°ø°Ý¼Óµµ * 0.5");
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerMoveSpeed /= 2f;
        PlayerManager.instance.playerShotDelay *= 2f;
        PlayerManager.instance.CheckedShotDelay();
    }

    public override void DropTrinket()
    {
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed * 2f;  
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay / 2f;
        PlayerManager.instance.CheckedShotDelay();
    }
}
