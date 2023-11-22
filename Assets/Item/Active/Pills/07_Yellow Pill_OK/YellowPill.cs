using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPill : ActiveInfo
{
    public override void Start()
    {
        base.Start();
        SetActiveItem(7, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerDamage += 0.15f;
            SetActiveString("힘에는 힘!",
                            "공격력 증가",
                            "사용 시 공격력이 증가한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            base.UseActive();
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);

        }
    }
}
