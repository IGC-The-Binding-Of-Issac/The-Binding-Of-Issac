using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : TEnemy_FSM<TEnemy>
{
    /// <summary>
    /// 1. Enemy의 idle 상태 구현
    /// 2. "Enemy_Idle" 스크립트를 실행 시킬 스크립트 변수 (TENemy이 주인)
    /// 
    /// * Enemy를 상속 받는 Attackly 플라이에서 Idle을 실행했다면?
    ///     - m_Owner는 Attackly
    ///     - 상속 받은 Attackly도 Enemy 타입임!! (Enemy를 상속 받았기 떄문)
    /// </summary>
    [SerializeField] TEnemy e_Owner;                        // 주인 변수 

    /// <summary>
    /// 1. 생성자 
    /// 2. 주인 스크립트에서 this를 넘겨줌 
    /// 3. Enemy 자식 스크립트에서 init을 하면 this 가 자식 오브젝트니까 그걸 넘기겠지 ?!
    /// </summary>

    public Enemy_Idle(TEnemy _ower)                          // 생성자 초기화
    {
        e_Owner = _ower;  
    }
                                    
    public override void Enter()                            // 해당 상태를 시작할 때 "1회" 호출
    {
        //Debug.Log(e_Owner.gameObject.tag + " : idle 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Idle;              // 현재 상태를 TENEMY_STATE의 idle로
    }


    public override void Excute()                               // 해당 상태를 업데이트 할 때 "매 프레임" 호출
    {
        if (e_Owner.playerInRoom)                               // 플레이어가 방 안에 있으면 상태변환
        {
            if (e_Owner.getIsTracking)                          // tracking을 하는
            {
                e_Owner.ChageFSM(TENEMY_STATE.Tracking);        // tracking 으로 상태 변환
            }
            
            if (!e_Owner.getIsTracking && e_Owner.getisProwl)   // prowl 만 하면?
            {
                e_Owner.ChageFSM(TENEMY_STATE.Prowl);           // prowl 으로 상태 변환
            }
            
        }

    }

    public override void Exit()                             // 해당 상태를 종료할 때 "1회" 호출
    {
        e_Owner.ePreState = TENEMY_STATE.Idle;              // 전 상태를 TENEMY_STATE의 idle로
    }

    
}


