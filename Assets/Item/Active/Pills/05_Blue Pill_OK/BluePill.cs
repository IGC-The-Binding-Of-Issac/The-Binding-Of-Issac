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
            SetActiveString("�߿� �� �پ���!",
                            "�̵� �ӵ� ����",
                            "��� �� �̵� �ӵ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
