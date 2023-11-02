using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPill : ActiveInfo
{
    
    private void Awake()
    {
        SetActiveItem(13, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerHp -= 1;
        SetActiveString("진통제인 줄 알았는데..",
        "체력 감소",
        "사용 시 현재 체력이 감소한다.");
        Destroy(gameObject);
        }
    }
}
