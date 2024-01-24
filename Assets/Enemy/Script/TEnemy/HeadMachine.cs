using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMachine<T>
{
    /// <summary>
    /// 1. FSM을 상속 받은 함수들을 관리하기 위해서
    /// 2. 상태 변환을 하는 메서드를 모아둔 느낌!
    /// 3. 관리를 Enemy에서 해도 되는데 보기 편리 하기 위해서 일부러 따로 빼놓은듯?
    /// </summary>

    // T 타입 변수 (T가 Enemy 타입 이면 , Enemy 타입의 변수 생성)
    private T Owner;

    // 상태 
    // 지금 돌아가고 있는 Enemy 상태 (Idle , Tracking , Prowl , Die 스크립트(상태)) 
    private FSM<TEnemy> currState = null; // 현재 상태
    private FSM<TEnemy> prestate = null; // 과거 상태

    // FSM의 Enter실행 , 처음 상태 시작
    public void H_Enter() // Enemy_HeadMachine에서 돌아가는 메서드
    {
        if (currState != null) // 돌아가는 상태가 없으면
        {
            currState.Enter();  // 해당 상태 (Enter)진입
                                // 해당 스크립트 (클래스)안의 Enter 실행
                                // FSM을 상속받고, 추상화 클래스를 구현해놓은 그 메서드
        }
    }

    // FSM의 Excute 실행 , 상태 업테이트
    public void H_Excute()
    {
        CheckState();
    }

    // 실행하기전 상태 검사
    public void CheckState()
    {
        if (currState != null) // 돌아가는 생태가 없으면
        {
            currState.Excute(); // 해당 상태 (Excute)진입
                                // 해당 스크립트 (클래스)안의 Excute 실행
                                // FSM을 상속받고, 추상화 클래스를 구현해놓은 그 메서드
        }
    }

    // FSM의 Exit 실행, 상태 종료
    public void H_Exit()
    {
        currState.Exit(); // 해당 상태 (Exit)진입
                          // 해당 스크립트 (클래스)안의 Exit 실행
                          // FSM을 상속받고, 추상화 클래스를 구현해놓은 그 메서드
        currState = null;
        prestate = null;
    }

    // FSM 안에서 state 바꾸기
    public void Chage(FSM<TEnemy> _eState)
    {
        //_eState : enemy의 상태를 나타내는 스크립트가 매개변수로
        //(Idle , Tracking , Prowl , Die 스크립트(상태)) 

        // 같은 상태이면 return (ex) idle인데 idle로 바꾸려면?
        if (_eState == currState)
            return;

        prestate = currState;
        // 현재 상태가 있다면 종료 후
        if (currState != null)
            currState.Exit(); //현재 상태 스크립트의 exit 메서드 실행 (상태 빠져나오기)

        currState = _eState; //현재 상태를 들어온 상태로 설정
        // 새로 적용된 상태를 실행
        if (currState != null)
            currState.Enter(); // 새로 들어온 상태의 Enter 실행 (새 상태 진입)
    }

    // 상태값 세팅 (순서상, 상태값 세팅을 먼저 하고 , Idle,Attack 스크립트를 실행 해야할 듯?)
    public void Setstate(FSM<TEnemy> _state, T _owner)
    {
        Owner = _owner;
        currState = _state;

        if (currState != _state && currState != null)
            prestate = currState;


    }
}
