using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFSM 
{
    public class Head_Machine<T>
    {
        // 오브젝트
        private T Owner;

        // 상태 값
        private FSM<T> m_CurState = null; // 현재 상태
        private FSM<T> m_PrevState = null; // 이전 상태

        // 처음 상태값
        public void Begin() // Head_Machine의 메서드??
        {
            if (m_CurState != null) //처음 상태가 null 이 아니면? 
            {
                m_CurState.Begin();
            }
        }

        // 상태 업데이트
        public void Run() 
        {
            CheckState();
        }

        // 상태 체크
        public void CheckState() 
        {
            if (m_CurState != null) 
            {
                m_CurState.Run();    // Run 상태로 바꾸는?
            }
        }

        // fsm 종료
        public void Exit() 
        {
            m_CurState.Exit(); // 원래는 m_CurState.Exit()인데 오류남!!
            m_CurState = null;
            m_PrevState = null;
        
        
        }

        public void Change(FSM<T> _state)
        {
            //같은 상태이면 리턴
            if (_state == m_CurState)
                return;

            m_PrevState = m_CurState; // 이전 상태를 현재 상태로 ??

            // 현재 상태가 있다면 종료
            if (m_CurState != null)
                m_CurState.Exit();

            m_CurState = _state;
            // 새로 적용된 상태가 null이 아니면 실행
            if (m_CurState != null)
                m_CurState.Begin();
        }

        //변화할 때 아무런 인자값이 없으면 전에 상태값 출력
        public void Revert() 
        {
            if (m_PrevState != null)
                Change(m_CurState);
        }
        
        // 상태값 세팅
        public void SetState(FSM<T> _state, T _Owner)
        {
            Owner = _Owner;
            m_CurState = _state;

            // 들어온 상태값이 지금과 다를때 && 현재상태값이 채워져 있을 때
            if (m_CurState != _state && m_CurState != null)
                m_PrevState = m_CurState;
        }

    }

}
