using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTM : ItemInfo
{
    private void Start()
    {
        SetItemCode(1);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage++;
        //먹으면 캐릭터 위에 월계관 달림, 눈물 색깔 빨간색으로 변경
        //노션에 월계관 달린 캐릭터 에셋 있음.
    }
}
