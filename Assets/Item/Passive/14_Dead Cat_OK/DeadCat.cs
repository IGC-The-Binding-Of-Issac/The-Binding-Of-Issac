using System;
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
        //int[] liveOrDeath = { 0, 50 };
        //int randomNum = UnityEngine.Random.Range(0, 1);
        PlayerManager.instance.playerMaxHp = 2;
        if (PlayerManager.instance.playerMaxHp < PlayerManager.instance.playerHp)
            PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        //if (liveOrDeath[randomNum] == 0)
        //{
        //    if (PlayerManager.instance.playerHp == 0)
        //    {
        //        GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
        //    }
        //}
        //else if (liveOrDeath[randomNum] == 50)
        //{
            if (PlayerManager.instance.playerHp == 0)
            {
                PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
            }
        //}

        //목숨이 3개로 변한다.
        //사망 후 부활 시 스테이지를 재시작한다.

    }
}
