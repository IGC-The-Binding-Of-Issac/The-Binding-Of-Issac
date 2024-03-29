using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : TEnemy
{
    /// <summary>
    /// <Larry J>
    /// 1. player가 방에 들어오면 몬스터 생성 , isPlayerInRoom 변수 필요없음
    /// 2. 움직임은 FIxedUpdate에서 관리
    /// 3. animator은 머리를 생성할 때 head의 애니이션으로 설정함 
    /// 
    /// </summary>

    [Header("Larry")]
    [SerializeField] float distanceBetween;
    [SerializeField] int larryLength; // body 길이 만
    [SerializeField] GameObject larryHead_;
    [SerializeField] GameObject larryBody_;

    [SerializeField] List<GameObject> bodyParts = new List<GameObject>(); // Larry의 얼굴, 몸통 오브젝트를 저장
    public List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;
    [SerializeField] Vector3 MoveDir; //움직일 방향 (위, 아래, 오,  왼)
    public int stateNum; //현재 상태의 번호
    [SerializeField] float stateTime; //현재 상태의 시간
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState; // 상태변환 
    [SerializeField] bool isReadyDie; //몸통이 다 생성 되었으면 true
    float iniHp; // 초기 hp 설정

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //SnakeManager
        // bodyParts배열에 값 넣어주기
        larryLength = 12; //head 포함 12 개 
        countUp = 0;
        distanceBetween = 0.2f;

        CreateBodyParts();  // bodyPart List에 값 넣기
        CreateSnakeBody(); //  SnakepPart List에 값 넣기 

        // Enemy
        hp = 110f;
        sight = 5f;
        moveSpeed = 5f; // 이거 바꾸면 distanceBetween도 바꿔서 생성 하는 타이밍 맞춰야함!!!
        waitforSecond = 0.3f;
        maxhp = hp;

        //Snake
        stateNum = 0; //상태 번호
        stateTime = 3f;
        chageState = true;
        isReadyDie = false;
        randTime(); //초기에 진행할 시간 한번 구해놓기
        currTime = stateTime; // 초기에 구한 시간
        iniHp = hp;

    }
    
    private void FixedUpdate()
    {
        if (bodyParts.Count > 0)
        {
            CreateSnakeBody();
        }
        Move();
    }
    
    private void Update()
    {
        //몸통이 다 만들어졌는지 검사
        if (!isReadyDie)
            hp = iniHp;
        if (snakeBody.Count == larryLength)
            isReadyDie = true;

        //죽음
        larryDie();
    }

    //데미지를 입는
    public void getDamageLarry() 
    {
        base.GetDamage(PlayerManager.instance.playerDamage);
    }


    //폭탄에 데미지를 입는
    public void getBombDamage(float bombDamage) 
    {
        base.GetDamage(bombDamage);
    }

    //데미지를 주는
    public void hitDamagePlayer() 
    {
        //플레이어랑 부딪히면 플레이어의 hp감소
        PlayerManager.instance.GetDamage();
    }

    // 움직이는 최종 로직
    public void Move()
    {
        SnakeMovement();

        //방향 정하기
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randNum(); //랜덤 숫자 구하기 (1~4)
            chageAniDir(); // 애니메이션과 방향 바꿈
            chageState = false;
        }
        else if (currTime <= 0)
        {
            stateReset();
        }
    }

    //초기화
    public void stateReset() 
    {  
        randTime(); // 랜덤 타임 구하기
        currTime = stateTime; //시간 초기화
        chageState = true;

    }

    //스크립트로 bodyParts배열에 오브젝트 넣기 (start에서 한번 실행)
    public void CreateBodyParts() 
    {
        bodyParts.Add(larryHead_);          //머리넣고
        for(int i= 1;i<larryLength; i++)    //갯수만큼 몸통
        {
            bodyParts.Add(larryBody_);
        }
    }

    // 일단 하드코딩 해놓고, 나중에 수정해보자!
    // 피가 10 줄어들면 몸통 하나씩 없어짐
    public void larryDie() 
    {
        //피가 90 이하이고
        if (hp <= 90)
        {
            // 스네이크 배열이 12개 이면 (다 있으면), snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength) 
            {
                GameObject deleSnake = snakeBody[larryLength - 1];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie" , true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake , waitforSecond);
                DestorySound();
            }
        }
        // 피가 80 이하이고
        if (hp <= 80f)
        {
            // 스네이크 배열이 11개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength-1 )
            {
                GameObject deleSnake = snakeBody[larryLength - 2];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }
        // 피가 70 이하이고
        if (hp <= 70f )
        {
            // 스네이크 배열이 10개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength - 2)
            {
                GameObject deleSnake = snakeBody[larryLength - 3];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }
        if (hp <= 60f )
        {
            // 스네이크 배열이 9개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength - 3)
            {
                GameObject deleSnake = snakeBody[larryLength - 4];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }
        if (hp <= 50f )
        {
            // 스네이크 배열이 8개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength - 4)
            {
                GameObject deleSnake = snakeBody[larryLength - 5];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }
        if (hp <= 40f )
        {
            // 스네이크 배열이 7개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength - 5)
            {
                GameObject deleSnake = snakeBody[larryLength - 6];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }
        if (hp <= 30f )
        {
            // 스네이크 배열이 6개 이면, snakeBody에 있는거 하나 지우기
            if (snakeBody.Count == larryLength - 6)
            {
                GameObject deleSnake = snakeBody[larryLength - 7];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
            }
        }
        if (hp <= 20f )
        {
            // 스네이크 배열이 4개 이면, snakeBody에 있는거 1 + 1 지우기
            if (snakeBody.Count == larryLength - 7)
            {
                GameObject deleSnake = snakeBody[larryLength - 8];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                // 하나 더 지우기
                GameObject deleSnake2 = snakeBody[larryLength - 9];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake2);
                Destroy(deleSnake2 , waitforSecond);
                DestorySound();
            }
        }

        //피가 10 남았을 떄, 머리 1 + 몸통이 1개
        if (hp <= 10f)
        {
            // 스네이크 배열이 3개 이면, snakeBody에 있는거 1지우기
            if (snakeBody.Count == larryLength - 9)
            {
                GameObject deleSnake = snakeBody[larryLength - 10];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }
        }

        // 죽으면
        if(hp <= 0) 
        {
            //마지막에 남아있는 body 지우기
            if (snakeBody.Count == larryLength - 10)
            {
                GameObject deleSnake = snakeBody[snakeBody.Count -1];
                deleSnake.GetComponent<Animator>().SetBool("isLarryBodyDie", true);
                snakeBody.Remove(deleSnake);
                Destroy(deleSnake, waitforSecond);
                DestorySound();
            }

            // 죽는 애니메이션 실행
            animator.SetBool("isLarryDie", true);
            // Destory
            Destroy(gameObject , waitforSecond);
        }


    }

    void DestorySound()
    {
        audioSource.clip = SoundManager.instance.GetEnemyDeadClip();
        audioSource.Play();
    }

    // 상태에 따른 애니메이션과 방향 전환
    public void chageAniDir() 
    {
        if (stateNum == 1)
        { 
            MoveDir = Vector3.up;
            animator.SetTrigger("isLarryTop");
        }
        else if (stateNum == 2) 
        {
            MoveDir = Vector3.down;
            animator.SetTrigger("isLarryDown");
        }
        else if (stateNum == 3) 
        { 
            MoveDir = Vector3.left;
            animator.SetTrigger("isLarryLeft");
        }
        else if (stateNum == 4) 
        { 
            MoveDir = Vector3.right;
            animator.SetTrigger("isLarryRight");
        }

    }

    //랜덤 상태 
    void randNum()
    {
        // state 1.up , 2. down , 3.left , 4. right 상태 중에 하나를 랜덤으로 고름
        int rand = 0;
        bool isreturn = true;

        //맨 처음에는 무조건 오른쪽으로
        if (stateNum == 0)
        {
            stateNum = 4;
        }
        else
        {
            while (true) 
            {
                rand = Random.Range(1, 5); // 1~4중
                if (rand != stateNum) //같은 상태가 아니면
                {
                    stateNum = rand;

                    isreturn = true;
                    break;
                }
            }
            if (isreturn)
                return;
        }

    }
    //랜덤 시간
    void randTime()
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(0.3f, 1f);
    }

    // 움직임
    void SnakeMovement()
    {
        //snakeBody[0]은 머리 , 머리의 움직임을 MoveDir방향으로 이동
        snakeBody[0].transform.position += MoveDir * moveSpeed * Time.deltaTime;



        // snakeBody의 몸통이 있으면 ( = 머리를 제외하고 몸통의 움직임)
        // 몸통이 움직이게 조건 걸어놓은듯!
        if (snakeBody.Count > 1)
        {
            // for문을 돌면서 첫번째 몸통~끝몸통 이동
            for (int i = 1; i < snakeBody.Count; i++)
            {
                // 이전의 몸통의 MarkManager를 가져옴
                MarkManager markM = snakeBody[i - 1].GetComponent<MarkManager>();
                // i번째 몸통의 위치는 "이전의 몸통"의 위치
                snakeBody[i].transform.position = markM.markerList[0].position;
                // i번째 몸통의 회전은 ``
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                // 한번 이동 했으면 list를 지워줌
                markM.markerList.RemoveAt(0);
            }
        }

    }

    //몸통 생성 코드
    void CreateSnakeBody()
    {
        //머리 생성 (snakeBody에 아무것도 생성 되어있지 않을 때)
        if (snakeBody.Count == 0)
        {
            // bodyParts의 첫번재(머리) 부분을, 현재위치에 생성
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            //첫번째 요소 (머리)의 애니메이터 가져오기
            animator = temp1.GetComponent<Animator>();

            // 컴포넌트 추가
            // MarkManager컴포넌트 , Rigidbody2D컴포넌트 (Rigidbody2D는 지금 안쓰지만 미리 넣어놓는다고 생각하자) 
            if (!temp1.GetComponent<MarkManager>())
                temp1.AddComponent<MarkManager>();

            // snakeBody에 instance화 된 오브젝트 추가
            snakeBody.Add(temp1);
            // bodyParts에 첫번째 삭제 (첫번째가 삭제하면서 뒤에 있는 오브젝트의 인덱스가 땡겨짐 / 1은 0으로, 2는1로.....)
            bodyParts.RemoveAt(0);
        }


        //snakeBody의 본인 바로 앞에 (인덱스 -1)의 MarkManager를 가져옴
        MarkManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkManager>();
        // countUp이 초기화 될 때 마다 (0 일때 마다)
        if (countUp == 0)
        {
            // 본인 앞에 있는 (인덱스 -1) 리스트를 지움
            markM.ClearmMarkerList();
        }

        // 순차적으로 생성 , distanceBetween 마다
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            // bodyParts에 첫번째가 전에 생성된 오브젝트의 위치에 생성
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation, transform);
            
            //LarryHeadParent밑의 첫번재 오브젝트의 자식으로
            temp.transform.SetParent(snakeBody[0].transform);

            // 컴포넌트 추가
            if (!temp.GetComponent<MarkManager>())
            {
                temp.AddComponent<MarkManager>();
            }

            // snakeBody에 추가
            snakeBody.Add(temp);
            // 추가된 오브젝트를 bodyParts에서 지우기
            bodyParts.RemoveAt(0);
            // 추가된 오브젝트의 리스트를 지움
            temp.GetComponent<MarkManager>().ClearmMarkerList();
            // count 초기화
            countUp = 0;
        }
    }


}