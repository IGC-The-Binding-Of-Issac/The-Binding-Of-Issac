using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    private void Start()
    {
        SetItemCode(6);
    }

    private void SetHitDelay()
    {
        PlayerManager.instance.CanGetDamage = true;
    }
    public override void UseItem()
    {
        //몸 주위에 방어막이 생긴다. 피격 시 방어막 사라지고 1초간 무적.
        //방 이동 시 방어막 재생성
    }
}
