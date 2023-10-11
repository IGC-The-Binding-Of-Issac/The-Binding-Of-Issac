using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : Enemy
{
    [SerializeField] protected float x;
    [SerializeField] protected float y;

    [SerializeField] float hiveMoveToX;
    [SerializeField] float hiveMoveToY;

    void Start()
    {
        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 10f;
        sight = 3f;
        moveSpeed = 5f;
        waitforSecond = 0.5f;

    }

    void Update()
    {
        if (playerInRoom)
            Move();
    }

    public override void Move()
    {

        //플레이어가 범위 안에 들어오면
        

    }

    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
