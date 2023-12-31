using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spider : TEnemy
{
    // prowl
    public override void En_setState()
    {
        playerInRoom     = false;
        dieParameter     = "isDie";

        hp              = 3f;
        sight           = 5f;
        moveSpeed       = 3f;
        waitforSecond   = 0.5f;
        fTime           = 0.5f;
        randRange       = 3f;

        maxhp           = hp;

        enemyNumber = 5;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = false;
        isProwl         = true;
        isDetective     = false;
        isShoot         = false;
    }

    private void Start()
    {
        // 하위 몬스터 state 설정
        En_setState();              // 스탯 설정
        En_kindOfEnemy();           // enemy의 행동 조건

        En_Start();                  // 초기세팅
    }

    private void Update()
    {
        E_Excute();                 // 상태 실행
    }

}
