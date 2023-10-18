using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : Top_IssacMonster
{
    bool paberisFlipped;
    void Start()
    {
        Issac_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 5f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        // Top_IssacMonster
        randRange = 3f;
        fTime = 2f;
        StartCoroutine(checkPosi(randRange));

        //pacer
        paberisFlipped = false;
    }

    void Update()
    {
        if (playerInRoom)
        { 
            justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
            Move();
        }
    }

    public override void Move()
    {
        Prwol();
    }

    //플레이어 위치에 따라 그쪽을 보는 애니메이션
    /*
    public void PaverTurn()
    {
        if (transform.position.x > justTrackingPlayerPosi.position.x && paberisFlipped)
        {
            animator.SetBool("isRight" , true);
            paberisFlipped = false;
        }
        else if (transform.position.x < justTrackingPlayerPosi.position.x && !paberisFlipped)
        {
            animator.SetBool("isRight", false);
            paberisFlipped = true;
        }
    }
    */
}
