using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수
    float currTIme;
    float stateTime;

    public Enemy_Jump(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        //Debug.Log("jump 상태 진입");
        e_Owner.eCurState = TENEMY_STATE.Jump;              // 현재 상태를 TENEMY_STATE의 Tracking으로 
        e_Owner.e_jumpSet();                                // 점프 전 세팅

        stateTime = Random.Range(0.1f, 1.2f);
        currTIme = e_Owner.getattaackSpeed + stateTime;   // 공격속도 가져오기

    }

    public override void Excute()
    {
        e_Owner.e_Tracking(e_Owner.JumpSpeed);

        currTIme -= Time.deltaTime;
        if (currTIme <= 0) 
        {
            e_Owner.e_doneJump();
        }

        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }
    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Jump;
    }

}
