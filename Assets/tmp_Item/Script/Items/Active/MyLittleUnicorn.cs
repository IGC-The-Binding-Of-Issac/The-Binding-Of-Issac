using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLittleUnicorn : ItemInfo
{
    MyLittleUnicorn()
    {
        itemCode = 32;
        itemName = "My Little Unicorn";
        item_MoveSpeed = 0.28f;
    }
    //사용 시 6초간 moveSpeed +0.28f;
    //무적이 되며, 접촉하는 적에게 초당 20의 피해를 입힌다.
}
