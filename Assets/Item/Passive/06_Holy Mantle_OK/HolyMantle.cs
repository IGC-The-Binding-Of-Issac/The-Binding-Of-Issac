using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(6);
        SetItemString("신성한 망토",
                      "성스러운 방패",
                      "3회 방어막을 부여한다." 
                    + "\nGuardian Angel 보유 시" 
                    + "\n방 클리어 시 1회 방어막을 부여한다.");
    }
    public override void UseItem()
    {
        PlayerManager.instance.CanBlockDamage += 3;
        UIManager.instance.guardCount.transform.parent.gameObject.SetActive(true);
    }
}
