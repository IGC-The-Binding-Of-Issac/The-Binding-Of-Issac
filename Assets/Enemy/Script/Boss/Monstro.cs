using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monstro : TEnemy
    {/// <summary>
     /// 1. ������ ����
     /// 2. ������
     /// 3. ���������� �ѹ��� �߻���
     /// </summary>
     /// 
    [Header("TEnemy")]
    [SerializeField] Animator MonstroAni;
    [SerializeField] GameObject bulletPosition;

    [SerializeField] float stateTime;
    [SerializeField] int stateNum;
    [SerializeField] float currTime;                // ���� ������ �ð�
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isFlipped = true;
        playerInRoom = false;
        dieParameter = "isDie";
        // Enemy
        hp = 100f;
        waitforSecond = 0.5f;   // �ױ��� �ð�
        attaackSpeed = 1.5f; // �Ѿ� �߻� �ϴ� �ð� 
        bulletSpeed = 5f;

        maxhp = hp;

        //Monstro
        randTime();
        currTime = stateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRoom)
        {
            Move();
        }

        e_Lookplayer();

    }
    void Move()
    {
        if (e_isDead())
        {
            e_destroyEnemy();
        }
        e_findPlayer();

        currTime -= Time.deltaTime;
        if (currTime > 0)
        {

        }
        else if (currTime <= 0)
        {

            // �ʱ�ȭ
            randNum();
            randTime();
            currTime = stateTime;

            if (stateNum == 1)
                MonstroSpit();
            else if (stateNum == 2)
                MonstroJump();

        }

    }
    void MonstroSpit()
    {
        MonstroAni.SetTrigger("SpitTrigger");
        for (int i = 0; i < 10; i++)
        {
            float randGravityScale;
            float randBulletSpeed;
            float randBulletSpeedY;
            randGravityScale = Random.Range(0f, 1f);
            randBulletSpeed = Random.Range(3f, 5f);
            randBulletSpeedY = Random.Range(2f, 4f);
            GameObject bulletobj = EnemyPooling.Instance.GetStraightBullet(bulletPosition); // Ǯ�� �Ѿ� ������Ʈ ��������
            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletobj.GetComponent<Rigidbody2D>().velocity = new Vector3(-randBulletSpeed, randBulletSpeedY, 0);
            }
            else
            {
                bulletobj.GetComponent<Rigidbody2D>().velocity = new Vector3(randBulletSpeed, randBulletSpeedY, 0);
            }
            bulletobj.GetComponent<Rigidbody2D>().gravityScale += randGravityScale;
        }
    }
    void MonstroJump()
    {
        float jumpPower = 3f;
        int numOfJump = 1;
        float duration = 1.5f;
        MonstroAni.SetTrigger("JumpTrigger");
        transform.DOJump(playerPosi.position, jumpPower, numOfJump, duration);
    }
    public void OffCrash()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void OnCrash()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    void randTime()
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(1f, 3f);
    }

    void randNum()
    {
        stateNum = Random.Range(1, 3); // 1~2��
    }
}