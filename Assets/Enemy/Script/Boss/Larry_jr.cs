using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] Transform larryBody;
    [SerializeField] int spawnObj; //머리 포함 갯수
    [SerializeField] List<Transform> segments = new List<Transform>();
    [SerializeField] Vector3 MoveDir; //움직일 방향 (위, 아래, 오,  왼)

    void Start()
    {


        playerInRoom = false;
        dieParameter = "isDie";

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
        // 머리(본인)를 segment 리스트에 저장
        segments.Add(transform);

        // 머리를 따라 다니는 꼬리 (larryBody)를 생성, segment 리스트에 저장하기
        for (int i = 1; i < spawnObj; i++) 
        {
            AddSegment();
        }

    }

    // 머리에 몸통 붙이기
    private void AddSegment() 
    {
        // position 사용해서 연결된거 뒤에 연결되게
        Transform seg = Instantiate(larryBody);
        seg.position += segments[segments.Count - 1].position; 
        // set위치 , 전에 있는 세그 위치 + x위치 1만큼
        segments.Add(seg);
    }

    //몸통까지 움직임
    private void MoveSegment() 
    {
        //본인(머리)움직임
        transform.position += MoveDir * Time.deltaTime * moveSpeed;
        Vector3 desti;
        desti = new Vector3(transform.position.x, transform.position.y, 0);

        //몸통 움직임
        for (int i = 1; i < segments.Count; i++)
        {
            //현재 tramsfom 저장
            Vector3 now = new Vector3(segments[i].position.x, segments[i].position.y, 0);
            //현재 위치를 저장 해놓은 desti로 이동
            segments[i].position = Vector3.MoveTowards(segments[i].transform.position, desti, moveSpeed * Time.deltaTime);
            //desti를 현재 위치로 저장
            desti = now;
        }

    }

    // 랜덤 시간 마다, 랜덤 상태로 변환
    void randNum() 
    {
        // state 1.up , 2. down , 3.left , 4. right 상태 중에 하나를 랜덤으로 고름
        int rand = 0;
        // 좌우 방향은 전환 x
        // 위아래 방향은 전환 x
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
