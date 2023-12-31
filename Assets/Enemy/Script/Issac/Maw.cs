using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Maw : TEnemy
{
    // tracking, pworl, shoot, 
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";
        shootParameter  = "isShoot";

        hp              = 4f;
        sight           = 2f;
        moveSpeed       = 1.5f;
        attaackSpeed    = 1f;
        waitforSecond   = 0.3f;
        fTime           = 0.5f;
        randRange       = 1f;

        maxhp           = hp;

        enemyNumber = 3;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = true;
        isDetective     = true;
        isShoot         = true;
    }

    private void Start()
    {
        // 하위 몬스터 state 설정
        En_setState();               // 스탯 설정
        En_kindOfEnemy();            // enemy의 행동 조건

        En_Start();                  // 초기세팅
    }

    private void Update()
    {
        E_Excute();                 // 상태 실행
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);
    }

}
