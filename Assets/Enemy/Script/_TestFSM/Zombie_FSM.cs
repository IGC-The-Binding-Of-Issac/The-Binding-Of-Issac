using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZOMBIE_STATE 
{
    Idle,
    Walk,
    Die, // 움직이고 이런거는 부모에서 작성 해야하는디
    Attack
}


public class Zombie_FSM : MonoBehaviour // 이게 Enemy 스크립트가 되고 , 이걸 상속받으면 되지 않을까?
{
    /// <summary>
    /// 
    ///  1. FSM이 오브젝트에 붙어 머신을 동작
    ///  2. State를 어떻게 관리하는지
    /// 
    /// </summary>

    // 머신에 들어갈 스테이드
    public Head_Machine<Zombie_FSM> m_state; 

    // 좀비가 미리 가지고 있을 state
    // System.Enum.GetValues(typeof(state이름)).Length : enum 타입의 길이 구하기
    // 여기선 [4]
    public FSM<Zombie_FSM>[] m_arrState = new FSM<Zombie_FSM>[System.Enum.GetValues(typeof(ZOMBIE_STATE)).Length];

    public float m_findRange;     // 좀비가 다른 오브젝트를 찾을 범위
    public Transform m_TransTarget; //  좀비가 찾은 타겟

    public int m_iHealth; // 좀비의 체력
    public float m_fAttackRange; // 좀비의 공격 범위
    public bool isTest; //true 이면 상태가 변환됨 ,  enemy에서 isplayerIn 같은 변수


    public ZOMBIE_STATE m_eCurState; // 좀비의 현재 상태 
    public ZOMBIE_STATE m_ePrevState; // 좀비의 이전 상태 

    public Animator m_Animator;

    // 생성자
    public Zombie_FSM() 
    {
        init();
    }

    // 필요 데이터 초기화
    public void init()
    {
        m_state = new Head_Machine<Zombie_FSM>();

        //생성자로 이 스크립트의 객체를 넘겨줌 (new 어쩌고 -> 생성자)
        m_arrState[(int)ZOMBIE_STATE.Idle] = new Zombie_Idle(this); //Zombie_FSM을 넘김!
        m_arrState[(int)ZOMBIE_STATE.Walk] = new Zombie_Walk(this);
        m_arrState[(int)ZOMBIE_STATE.Die] = new Zombie_Die(this);
        m_arrState[(int)ZOMBIE_STATE.Attack] = new Zombie_Attack(this);

        m_state.SetState(m_arrState[(int)ZOMBIE_STATE.Idle] , this); // Head_Machine의 SetState
        // 좀비 스테이트(this)를 idle로 바꾼다
    }

    // 상태 변화
    // 매개변수로 들어온 state로 상태를 바꾼다 (배열에 저장되어있는 상태를 비교)
    public void ChageFSM(ZOMBIE_STATE ps) 
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(ZOMBIE_STATE)).Length; i++) 
        {
            if (i == (int)ps)
                m_state.Change(m_arrState[(int)ps]);
        }
    }

    public void Begin() 
    {
        m_state.Begin(); //Zombie_FSM형식의 Head_Machine의 begin 실행 
    }

    public void Run()
    {
        m_state.Run(); //`` Head_Machine의 Run 실행 
    }

    public void Exit() 
    {
        m_state.Exit(); //`` Head_Machine의 Exit 실행 
    }
    
    // Awake
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Begin();
    }

    private void Update()
    {
        Run();

        // 뭐 시간 추가 하는 코드..어쩌고..
        
    }
}
