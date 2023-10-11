using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] TrideState trideState;

    void Start()
    {
        ChageState(TrideState.Idle); //초기 상태는 가만히 있는, 
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 20f;
        sight = 3f;
        moveSpeed = 10f;
        waitforSecond = 0.5f;
    }

    private void Update()
    {
        if (playerInRoom) 
        {
            Move();
        }
    }

    public override void Move()
    {
       // state변화 시키는 스크립트 작성

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
        //상태로 진입 할 때 1회 출력
        Debug.Log("Tride : " + trideState);

        while (true) 
        {
            Debug.Log("Tride가 플레이어를 추적 해야함");
            yield return null;
        }
    }
    IEnumerator Jump()
    {
        //상태로 진입 할 때 1회 출력
        Debug.Log("Tride : " + trideState);

        while (true) 
        {
            Debug.Log("Tride가 점프를 해야함 ");
            yield return null;
        }
    }
}
