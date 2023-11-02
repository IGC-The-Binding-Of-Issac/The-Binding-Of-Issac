using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum SpiderState 
{ 
    SpiderStay,
    SpiderRandMove
}

public class Spider : Top_Spider
{
    //  랜덤 움직임
    [SerializeField] SpiderState state;
    float currTime;
    float spdierMoveTime;

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 3f;
        sight = 4f;
        moveSpeed = 2f;
        waitforSecond = 1f;
        attaackSpeed = 0.5f; // stay <-> move

        maxhp = hp;
        //TopFly
        randRange = 3f;
        fTime = 0.5f;
        StartCoroutine(checkPosi(randRange));

        //Spider
        currTime = attaackSpeed;
        spdierMoveTime = 3f;
    }
    private void Update()
    {
        if (playerInRoom)
        {
            Move();
        }

    }

    public override void Move()
    {
        //상태 변화
        currTime += Time.deltaTime;
        if (currTime < attaackSpeed)
        {
            ChageState(SpiderState.SpiderStay);
        }
        else if (currTime < spdierMoveTime)
        {

            ChageState(SpiderState.SpiderRandMove);
        }
        else
        {
            // 타이머를 재설정하고 "SpiderStay" 상태로 돌아감
            animator.SetBool("isMove", false);
            currTime = 0;
            ChageState(SpiderState.SpiderStay);
        }
    }

    void ChageState(SpiderState newState)
    {
        // 이전 상태 종료
        StopCoroutine(state.ToString());
        // 새로운 상태로 변경
        state = newState;
        // 현재 상태의 코루틴 실행
        StartCoroutine(state.ToString());
    }

    //상태 
    IEnumerator SpiderStay()
    {
        while (true)
        {
            //아무것도 안함
            yield return null;
        }
    }
    IEnumerator SpiderRandMove()
    {
        animator.SetBool("isMove", true);
        while (true)
        {
            Prwol();
            yield return null;
        }
    }

}
