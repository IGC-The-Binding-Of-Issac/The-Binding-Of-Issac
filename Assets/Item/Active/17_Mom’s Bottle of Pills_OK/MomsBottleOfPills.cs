using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsBottleOfPills : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(17, 4);
        SetActiveString("엄마의 약병",
                        "충전식 알약 생성기",
                        "사용 시 랜덤한 알약 하나를 드랍한다.");
    }

    public override void UseActive()
    {
        if (canUse)
        {
            //액티브 아이템 중 5 ~ 15번까지가 알약. 
            int randomNum = Random.Range(5, 16);
            Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
            //그 중에 랜덤을 돌려서 랜덤한 알약 복제 및 생성
            GameObject pill = Instantiate(ItemManager.instance.itemTable.ActiveChange(randomNum),
                                          new Vector3(dropPosition.position.x, dropPosition.position.y - 1f, 0),
                                          Quaternion.identity) as GameObject;
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }
}
