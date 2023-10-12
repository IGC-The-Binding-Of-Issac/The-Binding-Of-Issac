using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum TrideState 
{
    Idle,
    Tracking, // 추적
    Jump // 점프
}


public class Tride : Top_Spider
{
    // 방에 들어오면 추적,
    // 일정 시간 마다 점프 (플레이어를 향해 바로)

    // 유한상태 머신 사용 (코루틴)
    // yield return null : 해당 상태로 넘어오면 , 다른 상태로 넘어갈 때 까지 while문 진행

    [Header("Tride")]
    [SerializeField] TrideState trideState;
    float currTime;
    float jumpCoolTIme;
    float oriSpeed;
    float jumpSpeed;

    void Start()
    {
        ChageState(TrideState.Idle); //초기 상태는 가만히 있는, 
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";
        animator = GetComponent<Animator>();

        //Enemy
        hp = 1f;
        sight = 3f;
        moveSpeed = 2f;
        waitforSecond = 0.8f;
        attaackSpeed = 2f;

        //Tride
        currTime = 0;
        jumpCoolTIme = 1f;
        oriSpeed = moveSpeed;
        jumpSpeed = 7f;
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
            ChageState(TrideState.Tracking);
        }
        else if (currTime <attaackSpeed + jumpCoolTIme)
        {

            ChageState(TrideState.Jump);
        }
        else
        {
            // 타이머를 재설정하고 "Tracking" 상태로 돌아감
            animator.SetBool("isJump", false);
            currTime = 0;
            ChageState(TrideState.Tracking);
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

    IEnumerator Tracking()
    {
        while (true) 
        {
            moveSpeed = oriSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
    IEnumerator Jump()
    {
        animator.SetBool("isJump", true);
        while (true) 
        {
            moveSpeed = jumpSpeed;
            Tracking(justTrackingPlayerPosi);
            //Tride가 점프하는 코드
            yield return null;


        }
    }
}
