using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(5, 0);
        SetActiveString("???",
            "???",
            "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
        PlayerManager.instance.playerMoveSpeed += 0.15f;
        SetActiveString("발에 붙 붙었어!",
            "이동 속도 증가",
            "사용 시 이동 속도가 증가한다.");
        UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
        Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}

