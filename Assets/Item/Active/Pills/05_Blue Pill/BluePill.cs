using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(5, 0);
        SetActiveString("???",
            "???",
            "???");

    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMoveSpeed += 0.15f;
        SetActiveString("발에 붙 붙었어!",
            "이동 속도 증가",
            "사용 시 이동 속도가 증가한다.");
        Destroy(gameObject);
    }
}

