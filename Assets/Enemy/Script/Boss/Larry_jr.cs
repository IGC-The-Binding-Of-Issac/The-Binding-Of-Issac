using System.Collections.Generic;
using UnityEngine;

/*
public enum LarryState 
{
    // 위, 아래, 오, 왼 으로 움직이는 상태
    // 0부터 4가지의 state 
    Idle, Up, Down, Left, Right
}
*/

public class Larry_jr : Enemy
{
    [Header("래리 리")]
    // [SerializeField] LarryState larryState;
    [SerializeField] int stateNum; //현재 상태의 번호
    [SerializeField] float stateTime; //현재 상태의 시간

    // 상태변환
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState;

    [Header("몸통")]
    [SerializeField] int spawnObj; //머리 포함 갯수
    [SerializeField] int gap;
    [SerializeField] GameObject larryBody;
    [SerializeField] Vector3 MoveDir; //움직일 방향 (위, 아래, 오,  왼)
    [SerializeField] List<GameObject> segments = new List<GameObject>(); // 몸통 오브젝트를 담을 배열
    [SerializeField] List<Vector3> PositionHistory = new List<Vector3>(); // 몸통 위치를 저장하는 배열 -> 뒤의 오브젝트가 앞에 따라가게 


    void Start()
    {

        playerInRoom = false;
        dieParameter = "isDie";
        animator = GetComponent<Animator>();

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 2f;
        waitforSecond = 0.5f;

        maxhp = hp;

        //Larry
        stateNum = 0;
        stateTime = 3f;
        chageState = true;

        randTime(); //초기에 진행할 시간 한번 구해놓기
        currTime = stateTime; // 초기에 구한 시간
        spawnObj = 13; //머리 포함 초기 생성 갯수
        gap = 50; // 게임 오브젝트들 사이에 움직임 수정할때 : 작으면 촘촘히 붙어서 움직임

        Setup(); //larry의 몸 만들기

    }

    private void Update()
    {
        if (playerInRoom)
            Move();

    }

    override public void Move()
    {
        // 움직임
        MoveSegment();
        // 머리 애니메이션
        chageHeadAni();

        //방향 정하기
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randNum(); //랜덤 숫자 구하기 (1~4)
            if (stateNum == 1)
                MoveDir = Vector3.up;
            else if (stateNum == 2)
                MoveDir = Vector3.down;
            else if (stateNum == 3)
                MoveDir = Vector3.left;
            else if (stateNum == 4)
                MoveDir = Vector3.right;
            chageState = false;
        }
        else if (currTime <= 0) 
        {
            randTime(); // 랜덤 타임 구하기
            currTime = stateTime; //시간 초기화
            chageState = true;
        }
        
    }

    // 머리 + 몸통 만들기
    private void Setup()
    {
        // Snake 본체를 segments 리스트에 저장
        segments.Add(larryBody);

        // Snake를 쫓아다니는 꼬리(segment 오브젝트)를 생성하고, segments 리스트에 저장
        for (int i = 0; i < spawnObj-1; ++i)
        {
            AddSegment();
        }
    }

    private void AddSegment()
    {
        GameObject segment = Instantiate(larryBody);
        segment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(segment);
    }

    //몸통까지 움직임
    private void MoveSegment() 
    {
        //본인(머리)움직임
        transform.position += MoveDir * Time.deltaTime * moveSpeed;

        //몸통 움직임
        PositionHistory.Insert(0, transform.position); //위치 담아놓는 배열-> 0번째 index에 머리위치 담기
        int index = 0;
        foreach (var body in segments)
        {
            Vector3 posi = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];
            // index : 머리가 움직이고 얼마뒤에 (gap * 0)만큼 움직임
            // gap : 게임 오브젝트 사이들 끼리 움직임?
            Vector3 posiforwad = posi - body.transform.position;
            body.transform.position += posiforwad;
            index++;
        }

    }

    // 방향에 따라 Head 애니메이션 바뀌게
    void chageHeadAni() 
    {
        // state 1.up , 2. down , 3.left , 4. right 상태 
        if (stateNum == 1)
            animator.SetTrigger("isLarryTop");
        else if (stateNum == 2)
            animator.SetTrigger("isLarryDown");
        else if (stateNum == 3)
            animator.SetTrigger("isLarryLeft");
        else if (stateNum == 4)
            animator.SetTrigger("isLarryRight");


    }

    // 랜덤 시간 마다, 랜덤 상태로 변환
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

        // 좌우 방향은 전환 x
        // 위아래 방향은 전환 x
        /*
        if (stateNum == 1 || stateNum == 2) 
        {
            rand = Random.Range(3,5); // 3~4중
            stateNum = rand;
        }
        else if (stateNum == 3 || stateNum == 4)
        {
            rand = Random.Range(1, 3); // 1~2중
            stateNum = rand;
        }
        else 
        {
            rand = Random.Range(1, 3); // 1~4중
            stateNum = rand;
        }
        */
    }
    void randTime() 
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(1f, 3f);
    }
    
    //유한상태머신 구현
    /*
    //상태 변환
    private void ChageState(LarryState newState) 
    {
        StopCoroutine(larryState.ToString());
        larryState = newState;
        StartCoroutine(larryState.ToString());
    }


    // Up 상태 일 때,
    private IEnumerator Up()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 위로 갑니다");
        MoveDir = Vector3.up;

        // 상태 변환 전 까지 실행
        while (true) 
        {
            //transform.position += new Vector3(0 , 1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Down()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 아래로 갑니다");
        MoveDir = Vector3.down;

        // 상태 변환 전 까지 실행
        while (true)
        {
            //transform.position += new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
    private IEnumerator Right()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 오른쪽 갑니다");
        MoveDir = Vector3.right;

        // 상태 변환 전 까지 실행
        while (true)
        {
            //transform.position += new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Left()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 왼쪽으로 갑니다");
        MoveDir = Vector3.left;

        // 상태 변환 전 까지 실행
        while (true)
        {
            //transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
    */
}
