using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPill : ActiveInfo
{
    public void Awake()
    {
        base.Start();
        SetActiveItem(12, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerDamage += 0.08f;
            PlayerManager.instance.playerMoveSpeed += 0.08f;
            SetActiveString("전장으로!",
                            "공격력 증가, 이동속도 증가",
                            "사용 시 공격력과 이동속도가 증가한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            base.UseActive();
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
