using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

enum MawState
{
    // Idle
    MawProwl,
    // 총 쏘는
    MawShoot,
    //추적
    MawTracking
}

public class Maw : TEnemy
{
    // 플레이어 추적
    public override void En_setState()
    {
        playerInRoom = false;

        hp = 2f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        maxhp = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking = true;
        isProwl = false;
        isDetective = false;
    }

    private void Start()
    {
        // 하위 몬스터 state 설정
        En_setState();              // 스탯 설정
        En_kindOfEnemy();           // enemy의 행동 조건
        En_stateArray();            // state 를 배열에 세팅

        E_Enter();                  // 상태 진입 (기본은 idle로 설정 되어잇음)
    }

    private void Update()
    {
        E_Excute();                 // 상태 실행
    }
}
