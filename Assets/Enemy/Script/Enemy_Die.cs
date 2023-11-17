using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;

    public Enemy_Die(TEnemy _ower)
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        Debug.Log(e_Owner.gameObject.tag + " : DIE 상태 ");
        e_Owner.eCurState = TENEMY_STATE.Die;          // 현재 상태를 TENEMY_STATE의 Die 
        e_Owner.e_destroyEnemy();
    }

    public override void Excute()
    {

    }

    public override void Exit()
    {

    }


}
