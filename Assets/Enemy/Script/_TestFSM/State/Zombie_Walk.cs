using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM 
{
    public class Zombie_Walk : FSM<Zombie_FSM>
    {
        // 좀비의 walk 상태 구현
        // 이 스크립트를 실행 시킬 스크립트 변수 (Zombie_FSM스크립트, 즉 주인)
        [SerializeField] Zombie_FSM m_Owner;
        Animator m_Animator;

        //생성자 초기화
        public Zombie_Walk(Zombie_FSM _ower)
        {
            m_Owner = _ower;
        }

        public override void Begin()
        {
            Debug.Log("Zombie_Walk : 걷기 시작");
            // 현재 상태를 walk 상태로
            m_Owner.m_eCurState = ZOMBIE_STATE.Walk;

            m_Animator = m_Owner.m_Animator; // 부모의 애니메이터 
            // m_Animator.SetBool("Walk", true);
        }

        public override void Run()
        {
            Debug.Log("Zombie_Walk : 걷기 진행");

            // 추적
            if(m_Owner.m_TransTarget != null) 
            {
                Tracnking();
            }
            // 일정 범위 안에 들어오면 
            if (m_Owner.m_TransTarget != null && m_Owner.m_fAttackRange >= 10f) 
            {
                // ZOMBIE_STATE를 Attack 상태로 변환
                m_Owner.ChageFSM(ZOMBIE_STATE.Attack);
            }
            
        }

        public override void Exit()
        {
            Debug.Log("Zombie_Walk : 걷기 끝");
            // 전 상태를 walk 상태로
            m_Owner.m_ePrevState = ZOMBIE_STATE.Walk;

            // m_Animator.SetBool("Walk", false); // 애니메이션 중지
        }

        public void Tracnking()
        {
            // walk 상태 일 때 추적하는 코드 넣기
        }
    }
}

