using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyTail : ItemInfo
{
    private void Start()
    {
        SetItemCode(15);
        SetItemString("구피의 꼬리",
            "잘린 구피 꼬리",
            "");
    }
    public override void UseItem()
    {
     //습득 후 캐릭터가 사망할 때마다 50% 확률로 스테이지 재시작
    }
}
