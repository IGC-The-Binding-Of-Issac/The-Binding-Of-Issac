using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class SnakeManager : Enemy
{
    /// <summary>
    /// <Larry J>
    /// 1. player가 방에 들어오면 몬스터 생성 , isPlayerInRoom 변수 필요없음
    /// 2. 움직임은 FIxedUpdate에서 관리
    /// 3. Head와 body에는 Enemy 태그 
    /// </summary>

    [SerializeField] float distanceBetween;

    [SerializeField] List<GameObject> bodyParts = new List<GameObject>(); // Larry의 얼굴, 몸통 오브젝트
    [SerializeField] List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;
    [SerializeField] Vector3 MoveDir; //움직일 방향 (위, 아래, 오,  왼)
    [SerializeField] int stateNum; //현재 상태의 번호
    [SerializeField] float stateTime; //현재 상태의 시간
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState; // 상태변환 

    private void Start()
    {
        //SnakeManager
        countUp = 0;
        distanceBetween = 0.2f;
        CreateBodyParts(); //초기 몸 생성 

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 5f; // 이거 바꾸면 distanceBetween도 바꿔서 생성 하는 타이밍 맞춰야함!!!
        waitforSecond = 0.5f;

        maxhp = hp;

        //Snake
        stateNum = 0; //상태 번호
        stateTime = 3f;
        chageState = true;

        randTime(); //초기에 진행할 시간 한번 구해놓기
        currTime = stateTime; // 초기에 구한 시간
    }

    
    private void FixedUpdate()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyParts();
        }
        Move();
    }
    
    private void Update()
    {

    }

    // 움직이는 최종 로직
    public override void Move()
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
            randTime(); // 랜덤 타임 구하기
            currTime = stateTime; //시간 초기화
            chageState = true;
        }
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

        //맨 처음에는 무조건 오른쪽으로
        if (stateNum == 0)
        {
            stateNum = 4;
        }
        else
        {
            rand = Random.Range(1, 5); // 1~4중
            stateNum = rand;
        }
    }
    //랜덤 시간
    void randTime()
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(0.5f, 2f);
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
    void CreateBodyParts()
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
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

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
            // 컴포넌트 추가
            if (!temp.GetComponent<MarkManager>())
            {
                temp.AddComponent<MarkManager>();
            }
            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
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