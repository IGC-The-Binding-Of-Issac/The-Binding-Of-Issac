using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수

    public Enemy_Jump(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        e_Owner.eCurState = TENEMY_STATE.Jump;              // 현재 상태를 TENEMY_STATE의 Tracking으로 

        e_Owner.e_moveInialize();                           // 내 위치 1회 받아오기
        e_Owner.e_findPlayer();                             // 플레이어 위치 1회 받아오기
        e_Owner.e_jumpSet();                                // 점프 전 세팅

        e_Owner.e_jump();                                   // 점프
    }

    public override void Excute()
    {
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
