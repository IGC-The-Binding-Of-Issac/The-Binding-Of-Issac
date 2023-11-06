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
        int randomNum = Random.Range(5, 16);
        //GameObject pill = Instantiate(ItemManager.instance.itemTable.GetComponent<ItemTable>().ActiveItems) 
    }
}
