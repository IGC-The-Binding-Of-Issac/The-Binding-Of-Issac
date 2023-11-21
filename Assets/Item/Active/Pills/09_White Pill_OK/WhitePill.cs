using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePill : ActiveInfo
{
    public override void Start()
    {
        base.Start();
        SetActiveItem(9, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerTearSpeed -= 0.15f;
            SetActiveString("우는 것마저 느리구나",
                            "눈물 속도 감소",
                            "사용 시 눈물이 날아가는 속도가 감소한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
