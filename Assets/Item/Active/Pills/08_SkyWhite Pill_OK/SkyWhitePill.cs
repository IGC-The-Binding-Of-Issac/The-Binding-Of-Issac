using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhitePill : ActiveInfo
{
    public void Awake()
    {
        base.Start();
        SetActiveItem(8, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerRange -= 0.15f;
            SetActiveString("작은 고추가 맵다",
                            "사거리 감소",
                            "사용 시 눈물의 사거리가 감소한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            base.UseActive();
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
