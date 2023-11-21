using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Playables;



public class Tride : TEnemy
{
    // prowl
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";
        jumpParameter   = "isJump";

        hp              = 2f;
        sight           = 5f;
        moveSpeed       = 1f;
        waitforSecond   = 0.5f;
        attaackSpeed    = 0.5f;
        jumpSpeed       = 2.5f;

        maxhp           = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = false;
        isDetective     = false;
        isShoot         = false;
        isJump          = true;
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


    public void LandingSound()
    {
        audioSource.Play();
    }
}
