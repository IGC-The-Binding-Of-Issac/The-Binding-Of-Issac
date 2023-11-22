using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWafer : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(8);
        SetItemString("밀빵",
                      "탄수화물 덩어리",
                      "습득 시 이동속도 - 1.3"
                    + "\n공격력 + 2"
                    + "\n사거리 - 2");
    }
    public override void UseItem()
    {
        Transform parentSize = GameManager.instance.playerObject.transform;
        Transform bodySize = GameManager.instance.playerObject.transform.GetChild(1).GetComponent<Transform>();
        PlayerManager.instance.playerMoveSpeed -= 1.3f;
        PlayerManager.instance.playerDamage += 2f;
        PlayerManager.instance.playerRange -= 2f;
        base.UseItem();
        bodySize.localScale = new Vector3(parentSize.localScale.x * 1.2f, parentSize.localScale.y, 0);
    }
}
