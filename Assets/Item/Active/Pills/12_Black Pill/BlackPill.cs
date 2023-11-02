using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(12, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 0.08f;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        SetActiveString("전장으로!",
        "공격력 증가, 이동속도 증가",
        "사용 시 공격력과 이동속도가 증가한다.");
        Destroy(gameObject);
    }
}
