using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(11, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerHp += 1;
        PlayerManager.instance.playerMaxHp += 1;
        SetActiveString("단단해지기",
        "체력 증가",
        "사용 시 최대 체력 및 체력이 증가한다.");
        Destroy(gameObject);
        }
    }
}
