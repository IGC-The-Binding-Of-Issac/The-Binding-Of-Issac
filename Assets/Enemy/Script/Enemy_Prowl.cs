using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Prowl : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수

    public Enemy_Prowl(TEnemy _ower)                        // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()                                // 해당 상태를 시작할 때 "1회" 호출
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Prowl 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Prowl;                 // 현재 상태를 TENEMY_STATE의 Prowl  

        e_Owner.e_moveInialize();                               // 랜덤으로 움직이기 위한 초기값 설정
        CoroutineHandler.Start_Coroutine(e_Owner.checkPosi());  // 랜덤 위치 설정 코루틴 실행
    }

    public override void Excute()                               // 해당 상태를 업데이트 할 때 "매 프레임" 호출
    {
        e_Owner.e_Prwol();                                          // prowl 

        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }

        //범위 감지 하는 애면?
        if (e_Owner.getisDetective) 
        {
            if (e_Owner.e_SearchingPlayer())                         // sight 범위 안에 들어오면
            {
                if (e_Owner.getIsTracking)                          // tracking 하는 애면
                {
                    e_Owner.ChageFSM(TENEMY_STATE.Tracking);        // tracking으로 변화
                }
            }
        }
    }

    public override void Exit()                              // 해당 상태를 종료할 때 "1회" 호출
    {        
        e_Owner.ePreState = TENEMY_STATE.Prowl;              // 전 상태를 TENEMY_STATE의 Prowl
    }
}
