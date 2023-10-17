using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetItemCode(14);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        if (PlayerManager.instance.playerMaxHp < PlayerManager.instance.playerHp)
            PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        //목숨이 3개로 변한다.
        //사망 후 부활 시 스테이지를 재시작한다.

    }
}
