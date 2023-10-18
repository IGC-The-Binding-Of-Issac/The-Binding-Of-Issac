using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum TrideState
{
    TrideIdle,
    TrideTracking, // 추적
    TrideJump // 점프
}


public class Tride : Top_Spider
{
    // 방에 들어오면 추적,
    // 일정 시간 마다 점프 (플레이어를 향해 바로)

    // 유한상태 머신 사용 (코루틴)
    // yield return null : 해당 상태로 넘어오면 , 다른 상태로 넘어갈 때 까지 while문 진행

    [Header("Tride")]
    [SerializeField] TrideState trideState;
    [SerializeField] Vector3 jumpDesti; //점프 할 목표 위치
    [SerializeField] Vector3 tridePosi; //점프 할 목표 위치
    float currTime;
    float oriSpeed;
    float jumpSpeed;

    [SerializeField] float jumpAiPlayTime;

    bool trackingStarted;
    bool jumpStarted;

    void Start()
    {
        trideState = TrideState.TrideIdle;

        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";
        animator = GetComponent<Animator>();

        //Enemy
        hp = 3f;
        sight = 3f;
        moveSpeed = 2f;
        waitforSecond = 0.4f;
        attaackSpeed = 2f;

        //Tride
        currTime = 0;
        oriSpeed = moveSpeed;
        jumpSpeed = 4f;

        trackingStarted = false; //추적 코루틴 실행
        jumpStarted = false; //점프 코루틴 실행
    }

    private void Update()
    {
        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom)
        {
            Move();
            Lookplayer(justTrackingPlayerPosi);
        }
    }

    public override void Move()
    {
        //상태 변화
        currTime += Time.deltaTime;
        if (currTime < attaackSpeed)
        {
            if (!trackingStarted)
            {
                // state변환을 단 한번 실행
                ChageState(TrideState.TrideTracking);

                trackingStarted = true;
                jumpStarted = false;

                GetJumpTime();
                // tracnking 상태가 끝나기 직전에 / 점프 할 시간 (jumpAiPlayTime)구함
            }
        }
        else if (currTime < attaackSpeed + (jumpAiPlayTime * 10)) // jumpAiPlayTime 만큼 실행
        {
            if (!jumpStarted)
            {
                // state변환을 단 한번 실행
                ChageState(TrideState.TrideJump);
                jumpStarted = true;
            }

        }
        else
        {

            // 타이머를 재설정하고 "Tracking" 상태로 돌아감
            //Debug.Log("상태를 바꿔요~");
            trackingStarted = false;
            jumpStarted = true;

            currTime = 0;
            ChageState(TrideState.TrideTracking);
        }
    }


    // 플레이어의 행동을 newState로 변환
    void ChageState(TrideState newState)
    {
        // 이전 상태 종료
        StopCoroutine(trideState.ToString());
        // 새로운 상태로 변경
        trideState = newState;
        // 현재 상태의 코루틴 실행
        StartCoroutine(trideState.ToString());
    }


    IEnumerator Idle()
    {
        //상태 초기화
        yield return null;
    }

    IEnumerator TrideTracking()
    {
        // z축을 0 으로
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        while (true)
        {
            moveSpeed = oriSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }

    IEnumerator TrideJump()
    {
        // 구한 jumpAiPlayTime만큼 jump 애니메이션 실행
        animator.SetFloat("isJump", jumpAiPlayTime);
        // z축으로 1올리기
        transform.position = new Vector3(transform.position.x, transform.position.y, 1 );

        while (true)
        {
            moveSpeed = jumpSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;

        }
    }

    //jump애니메이션을 실행 할 시간 구하기
    void GetJumpTime()
    {
        // 점프 위치 받아옴
        jumpDesti = new Vector3(justTrackingPlayerPosi.position.x, justTrackingPlayerPosi.position.y, 0);
        // tride위치 1회 받아옴
        tridePosi = new Vector3(transform.position.x, transform.position.y, 0);

        //거리, 속력(jumpSpeed), 시간 공식 사용, 시간 구하기
        float plyerTotride = Vector3.Distance(jumpDesti, tridePosi);
        jumpAiPlayTime = plyerTotride / jumpSpeed; // (점프)시간 = 플레이어와 tride거리 / 점프 속도
    }

}
