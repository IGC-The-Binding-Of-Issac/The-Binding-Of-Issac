using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tracking : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수

    public Enemy_Tracking(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()                            // 해당 상태를 시작할 때 "1회" 호출
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Tracking 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Tracking;          // 현재 상태를 TENEMY_STATE의 Tracking으로 
    }

    public override void Excute()                                   // 해당 상태를 업데이트 할 때 "매 프레임" 호출
    {
        e_Owner.e_findPlayer();                                     // player 탐색

        if (e_Owner.trackingTarget == null)
            return;

        if (e_Owner.getIsTracking)                                  // Tracking 하는 enemy  이면
        {
            e_Owner.e_Tracking();                                   // tracking
        }

        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }

        /*
        if (e_Owner.getisProwl && !e_Owner.e_SearchingPlayer())      // prowl을 하는 enemy + 플레이어가 범위 안에 없을 때 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Prowl);                    // prowl로 상태 변화
        }
        if (e_Owner.getisProwl && e_Owner.e_SearchingPlayer())       // prowl을 하는 enemy + 플레이어가 범위 안에 있으면
        {
            e_Owner.e_Tracking();                                    // tracking
        } 
        */
    }

    public override void Exit()                              // 해당 상태를 종료할 때 "1회" 호출
    {
        e_Owner.ePreState = TENEMY_STATE.Tracking;              // 전 상태를 TENEMY_STATE의 Tracking
    }


}
