using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(9, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerTearSpeed -= 0.15f;
        SetActiveString("우는 것마저 느리구나",
        "눈물 속도 감소",
        "사용 시 눈물이 날아가는 속도가 감소한다.");
        Destroy(gameObject);
    }
}
