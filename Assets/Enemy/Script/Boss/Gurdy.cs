using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Gurdy : TEnemy
{
    /// <summary>
    /// 
    /// 1. 가만히 있음
    /// 2. 좌우에서 유도탄 쏨
    /// 3. 앞에서 4방향 총알 쏨
    /// 4. 앞에 pooter 두마리 소환
    /// 
    /// </summary>
    /// 
    [Header("TEnemy")]
    [SerializeField] GameObject bigShootBullet;
    [SerializeField] GameObject pooter;
    [SerializeField] Animator childAni;
    public Transform[] Children;

    [SerializeField] float  stateTime;
    [SerializeField] int    stateNum;
    [SerializeField] float  currTime;                // 현재 상태의 시간
    bool coruState;
    Coroutine runningCoroutine = null;
    bool isGene;

    void Start()
    {
        // 나포함 , 자식 배열 받아오기
        // index0 : 본인
        // index1 : 머리
        // index2 : right
        // index3 : down
        // index4 : left
        // index5 : top
        // index6 : left top
        // index7 : genePosi

        Children = gameObject.GetComponentsInChildren<Transform>();
        animator = GetComponent<Animator>();
        childAni = Children[1].gameObject.GetComponent<Animator>();

        playerInRoom        = false;
        dieParameter        = "isDie";

        // Enemy
        hp                  = 400f;
        waitforSecond       = 1f;   // 죽기전 시간
        attaackSpeed        = 1.5f; // 총알 발사 하는 시간 
        bulletSpeed         = 5f;

        maxhp               = hp;

        //Gurdy
        randTime();

        currTime = stateTime;
        coruState = true;
        isGene = true;
    }

    private void Update()
    {
        if (playerInRoom) 
        {
            Move();
        }
    }

    void Move() 
    {
        if (e_isDead())
        {
            childAni.SetBool("isDie" , true);       // 죽기전 머리 애니메이션
            animator.SetBool("isBeforeDie" , true); // 죽기전 몸통 애니메이션

            e_destroyEnemy();
        }


        currTime -= Time.deltaTime;
        if (currTime > 0)
        {

        }
        else if (currTime <= 0) 
        {
            if (runningCoroutine != null) //실행중인 코루틴이 있으면
            {
                // 멈추기
                StopCoroutine(runningCoroutine);
                childAni.SetBool("isOpps", false);
                childAni.SetBool("isdisapear", false);
                childAni.SetBool("isShoot", false);
                childAni.SetBool("isHi", false);
                isGene = true;
            }


            // 초기화
            randNum();
            randTime();
            currTime = stateTime;

            if (stateNum == 1)
                gurdyShoot();
            else if (stateNum == 2)
                gurdyGeneFly();

            coruState = true;
        }

    }

    void gurdyShoot() 
    {
        if (coruState)
        {

            childAni.SetBool("isOpps", true);
            childAni.SetBool("isdisapear", true);
            runningCoroutine = StartCoroutine(ShootBullets());
            coruState = false;
        }
    }

    IEnumerator ShootBullets()
    {
        float randGravityScale;
        childAni.SetBool("isShoot", true);

        while (true)
        {
            int rand = Random.Range(2, 7);                  // 랜덤방향 (2~6)
            GameObject _obj = Children[rand].gameObject;    // 랜덤 방향에 따른 자식 오브젝트

            float randWait = Random.Range(0.03f, 0.1f);      // 랜덤 총알발사 사이의 시간
            bool isbullet = true;
            if (isbullet)
            {

                GameObject bulletobj = EnemyPooling.Instance.GetStraightBullet(_obj);
                
                bulletobj.GetComponent<Rigidbody2D>().velocity = _obj.transform.right * bulletSpeed;

                randGravityScale = Random.Range(0f, 1f);
                bulletobj.GetComponent<Rigidbody2D>().gravityScale += randGravityScale;
                isbullet = false;
            }
            yield return new WaitForSeconds(randWait);
        }

    }

    void gurdyGeneFly() 
    {
        childAni.SetBool("isappear", true);
        childAni.SetBool("isHi", true) ;

        if (isGene)
        { 
            GenerateAttackFly();
            isGene = false;
        }

    }

    void GenerateAttackFly()
    {
        GameObject obj = Instantiate(pooter, Children[7].transform.position, Quaternion.identity) as GameObject;

        // SoundManage의 sfxObject로 추가.
        if (obj.GetComponent<AudioSource>() != null)
        {
            SoundManager.instance.sfxDestoryObjects.Add(obj.GetComponent<AudioSource>());
            obj.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
        }

        roomInfo.GetComponent<Room>().enemis.Add(obj);
    }

    void randTime()
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(1f, 3f);
    }

    void randNum()
    {
        stateNum = Random.Range(1, 3); // 1~2중
    }
}
