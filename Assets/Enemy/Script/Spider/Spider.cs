using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Top_Spider
{
    // Start is called before the first frame update
    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //최상위 enemy
        hp = 20f;
        sight = 3f;
        moveSpeed = 10f;
        waitforSecond = 0.5f;
        isInRange = false;

        //상위 Top_Spider
        randRange = 5f;
        fTime = 0.5f;
    }

    void Update()
    {
        if (playerInRoom)
            Move();
    }

    public override void Move()
    {

    }

    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
