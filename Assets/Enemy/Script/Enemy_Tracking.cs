using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tracking : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수
    bool isIsInvoke;

    public Enemy_Tracking(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()                            // 해당 상태를 시작할 때 "1회" 호출
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Tracking 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Tracking;          // 현재 상태를 TENEMY_STATE의 Tracking으로 

        isIsInvoke = true;
    }

    public override void Excute()                                   // 해당 상태를 업데이트 할 때 "매 프레임" 호출
    {
        e_Owner.e_findPlayer();                                     // player 탐색
        e_Owner.e_Tracking();                                       // tracking
        e_Owner.e_Lookplayer();                                     // 플레이어 look
       
        if (e_Owner.playerPosi == null)
            return;

        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }

        // 범위 감지 x
        if (!e_Owner.getisDetective) 
        {
            if ((e_Owner.getisJump))                            // 점프를 하는 애면? 
            {
                if (isIsInvoke)
                {
                    e_Owner.invokeJump();                          // 일정 시간후 점프로 넘어감
                    isIsInvoke = false;
                }
            }
        }

        // 범위 감지 하는애면?
        // -> 총 쏘는지 아닌지에 따라 달라짐
        if (e_Owner.getisDetective)                                
        {
            
            if (e_Owner.e_SearchingPlayer())                         // sight 범위 안에 들어오면
            {
                if (e_Owner.getisShoot)                              // 총 쏘는 애면?
                {
                    if (isIsInvoke) 
                    { 
                        e_Owner.invokeShoot();                          // 일정 시간후 총 쏘기로 넘어감
                        isIsInvoke = false;
                    }
                }

            }
            else if (!e_Owner.e_SearchingPlayer())                  // 범위 밖에 있으면
            {
                e_Owner.ChageFSM(TENEMY_STATE.Prowl);               // prowl로 상태 변화
            }
        }

    }

    public override void Exit()                              // 해당 상태를 종료할 때 "1회" 호출
    {
        e_Owner.ePreState = TENEMY_STATE.Tracking;              // 전 상태를 TENEMY_STATE의 Tracking
    }





}
