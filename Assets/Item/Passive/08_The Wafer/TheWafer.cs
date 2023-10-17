using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWafer : ItemInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetItemCode(8);
    }
    public override void UseItem()
    {
        //모든 피격 데미지가 하트 반칸으로 고정된다.
    }
}
