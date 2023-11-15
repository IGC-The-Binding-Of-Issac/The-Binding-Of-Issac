using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TEnemy_FSM<T>
{
    // 상태가 상속 받을 부모 ( 해당 상태는 Begine , Run, Exit 상태를 꼭 가져야함)

    /// <summary>
    /// 해당 상태를 시작할 때 "1회" 호출
    /// </summary>
    abstract public void Enter();

    /// <summary>
    /// 해당 상태를 업데이트 할 때 "매 프레임" 호출
    /// </summary>
    abstract public void Excute();

    /// <summary>
    ///  해당 상태를 종료할 때 "1회" 호출
    /// </summary>
    abstract public void Exit();
}
