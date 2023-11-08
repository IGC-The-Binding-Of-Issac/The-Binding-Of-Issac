using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePill : ActiveInfo
{
    private void Awake()
    {
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
            SetActiveString("��� �͸��� ��������",
                            "���� �ӵ� ����",
                            "��� �� ������ ���ư��� �ӵ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}