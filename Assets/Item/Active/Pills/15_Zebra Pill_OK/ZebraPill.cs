using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(15, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerMoveSpeed -= 0.15f;
            PlayerManager.instance.playerShotDelay -= 0.08f;
            PlayerManager.instance.CheckedShotDelay();
            SetActiveString("�������� �����ٰ�.",
                            "�̵��ӵ� ����, ���ݼӵ� ����",
                            "��� �� �̵��ӵ��� ����������, ���ݼӵ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}