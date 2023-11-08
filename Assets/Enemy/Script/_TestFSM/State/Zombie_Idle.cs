using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM 
{
    public class Zombie_Idle : FSM<Zombie_FSM> // <현재 스크립트를 실행 할 스크립트 이름>
    {
        // 좀비의 Idle 상태 구현
        // 이 스크립트를 실행 시킬 스크립트 변수 (Zombie_FSM스크립트, 즉 주인)
        [SerializeField] Zombie_FSM m_Owner;

        //생성자 초기화
        public Zombie_Idle(Zombie_FSM _ower)
        {
            m_Owner = _ower;
        }

        // 상태 begin
        public override void Begin()
        {
            Debug.Log("Zombie_Idle : 기본 시작");

            // Zombie_FSM스크립트의 enum형 ZOMBIE_STATE을 idle로 바꿈
            // 현재상태(Cur)를 바꿈
            m_Owner.m_eCurState = ZOMBIE_STATE.Idle;
        }

        // 상태 동작
        public override void Run()
        {
            Debug.Log("Zombie_Idle : 기본 진행");

            //m_Owner.m_TransTarget = null; // 부모에 선언한 m_TransTarget을 가져올 수 잇음
            if (FindRange()) 
            {
                m_Owner.ChageFSM(ZOMBIE_STATE.Walk);
                // 조건에 만족하면 상태 변경            
            }

            // idle 이 실행이 되면 = idle 동작을 하면
            // ZOMBIE_STATE의  walk로 Fsm 을 전환한다

        }

        // 상태 끝
        public override void Exit()
        {
            Debug.Log("Zombie_Idle : 기본 끝");

            // Zombie_FSM스크립트의 enum형 ZOMBIE_STATE을 idle로 바꿈
            // 현재상태(Pre)를 바꿈
            m_Owner.m_ePrevState = ZOMBIE_STATE.Idle;

        }

        // 찾는 함수
        private bool FindRange() 
        {
            // 어떤 코드를 넣든지
            // m_Owner.m_TransTarget 즉 부모의 Target을 찾으면 될듯

            return true;
        }
    }

}
