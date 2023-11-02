using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(16, 0);
        SetActiveString("???",
        "???",
        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerHp -= 1;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        PlayerManager.instance.playerDamage += 0.15f;
        SetActiveString("근육통",
        "체력 감소, 이동속도와 공격력 증가",
        "사용 시 현재 체력이 감소하지만, 이동속도와 공격력이 증가한다.");
        Destroy(gameObject);
        }
    }
}
