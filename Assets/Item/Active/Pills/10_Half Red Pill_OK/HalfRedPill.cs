using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRedPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(10, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerDamage -= 0.15f;
            PlayerManager.instance.CheckedDamage();
            SetActiveString("나는 멸치야..",
                            "공격력 감소",
                            "사용 시 공격력이 감소한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
