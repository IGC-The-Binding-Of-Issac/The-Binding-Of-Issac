using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFingers : ActiveInfo
{
    
    public void Awake()
    {
        base.Start();
        SetActiveItem(1, 0);
        SetActiveString("유료 안마기",
                        "1원을 넣어주세요",
                        "사용 시 1원을 소모하여 공격력 + 0.13");

        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(ItemManager.instance.coinCount > 0 && canUse)
          {
            ItemManager.instance.coinCount--;
            PlayerManager.instance.playerDamage += 0.13f;
          }
        base.UseActive();
        canUse = false;
        Invoke("SetCanUse", 1f);

        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
        Invoke("SetCanChangeItem", 1f);

    }
}
