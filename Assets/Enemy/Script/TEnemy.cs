using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TENEMY_STATE // 스크립트로 만들 상태 
{
    Idle,
    Prowl,
    Tracking,
    Shoot,
    Die
}

public class TEnemy : MonoBehaviour
{
    /// <summary>
    /// - 내가 짠 Zombie_FSM 의 기능을 하는, 머신이 돌아가는 주체 
    /// - Enemy 스크립트가 될 예정
    /// </summary>

    // TEnemy를 넣을 머신
    public TEnemy_HeadMachine<TEnemy> headState;

    /// <summary>
    /// 1. 좀비가 미리 가지고 있을 state배열
    /// 2. System.Enum.GetValues(typeof(state이름)).Length : enum 타입의 길이 구하기 ,  여기서는 5
    /// </summary>
    public TEnemy_FSM<TEnemy>[] arrayState = new TEnemy_FSM<TEnemy>[System.Enum.GetValues(typeof(TENEMY_STATE)).Length];

    // 상속 받을 Enemy의 종류
    [SerializeField] protected bool isTracking;              // tracking 하는가?
    [SerializeField] protected bool isProwl;                 // prowl (배회) 하는가?
    [SerializeField] protected bool isDetective;             // detective (감지) 하는가?

    // enemy 종류 프로퍼티
    public bool getIsTracking { get => isTracking; }
    public bool getisProwl { get => isProwl; }
    public bool getisDetective { get => isDetective; }


    // Enemy가 가지고 있을 기본 스탯들
    public bool playerInRoom;               // 플레이어가 방에 들어왔는지 여부
    protected float hp;                       // hp
    protected float sight;                  // 시야 범위
    protected float moveSpeed;              // 움직임 속도

    // enemy 스탯 프로퍼티
    public float getMoveSpeed { get => moveSpeed; }
    public float getSight { get => sight; }

    // 플레이어 위치
    public Transform trackingTarget;

    // Enemy가 가지고 있는 컴포넌트 (init 에서 초기화)
    protected Animator animator;            // 애니메이터

    // Enemy의 상태 (enum)
    public TENEMY_STATE eCurState;          // 현재 상태
    public TENEMY_STATE ePreState;          // 이전 상태

    /// <summary>
    /// - 생성자
    /// 1. 자식 클래스가 부모 클래스의 생성자를 가져와야함 -> 오류남, start나 awake 에 사용헤야함
    /// (해결 : 초기화 하는 메서드를 하위 오브젝트의 start 함수에서 실행시킴)
    /// </summary>
    /*
    public TENemy() 
    {
        Debug.Log("부모 생성자 실행");
        init();
    }
    */

    /// <summary>
    /// 
    /// - 하위 몬스터가 반드시 가져야 할 것
    /// 1. setStateArray()
    ///     - 상태 배열 arrayState를 세팅 
    ///     - 상태 배열에 this로 넣어서 하위 몬스터가 들어감
    ///     - 하위 몬스터에서 현재 상태 (idle)를 실행하면, 상태변화가 될 것
    /// 2. En_setState()
    ///     - 몬스터가 필요한 스탯
    ///     - ex) playerInRoom , hp, sight등
    /// 3, En_kindOfEnemy()
    ///     - 어떤 행동을 하는지 
    ///     - isTracking(추적) , isProwl(배회) ,isDetective(감지) 를 true, false로 표현
    /// 4. En_Start()
    ///     - 현재 상태를 실행 해주는 함수
    ///     - 하위 몬스터의 start에 넣으면됨
    ///     
    /// </summary>


    protected void En_stateArray()
    {
        init();
    }

    // 필요한 데이터 초기화
    private void init()
    {
        // Enemy_HeadMachine의 타입을 TEnemy로 지정 , 머신을 생성 (new 사용)
        headState = new TEnemy_HeadMachine<TEnemy>();

        /// <summary>
        /// 1. 해당 상태 (스크립트)의 생성자를 사용 -> Enemy 가 붙어있는 객체를 넘겨줌 
        /// 2. new 스크립트 이름 (매개변수) 
        /// 3. Enemy를 상속 하는 하위몬스터 또한 Enemy 타입(Enemy를 상속 받고 있기 때문)
        ///     <!테스트 필요>
        ///     -> 하위 몬스터에서 init을 실행하면 상태머신을 실행 하지 않을까?
        ///  
        /// Q. 왜 Enemy_Idle등의 스크립트를 FSM<TENemy> 배열에 넣을 수 있는가?
        ///     A. FSM<Zombie_FSM>를 가진 스크립트들의 FSM<Zombie_FSM>를 arr에 넣기때문 
        /// Q. 왜 어렵게  arrayState[(int)TENEMY_STATE.Idle]이렇게 하지? arr[0]하면 안됨?
        ///     A. 직관성을 위해서 , arr[0] 하면 배열의 0번째가 어떤 타입인지 모르니까
        /// </summary>
        arrayState[(int)TENEMY_STATE.Idle] = new Enemy_Idle(this);
        arrayState[(int)TENEMY_STATE.Prowl] = new Enemy_Prowl(this);
        arrayState[(int)TENEMY_STATE.Tracking] = new Enemy_Tracking(this);
        arrayState[(int)TENEMY_STATE.Shoot] = new Enemy_Shoot(this);
        arrayState[(int)TENEMY_STATE.Die] = new Enemy_Die(this);
        //Debug.Log(this.gameObject.tag); -> 자식 오브젝트에서 생성하면 자식 오브젝트가 this

        // Enemy 상태를 Idle 상태로 초기 설정
        headState.Setstate(arrayState[(int)TENEMY_STATE.Idle], this);

        // 컴포넌트 초기화
        animator = this.gameObject.GetComponent<Animator>();
    }

    public virtual void En_setState() { }         // 초기 세팅 (Getcomponent, hp 설정 등)
    public virtual void En_kindOfEnemy() { }      // Enemy의 종류 (isTracking , isProwl ,isDetective )
    public void En_Start()                        // 현재 상태 (idle)의 begin 실행 
    {
        E_Enter();
    }


    public void E_Enter()
    {

        /// <summary>
        /// - init()메서드에서 현재 상태를 idle 로 설정
        /// - idle 상태의 Enter 메서드 실행
        /// </summary>
        headState.H_Enter();
    }
    public void E_Excute()
    {
        headState.H_Excute();
    }

    public void E_Exit()
    {
        headState.H_Exit(); ;
    }

    /// <summary>
    /// - 상태 변환
    /// 1. 상태 안에서 (상태 스크립트 안에서) "내상태-> 다음상태" 변환 할 때 사용
    /// </summary>
    public void ChageFSM(TENEMY_STATE ps)
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(TENEMY_STATE)).Length; i++)
        {
            if (i == (int)ps)
                headState.Chage(arrayState[(int)ps]);
        }

    }

    /// <summary>
    /// * 기타 동작 메서드
    /// 
    /// </summary>
    /// 
    public void e_findPlayer()                              // tracking할 enemy 를 탐색
    {
        trackingTarget = GameObject.FindWithTag("Player").transform;
    }
    public void e_Tracking()                                // tracking 움직임
    {
        gameObject.transform.position
            = Vector3.MoveTowards(gameObject.transform.position, trackingTarget.transform.position, getMoveSpeed * Time.deltaTime);
    }

    public bool e_SearchingPlayer()                         // 범위 안에 player 감지
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, getSight);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }

        }
        return false;
    }
}
