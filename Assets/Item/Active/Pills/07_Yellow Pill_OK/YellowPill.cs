using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPill : ActiveInfo
{
    private void Awake()
    {
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
            SetActiveString("������ ��!",
                            "���ݷ� ����",
                            "��� �� ���ݷ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);

        }
    }
}