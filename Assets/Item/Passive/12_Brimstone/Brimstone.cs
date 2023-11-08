using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemInfo
{

    private void Start()
    {
        SetItemCode(12);
        SetItemString("유황",
            "혈사포",
            "습득시 공격속도 / 3배");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.33f;
        PlayerManager.instance.CheckedShotDelay();
        //눈물 및 캐릭터 외모 변경
        //공격이 차징으로 바뀜
        //키를 3초이상 누르면 조건을 만족하고 키를 뗏을 때 입에서 해당 방향으로 발사
        //키를 누르는 시점에 게이지 바 생성
    }
}
