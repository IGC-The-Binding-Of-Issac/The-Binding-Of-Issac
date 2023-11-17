using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : TEnemy
{
    // 플레이어 추적
    public override void En_setState()
    {
        playerInRoom         = false;

        hp                   = 2f;
        sight                = 2f;
        moveSpeed            = 1.5f;
        waitforSecond        = 0.5f;
        fTime                = 0.5f;
        randRange            = 1f;

        maxhp                = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking          = true;
        isProwl             = true;
        isDetective         = true;
        isShoot             = true;
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
