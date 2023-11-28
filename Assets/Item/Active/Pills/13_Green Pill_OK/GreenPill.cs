using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPill : ActiveInfo
{
    public void Awake()
    {
        base.Start();
        SetActiveItem(13, 0);
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
            PlayerManager.instance.CheckedStatus();
            UIManager.instance.SetPlayerCurrentHP();
            SetActiveString("진통제인 줄 알았는데..",
                            "체력 감소",
                            "사용 시 현재 체력이 감소한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
