using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public enum TENEMY_STATE // 스크립트로 만들 상태 
{
    Idle,
    Prowl,
    Tracking,
    Shoot,
    Jump,
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
    [SerializeField] protected bool isShoot;                 // shoot (총 쏨) 하는가?
    [SerializeField] protected bool isJump;                  // jump (점프) 하는가?

    // enemy 종류 프로퍼티
    public bool getIsTracking { get => isTracking; }
    public bool getisProwl { get => isProwl; }
    public bool getisDetective { get => isDetective; }
    public bool getisShoot { get => isShoot; }
    public bool getisJump { get => isJump; }


    // Enemy가 가지고 있을 기본 스탯들
    public bool playerInRoom;                   // 플레이어가 방에 들어왔는지 여부
    protected float hp;                         // hp
    protected float sight;                      // 시야 범위
    protected float moveSpeed;                  // 움직임 속도
    protected float waitforSecond;              // destroy 전 대기 시간 
    protected float attaackSpeed;               // 공격 속도
    protected float bulletSpeed;                // 총알 속도 
    protected float fTime;                      // prowl - 랜덤 이동 시간
    protected float randRange;                  // prowl - 랜덤 이동 거리
    protected float jumpSpeed;                  // jump -  점프 속도
    protected int enemyNumber = -1;                  // enemyNumber = 몬스터 고유번호 ( 풀링

    //
    protected string dieParameter;              // 죽는   애니메이션 실행 파라미터
    protected string shootParameter;            // 총쏘는 애니메이션 실행 파라미터
    protected string jumpParameter;             // 점프   애니메이션 실행 파라미터
    protected bool knockBackState = false;      // 넉백s
    protected bool isFlipped = true;                      // 뒤집기
    bool isRaadyShoot;                          // shoot -  총 쏘는 조건

    // prowl
    float mx;                         // 본인 x
    float my;                         // 본인 y
    float xRan;                       // random - x 랜덤 움직임
    float yRan;                       // random - y 랜덤 움직임


    // enemy 스탯 프로퍼티
    public bool setIsReadyShoot
    {
        set { isRaadyShoot = value; }
    }
    public float getMoveSpeed { get => moveSpeed;  }
    public float getattaackSpeed { get => attaackSpeed; }
    public float JumpSpeed
    {
        get => jumpSpeed;
        set { JumpSpeed = value; }
    }

    // Enemy의 Hp바
    protected float maxhp;                      // hp 바의 max 
    public Image hpBarSlider;                   // hp 바의 이미지

    // 컴포넌트
    public GameObject enemyBullet;              // 총알
    public GameObject roomInfo;                 // 방 정보 오브젝트
    public Transform playerPosi;                // 플레이어 위치

    // Enemy가 가지고 있는 컴포넌트 (init 에서 초기화)
    protected Animator animator;                // 애니메이터
    protected AudioSource audioSource;          // 몬스터 오디오

    // Enemy의 상태 (enum)
    public TENEMY_STATE eCurState;              // 현재 상태
    public TENEMY_STATE ePreState;              // 이전 상태

    /// <summary>
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
        arrayState[(int)TENEMY_STATE.Idle]      = new Enemy_Idle(this);
        arrayState[(int)TENEMY_STATE.Prowl]     = new Enemy_Prowl(this);
        arrayState[(int)TENEMY_STATE.Tracking]  = new Enemy_Tracking(this);
        arrayState[(int)TENEMY_STATE.Shoot]     = new Enemy_Shoot(this);
        arrayState[(int)TENEMY_STATE.Jump]      = new Enemy_Jump(this);
        arrayState[(int)TENEMY_STATE.Die]       = new Enemy_Die(this);
        //Debug.Log(this.gameObject.tag); -> 자식 오브젝트에서 생성하면 자식 오브젝트가 this

        // Enemy 상태를 Idle 상태로 초기 설정
        headState.Setstate(arrayState[(int)TENEMY_STATE.Idle], this);

        // 컴포넌트 초기화
        animator = this.gameObject.GetComponent<Animator>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public virtual void En_setState() { }         // 초기 세팅 (Getcomponent, hp 설정 등)
    public virtual void En_kindOfEnemy() { }      // Enemy의 종류 (isTracking , isProwl ,isDetective )
    public void En_Start()                        // 하위 몬스터 start에서 실행         
    {
        En_stateArray();                          // 초기 배열 설정
        E_Enter();                                // 현재 상태 (idle)의 begin 실행 
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
    /// </summary>  

  

    // 플레이어 쳐다보기
    public void e_Lookplayer()
    {
        if (transform.position.x > playerPosi.position.x && isFlipped)
        {
            //transform.Rotate(0f, 180f, 0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = false;
        }
        else if (transform.position.x < playerPosi.position.x && !isFlipped)
        {
            //transform.Rotate(0f, 180f, 0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = true;
        }
    }
    // tracking할 enemy 를 탐색
    public void e_findPlayer()                             
    {
        playerPosi = GameObject.FindWithTag("Player").transform;
    }
    // tracking 움직임
    public void e_Tracking(float _speed)                               
    {
        if (knockBackState) // 넉백 상태
            return;

        gameObject.transform.position
            = Vector3.MoveTowards(gameObject.transform.position, playerPosi.transform.position, _speed * Time.deltaTime);
    }

    // 범위 안에 player 감지
    public bool e_SearchingPlayer()                         
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 sightSize = new Vector2(x, y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sightSize, sight); //시작 위치 , 범위

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    // enemy 초기 위치
    public void e_moveInialize() 
    {
        mx = gameObject.transform.position.x;
        my = gameObject.transform.position.y;
        xRan = mx;
        yRan = my;
    } 

    //랜덤 움직임
    public void e_Prwol()
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, moveSpeed * Time.deltaTime);
    }

    // 총 쏘기
    public void e_Shoot() 
    {
        animator.SetTrigger(shootParameter);
        if (isRaadyShoot) {
            GameObject bulletobj = EnemyPooling.Instance.GetFollowBullet(this.gameObject);   // follow 하는 bullet pooling
            //bulletobj.transform.position = gameObject.transform.position;
            //GameObject bulletobj = Instantiate(enemyBullet, transform.position + new Vector3(0, -0.1f, 0), Quaternion.identity);
            isRaadyShoot = false;
        }
    }

    // Tracking -> Shoot으로 넘어갈때 조건 (invoke)
    public void invokeShoot()
    {
        Invoke("chageToShoot", attaackSpeed);             // attaackSpeed 후에  

    }
    public void chageToShoot()
    {
        ChageFSM(TENEMY_STATE.Shoot);           // Shoot으로 상태 변화
    }

    // Tracking -> jump로 넘어갈때 조건 (invoke)
    public void invokeJump()
    {
        Invoke("chageTojump", attaackSpeed);             //  attaackSpeed 후에

    }
    public void chageTojump()
    {
        ChageFSM(TENEMY_STATE.Jump);           // Shoot으로 상태 변화
    }

    // jump 실행전 세팅
    public void e_jumpSet()
    {
        animator.SetBool(jumpParameter, true);       //jumpAiPlayTime 만큼 점프 ani 실행
    }

    // jump -> tracking 
    public void e_doneJump()
    {
        animator.SetBool(jumpParameter, false);
        ChageFSM(TENEMY_STATE.Tracking);
    }

    // Prowl - 랜덤 움직임 코루틴 실행
    public void startRaomPosiCoru()
    {
        StartCoroutine(checkPosi());
    }
    //랜덤 움직임 검사
    public IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);
            xRan = Random.Range(mx + randRange, mx - randRange);    // x위치는 현재 위치 randRange부터 , 현재위치 -randRange까지
            yRan = Random.Range(my + randRange, my - randRange);    // y위치 동일
        }
    }


    // 일반 몬스터 collider검사
    private void OnCollisionStay2D(Collision2D collision)       
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();                 //플레이어랑 부딪히면 플레이어의 hp감소
        }
    }

    // 일반 몬스터 trigger 검사
    private void OnTriggerEnter2D(Collider2D collision)             
    {
        if (collision.gameObject.CompareTag("Tears"))               //눈물이랑 부딪히면 색 변화
        {
            StartCoroutine(Hit());
        }
    }

    // 색 변화 코루틴
    IEnumerator Hit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // 넉백 코루틴 
    public IEnumerator knockBack()
    {
        knockBackState = true;
        yield return new WaitForSeconds(0.2f);
        knockBackState = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // 일반 몬스터 hp 바
    // 일반 몬스터 hp 바
    //public void CheckHp()
    //{
    //    hpBarSlider.fillAmount = hp / maxhp;
    //}
    public IEnumerator CheckHp()
    {
        //hpBarSlider.fillAmount = Mathf.Lerp(hpBarSlider.fillAmount, hp / maxhp, Time.deltaTime * 1f);
        while (hp <= maxhp)
        {
            float health = hp / maxhp;
            float currentHp = Mathf.Lerp(hpBarSlider.fillAmount, hp / maxhp, Time.deltaTime * 3.5f);
            hpBarSlider.fillAmount = currentHp;

            yield return null;
        }
    }

    // player 데미지 만큼 피 감소 (Tear 스크립트 에서 사용 )
    public void GetDamage(float damage)
    {
        hp -= damage;
        StartCoroutine(CheckHp());
    }

    // enemy 죽음
    public bool e_isDead() 
    {
        if (hp <= 0)
            return true;
        return false;
    }

    // enemy 삭제
    public void e_destroyEnemy()
    {
        e_deadSound();
        animator.SetBool(dieParameter , true);
        Destroy(gameObject ,waitforSecond);
    }

    public void e_deadSound()
    {
        // 예외처리 - 오디오가 없는 오브젝트
        if (audioSource == null) 
            return;

        Debug.Log("dead");

        audioSource.clip = SoundManager.instance.GetEnemyDeadClip();
        audioSource.Play();
    }
}
