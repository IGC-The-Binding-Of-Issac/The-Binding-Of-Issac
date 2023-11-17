using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수

    public Enemy_Jump(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        e_Owner.eCurState = TENEMY_STATE.Jump;          // 현재 상태를 TENEMY_STATE의 Tracking으로 
    }

    public override void Excute()
    {

    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Jump;
    }
}
