using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfYellowPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(14, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerRange += 0.15f;
        SetActiveString("난 기린이야.",
        "사거리 증가",
        "사용 시 눈물의 사거리가 증가한다.");
        Destroy(gameObject);
    }
}
