using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFly : TEnemy
{
    // tracking
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";

        hp              = 2f;
        sight           = 5f;
        moveSpeed       = 1.5f;
        waitforSecond   = 0.5f;

        maxhp           = hp;
        enemyNumber = 0;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = false;
        isDetective     = false;
        isShoot         = false;
        isJump          = false;
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