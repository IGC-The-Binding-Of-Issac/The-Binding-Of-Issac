using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaBattery : TrinketInfo

{
    // Start is called before the first frame update
    private void Start()
    {
        SetTrinketItemCode(4);
    }

    public override void GetItem()
    {
        //장착 시 배터리 충전 효율이 2배로 늘어난다. 가능하면 넣어주고 아니면 빼고
        PlayerManager.instance.playerMoveSpeed += 0.1f;
    }
}
