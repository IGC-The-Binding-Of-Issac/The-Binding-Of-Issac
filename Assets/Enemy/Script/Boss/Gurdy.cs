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
    [Header("BigAttackFly")]
    [SerializeField] GameObject bigShootBullet;
    [SerializeField] GameObject rightPosition;
    [SerializeField] Animator childAni;
    public Transform[] Children;

    [SerializeField] float  stateTime;            
    [SerializeField] int    stateNum;
    [SerializeField] float  currTime;                // 현재 상태의 시간
    [SerializeField] bool chageState;               // 상태변환
    bool coruState;
    Coroutine runningCoroutine = null;

    void Start()
    {
        // 나포함 , 자식 배열 받아오기
        // index0 : 본인
        // index1 : 머리
        // index2 : right
        // index3 : down
        // index4 : left
        // index5 : top

        Children = gameObject.GetComponentsInChildren<Transform>();
        animator = GetComponent<Animator>();
        childAni = Children[1].gameObject.GetComponent<Animator>();

        playerInRoom        = false;
        dieParameter        = "isBigFlyDie";

        // Enemy
        hp                  = 100f;
        waitforSecond       = 0.4f;
        attaackSpeed        = 1.5f; // 총알 발사 하는 시간 
        bulletSpeed         = 5f;

        maxhp               = hp;

        //Gurdy
        randTime();

        currTime = stateTime;
        chageState = true;
        coruState = true;
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
        Debug.Log("거디 총쏨");


        int rand = Random.Range(2 , 6); // 2~5

        if (coruState)
        {
            runningCoroutine = StartCoroutine(ShootBullets(Children[rand].gameObject));
            coruState = false;
        }


    }

    IEnumerator ShootBullets(GameObject _obj)
    {
        float randGravityScale;


        while (true)
        {
            float randWait = Random.Range(0.2f, 0.5f);
            bool isbullet = true;
            if (isbullet)
            {

                GameObject bu = Instantiate(bigShootBullet, _obj.transform.position, transform.rotation);
                bu.GetComponent<Rigidbody2D>().velocity = _obj.transform.right * bulletSpeed;

                randGravityScale = Random.Range(0f, 1f);
                bu.GetComponent<Rigidbody2D>().gravityScale += randGravityScale;
                isbullet = false;
            }
            yield return new WaitForSeconds(randWait);
        }
    }

    void gurdyGeneFly() 
    {
        Debug.Log("거디 생성");

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
