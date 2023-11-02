using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(6, 0);
        SetActiveString("???",
    "???",
    "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerShotDelay -= 0.15f;
        SetActiveString("빨리빨리",
        "공격 속도 증가",
        "사용 시 공격 속도가 증가한다.");
        Destroy(gameObject);
    }
}
