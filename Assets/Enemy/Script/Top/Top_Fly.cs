using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Fly : Enemy
{
    //Fly 이동
    [HideInInspector] 
    protected float x;
    protected float y;
    protected float xRan;
    protected float yRan;
    // 랜덤 움직임 넣을 때 꼭 필요함! + 
    protected float randRange; //랜덤으로 이동할 범위 
    protected float fTime; //랜덤이동 시간
    protected Transform justTrackingPlayerPosi; // 무조건 추적 위한 update문에서 player 실시간 받아오기
    protected bool isFlipped;

    protected void Fly_Move_InitialIze()
    { 

        // enemy 초기 위치
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        xRan = x;
        yRan = y;

        isFlipped = false;
    }

    // 플레이어 추적
    // 1. 범위안에 들어왔을 때 searching 후 tracking -> fp : enemy에서 검사한 playerPosi
    // 2. 그냥 무조건 tracking -> fp : 하위에서 update문에 있는 justTrackingPlayerPosi
    protected void Tracking(Transform fp)
    {
        if (fp == null)
            return;

        if (knockBackState) // 넉백 상태
            return;

        transform.position = Vector3.MoveTowards(transform.position, fp.transform.position, moveSpeed * Time.deltaTime);
    }


    //랜덤 움직임
    
    protected void Prwol()
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, moveSpeed * Time.deltaTime);
    }

    //랜덤 움직임 검사
    protected IEnumerator checkPosi(float randRange)
    {        
        while (true)
        {
            yield return new WaitForSeconds(fTime);
            //Debug.Log("top_fly : checkPosi실행");
            // x위치는 현재 위치 randRange부터 , 현재위치 -randRange까지
            // y위치 동일
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }

    // 몬스터 플립
    public void Lookplayer() 
    {
        if (transform.position.x > playerPos.position.x && isFlipped) 
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPos.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    
}
