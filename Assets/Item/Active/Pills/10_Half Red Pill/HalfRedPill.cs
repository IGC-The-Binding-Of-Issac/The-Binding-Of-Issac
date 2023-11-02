using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRedPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(10, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage -= 0.15f;
        SetActiveString("나는 멸치야..",
        "공격력 감소",
        "사용 시 공격력이 감소한다.");
        Destroy(gameObject);
    }
}
