using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

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

    [SerializeField] TrideState trideState;
    float currTime;
    float trideJumpTime;
    float oriSpeed;
    float jumpSpeed;

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 5f;
        sight = 4f;
        moveSpeed = 2f;
        waitforSecond = 0.8f;
        attaackSpeed = 3f; // stay <-> move

        //Spider
        currTime = 0;
        trideJumpTime = 1.2f + attaackSpeed;
        oriSpeed = moveSpeed;
        jumpSpeed = moveSpeed * 2;
    }
    private void Update()
    {
        if (playerInRoom)
        {
            justTrackingPlayerPosi = GameObject.FindGameObjectWithTag("Player").transform;
            Move();
        }

    }

    public override void Move()
    {
        //상태 변화
        currTime += Time.deltaTime;
        if (currTime < attaackSpeed)
        {
            ChageState(TrideState.TrideTracking);
        }
        else if (currTime < trideJumpTime)
        {
            ChageState(TrideState.TrideJump);
        }
        else
        {
            // 타이머를 재설정하고 "Tracking" 상태로 돌아감
            animator.SetBool("isJump", false);
            moveSpeed = oriSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            currTime = 0;
            ChageState(TrideState.TrideTracking);
        }
    }

    void ChageState(TrideState newState)
    {
        // 이전 상태 종료
        StopCoroutine(trideState.ToString());
        // 새로운 상태로 변경
        trideState = newState;
        // 현재 상태의 코루틴 실행
        StartCoroutine(trideState.ToString());
    }

    //상태 
    IEnumerator TrideTracking()
    {
        while (true)
        {
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
    IEnumerator TrideJump()
    {
        moveSpeed = jumpSpeed;
        animator.SetBool("isJump", true);
        transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
        while (true)
        {
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
}
