using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhitePill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(8, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerRange -= 0.15f;
        SetActiveString("작은 고추가 맵다",
        "사거리 감소",
        "사용 시 눈물의 사거리가 감소한다.");
        Destroy(gameObject);
    }
}
