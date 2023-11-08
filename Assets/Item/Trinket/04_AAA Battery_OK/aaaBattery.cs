using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaBattery : TrinketInfo

{
    float beforeDropMoveSpeed;
    private void Start()
    {
        SetTrinketItemCode(4);
        SetTrinketString("AAA 건전지",
                         "충전 효율 증가",
                         "습득 시 방 클리어 시 액티브 아이템 충전 효율이 2배가 되며" +
                         "이동속도 + 0.1");
    }

    public override void GetItem()
    {
        //장착 시 배터리 충전 효율이 2배로 늘어난다. 가능하면 넣어주고 아니면 빼고
        PlayerManager.instance.playerMoveSpeed += 0.1f;
    }

    public override void DropTrinket()
    {
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed - 0.1f;
    }
}
