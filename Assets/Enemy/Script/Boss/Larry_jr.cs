using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LarryState 
{
    // 위, 아래, 오, 왼 으로 움직이는 상태
    // 0부터 4가지의 state 
    Idle, Up, Down, Left, Right
}

public class Larry_jr : Enemy
{
    [Header("래리 리")]
    [SerializeField] LarryState larryState;
    [SerializeField] int stateNum; //현재 상태의 번호
    [SerializeField] float stateTime; //현재 상태의 시간

    // 상태변환
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState;

    void Start()
    {
        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        maxhp = hp;

        //Larry
        stateNum = 0;
        stateTime = 3f;
        chageState = true;

        randTime(); //초기에 진행할 시간 한번 구해놓기
        currTime = stateTime; // 초기에 구한 시간
        larryState = LarryState.Idle; // 초기상태는 idleㄹ
    }

    private void Update()
    {
        if (playerInRoom)
            Move();
    }

    override public void Move()
    {
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randomStateNum(); //랜덤 숫자 구하기 (1~4)
            if (stateNum == 1)
                ChageState(LarryState.Up);
            else if (stateNum == 2)
                ChageState(LarryState.Down);
            else if (stateNum == 3)
                ChageState(LarryState.Left);
            else if (stateNum == 4)
                ChageState(LarryState.Right);
            chageState = false;
        }
        else if (currTime <= 0) 
        {
            randTime(); // 랜덤 타임 구하기
            stateNum = 0; //랜덤 숫자 초기화
            currTime = stateTime; //시간 초기화
            chageState = true;
        }
        
    }

    // 랜덤 시간 마다, 랜덤 상태로 변환
    void randomStateNum() 
    {
        // state 1.up , 2. down , 3.left , 4. right 상태 중에 하나를 랜덤으로 고름
        stateNum = Random.Range(1 , 5); // 1부터 4까지 상태 
    }
    void randTime() 
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(1f, 3f);
    }


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

        // 상태 변환 전 까지 실행
        while (true) 
        {
            Debug.Log("위");
            transform.position += new Vector3(0 , 1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Down()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 아래로 갑니다");

        // 상태 변환 전 까지 실행
        while (true)
        {
            transform.position += new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed;

            yield return null;
        }
    }
    private IEnumerator Right()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 오른쪽 갑니다");

        // 상태 변환 전 까지 실행
        while (true)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Left()
    {
        // 단 한번 실행
        Debug.Log("래리 리 가 왼쪽으로 갑니다");

        // 상태 변환 전 까지 실행
        while (true)
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

}
