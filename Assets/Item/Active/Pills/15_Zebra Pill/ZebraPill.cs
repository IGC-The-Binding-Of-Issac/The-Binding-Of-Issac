using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(15, 0);
        SetActiveString("???",
        "???",
        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerMoveSpeed -= 0.15f;
        PlayerManager.instance.playerShotDelay -= 0.08f;
        PlayerManager.instance.CheckedShotDelay();
        SetActiveString("느리지만 빠르다고.",
        "이동속도 감소, 공격속도 증가",
        "사용 시 이동속도가 감소하지만, 공격속도가 증가한다.");
        Destroy(gameObject);
        }
    }
}
