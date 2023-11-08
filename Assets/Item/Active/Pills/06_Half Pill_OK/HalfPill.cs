using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(6, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerShotDelay -= 0.05f;
            PlayerManager.instance.CheckedShotDelay();
            SetActiveString("빨리빨리",
                            "공격 속도 증가",
                            "사용 시 공격 속도가 증가한다.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
