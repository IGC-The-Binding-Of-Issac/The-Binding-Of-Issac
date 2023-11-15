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

    public override void Enter()                            // 해당 상태를 시작할 때 "1회" 호출
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Prowl 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Prowl;          // 현재 상태를 TENEMY_STATE의 Prowl  
    }

    public override void Excute()                           // 해당 상태를 업데이트 할 때 "매 프레임" 호출
    {

    }

    public override void Exit()                              // 해당 상태를 종료할 때 "1회" 호출
    {        
        e_Owner.ePreState = TENEMY_STATE.Prowl;              // 전 상태를 TENEMY_STATE의 Prowl
    }
}
