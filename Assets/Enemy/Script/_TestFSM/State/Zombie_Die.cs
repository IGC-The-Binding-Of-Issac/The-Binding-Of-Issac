using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Die : FSM<Zombie_FSM>
{
    // 좀비의 Attack 상태 구현
    // 이 스크립트를 실행 시킬 스크립트 변수 (Zombie_FSM스크립트, 즉 주인)
    [SerializeField] Zombie_FSM m_Owner;

    //생성자 초기화
    public Zombie_Die(Zombie_FSM _ower)
    {
        m_Owner = _ower;
    }

    public override void Begin()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Run()
    {
        throw new System.NotImplementedException();
    }
}
