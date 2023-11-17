using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;


    //생성자 초기화
    public Enemy_Shoot(TEnemy _ower)
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        Debug.Log(e_Owner.gameObject.tag + " : Shoot 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Shoot;          // 현재 상태를 TENEMY_STATE의 Tracking으로 

        e_Owner.setIsReadyShoot = true;
    }

    public override void Excute()
    {
        e_Owner.e_Shoot();                                          // 총알 쏘기

        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }

        if (e_Owner.e_SearchingPlayer())                            // 총쏜 후 -> 플레이어가 범위 안에 있으면
        {
            e_Owner.ChageFSM(TENEMY_STATE.Tracking);                // tracking
        }
        else if (!e_Owner.e_SearchingPlayer())                      // 총쏜 후 -> 플레이어가 범위 밖
        { 
            e_Owner.ChageFSM(TENEMY_STATE.Prowl);                   // prowl
        }
    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Shoot;
    }


}
