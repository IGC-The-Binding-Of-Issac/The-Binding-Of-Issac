
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    public Animator playerMoveAnim;
    public Animator playerShotAnim;
    public Animator playerAnim;
    public Animator getItem;

    [Header("Transform")]
    public Transform itemPosition;
    public Transform head;
    public Transform body;
    public Transform headClothes;
    public Transform bodyClothes;
    public Transform useActiveItemImage;
    public Transform knifePosition;
    public Transform familiarPosition;
    public Transform tearPointTransform;
    public Transform bombPointTransform;
    public Transform DrBombPointTransform;

    [Header("Sprite")]
    SpriteRenderer bodyRenderer;
    SpriteRenderer headRenderer;
    SpriteRenderer headItem;
    Rigidbody2D playerRB;
    public Sprite tearDefaultSprite;
    public Sprite bombDefaultSprite;

    [Header("Function")]
    private float lastshot;
    Vector2 moveInput;
    float shootHor;
    float shootVer;
    public float tearY;

    [Header("PlayerState")]
    public GameObject HeadItem;
    public GameObject CheckedObject;
    public GameObject tear;
    public GameObject bomb;
    public TearPoint tearPoint;
    GameObject DefaultTearObject;

    [Header("ItemState")]
    public GameObject knife;
    public bool bombState;
    public bool nailActivated; // 대못 아이템을 사용했을 때
    public bool canUseActive = true; //액티브 아이템 개갈김을 방지하기 위한
    public bool canChangeItem = false; //액티브 아이템 변경 과부하를 위한

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] hitClips;
    public AudioClip[] deadClips;
    public AudioClip getItemClip;
    public AudioClip ShootClip;

    Stack<GameObject> tearPool;
    Stack<GameObject> putBombPool;
    Stack<GameObject> DrBombPool;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        headItem = GameManager.instance.playerObject.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>();

        canUseActive = true; // 액티브 아이템 개갈김을 방지하기 위한
        canChangeItem = true; // 액티브 아이템 변경 과부하를 위한
        nailActivated = false;
        tearPool = new Stack<GameObject>();
        putBombPool = new Stack<GameObject>();
        DrBombPool = new Stack<GameObject>();
        SetTearPooling();
        SetBombPooling();
        SetDrBombPooling();
        //knifePosition.gameObject.SetActive(false);
    }

    void Update()
    {
        MoveAnim();
        //ShotAnim();
        InstallBomb();
        UseActive();
    }
    void FixedUpdate()
    {
        Movement();
    }
    #region Dr_BombPooling

    public void SetDrBombPooling()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject DrbombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            DrBombPool.Push(DrbombObj);
            DrbombObj.transform.SetParent(DrBombPointTransform.transform);
            DrbombObj.gameObject.SetActive(false);
        }
    }
    public GameObject GetDrBombPooling()
    {
        if (DrBombPool.Count == 0)
        {
            GameObject DrbombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            DrBombPool.Push(DrbombObj);
            DrbombObj.transform.SetParent(DrBombPointTransform.transform);
            DrbombObj.gameObject.SetActive(false);
        }
        GameObject DrbombObject = DrBombPool.Pop();
        DrbombObject.SetActive(true);
        return DrbombObject;
    }
    public void ReturnDrBombPooling(GameObject bombObj)
    {
        bombObj.GetComponent<SpriteRenderer>().sprite = bombDefaultSprite;
        bombObj.transform.localPosition = Vector3.zero;
        //bombObj.transform.localScale = Vector3.one;
        bombObj.SetActive(false);
        bombObj.GetComponent<PutBomb>().CanAttack = false;
        bombObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.04f, -0.03f);
        bombObj.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.64f);
        DrBombPool.Push(bombObj);
    }
    #endregion

    #region putBombPooling

    public void SetBombPooling()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject bombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            putBombPool.Push(bombObj);
            bombObj.transform.SetParent(bombPointTransform.transform);
            bombObj.gameObject.SetActive(false);
        }
    }
    public GameObject GetBombPooling()
    {
        if (putBombPool.Count == 0)
        {
            GameObject bombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            putBombPool.Push(bombObj);
            bombObj.transform.SetParent(bombPointTransform.transform);
            bombObj.gameObject.SetActive(false);
        }
        GameObject bombObject = putBombPool.Pop();
        bombObject.SetActive(true);
        return bombObject;
    }
    public void ReturnBombPooling(GameObject bombObj)
    {
        bombObj.GetComponent<SpriteRenderer>().sprite = bombDefaultSprite;
        bombObj.transform.localPosition = Vector3.zero;
        bombObj.SetActive(false);
        bombObj.GetComponent<PutBomb>().CanAttack = false;
        bombObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.04f, -0.03f);
        bombObj.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.64f);
        putBombPool.Push(bombObj);
    }
    #endregion

    #region tearPooling
    public void SetTearPooling()
    {
        for (int i = 0; i < 40; i++)
        {
            GameObject tearObj = Instantiate(tear, tearPointTransform.position, Quaternion.identity);
            tearPool.Push(tearObj);
            tearObj.transform.SetParent(tearPoint.transform);
            tearObj.gameObject.SetActive(false);
        }
    }
    public GameObject GetTearPooling()
    {
        if (tearPool.Count == 0)
        {
            GameObject tearObj = Instantiate(tear, tearPointTransform.position, Quaternion.identity);
            tearPool.Push(tearObj);
            tearObj.transform.SetParent(tearPoint.transform);
            tearObj.gameObject.SetActive(false);
        }
        GameObject tearObject = tearPool.Pop();
        tearObject.SetActive(true);
        tearObject.GetComponent<Tear>().SetPlayerPosition(transform.position);
        return tearObject;
    }

    public void ReturnTearPooling(GameObject bullet)
    {
        bullet.GetComponent<SpriteRenderer>().sprite = tearDefaultSprite;
        //bullet.transform.localScale = new Vector3(1, 1, 1);
        bullet.transform.localPosition = Vector3.zero;
        bullet.GetComponent<CircleCollider2D>().enabled = true;
        bullet.GetComponent<SpriteRenderer>().sortingOrder = 103;
        bullet.SetActive(false);
        tearPool.Push(bullet);
    }
    #endregion

    #region UseActiveItem
    //액티브 아이템 사용
    void UseActive()
    {
        // 아이템이 있고, 스페이스바 눌렀을때
        if (ItemManager.instance.ActiveItem != null && canUseActive)
        {
            if (ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().canUse)
            {
                ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().CheckedItem();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ActiveInfo active = ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>();

                    if (active.currentEnergy >= active.needEnergy) // 필요 에너지 넘었을때.
                    {
                        if (active.activeItemCode == 1 && ItemManager.instance.coinCount <= 0)
                        {
                            return;
                        }
                        else
                        {
                            StartCoroutine(UseActiveItem()); // 아이템 사용 애니메이션
                            active.UseActive();  // 아이템 기능 실행
                            if (active.activeItemCode == 0)
                            {
                                nailActivated = true;
                            }
                        }
                        active.currentEnergy = 0;
                        UIManager.instance.UpdateActiveEnergy();
                        canUseActive = false;
                        Invoke("SetActiveDelay", 1f);
                        Invoke("SetCanChangeItem", 1f);
                    }
                }
            }
        }
    }

    void SetActiveDelay()
    {
        canUseActive = true;
    }
    
    void SetCanChangeItem()
    {
        canChangeItem = true;
    }
    #endregion

    #region PlayerFunction
    //이동 기능
    void Movement()
    {
        //이동속도
        float moveSpeed = PlayerManager.instance.playerMoveSpeed;
        //발사 딜레이
        float shotDelay = PlayerManager.instance.playerShotDelay;

        //가로 이동 키입력
        float hori = Input.GetAxis("Horizontal");
        //세로 이동 키입력
        float verti = Input.GetAxis("Vertical");
        //입력했을 때 이동 방향 계산식
        moveInput = hori * Vector2.right + verti * Vector2.up;
        //대각 이동속도 1 넘기지 않기
        if(moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }
        //가로 발사 키 입력
        shootHor = Input.GetAxis("ShootHorizontal");
        //세로 발사 키 입력
        shootVer = Input.GetAxis("ShootVertical");

        if (ItemManager.instance.PassiveItems[13] && !ItemManager.instance.PassiveItems[16])
        {
            KnifeAttack(hori,verti,shootHor, shootVer);
        }
        else
        {
            //총알 발사 딜레이 마지막 발사와 딜레이를 합쳐 현재시간(초단위)을 넘어서면 실행
            if ((shootHor != 0 || shootVer != 0) && Time.time > lastshot + shotDelay)
            {
                //가로나 세로 입력이 같이 될 때
                if (shootHor != 0 && shootVer != 0)
                {
                    //대각 발사 X
                    shootHor = 0;
                }
                //총알이 포물선을 그리는 아이템
                if (ItemManager.instance.PassiveItems[9])
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        //우측키를 눌렀을 때 대각 위 55도로 힘을 주는 코드
                        Shoot(Mathf.Cos(55 * Mathf.Deg2Rad), Mathf.Sin(55 * Mathf.Deg2Rad));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        //좌측키를 눌렀을 때 대각 위 120도로 힘을 주는 코드
                        Shoot(Mathf.Cos(120 * Mathf.Deg2Rad), Mathf.Sin(120 * Mathf.Deg2Rad));
                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        Shoot(shootHor, shootVer);
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        Shoot(shootHor, shootVer);
                    }
                }
                else
                {
                    Shoot(shootHor, shootVer);
                }
                //마지막 발사에 현재 시간(초단위)을 넣음
                lastshot = Time.time;
            }
        }
        //플레이어 움직임
        playerRB.velocity = moveInput * moveSpeed;
    }
    //발사 기능
    public void Shoot(float x, float y)
    {
        ShotAnim();
        //눈물 Y값
        tearY = y;
        //눈물 발사 속도
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        //눈물 생성 지점
        Vector3 firePoint = tearPoint.transform.position;

        if (ItemManager.instance.PassiveItems[16])
        {
            DefaultTearObject = DrFetusBomb();
            bombState = true;
        }
        else
        {
            DefaultTearObject = GetTearPooling();
        }

        DefaultTearObject.transform.position = firePoint;
        //생성된 눈물에 눈물속도 곱해서 힘주기
        DefaultTearObject.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);

        //9번 패시브 아이템을 먹으면
        if (ItemManager.instance.PassiveItems[9])
        {
            if(tearY != -1)
                //눈물 중력 증가
                DefaultTearObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        else
            DefaultTearObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        CheckedObject = null;
        if (y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //눈물이 이동속도의 영향을 받음
        //해당 이동키를 누르면 
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //해당 방향으로 힘을 줌
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
        //2번 패시브 아이템을 먹으면
        if (ItemManager.instance.PassiveItems[2])
        {
            //해당 함수 4번 실행
            for (int i = 0; i < 3; i++)
                MutantShoot(x, y);
        }
    }
    public void MutantShoot(float x, float y)
        {
        //눈물 발사 속도
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        //눈물 생성 지점
        Vector3 firePoint = tearPoint.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        if (ItemManager.instance.PassiveItems[16])
        {
            DefaultTearObject = DrFetusBomb();
            bombState = true;
        }
        else
        {
            DefaultTearObject = GetTearPooling();
        }

        DefaultTearObject.transform.position = firePoint;
        //생성된 눈물에 눈물속도 곱해서 힘주기
        DefaultTearObject.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);

        //9번 패시브 아이템을 먹으면
        if (ItemManager.instance.PassiveItems[9])
        {
            //눈물 중력 증가
            DefaultTearObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        else
            DefaultTearObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        CheckedObject = null;
        if (y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //눈물이 이동속도의 영향을 받음
        //해당 이동키를 누르면 
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //해당 방향으로 힘을 줌
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
    }

    //눈물 애니메이션 클립 이벤트로 마지막에 추가 되어있음

    public void KnifeAttack(float moveX, float moveY, float shootX, float shootY)
    {
        if(knife.GetComponent<KnifeObject>().canShoot)
        {
            if (shootX > 0) //오른쪽
            {
                knifePosition.localPosition = new Vector3(0.94f, 0.2f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (shootX < 0) //왼쪽
            {
                knifePosition.localPosition = new Vector3(-0.84f, 0.2f, 0);
                knife.transform.rotation = Quaternion.Euler(180, 0, 90);
            }
            if (shootY > 0) //위
            {
                knifePosition.localPosition = new Vector3(0, 1.31f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (shootY < 0) //아래
            {
                knifePosition.localPosition = new Vector3(0, -0.69f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }
    //폭탄 설치 기능
    void InstallBomb()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // 필드에 설치된 폭탄이없을때 && 보유중인 폭탄 개수가 1개 이상일때
            if(GameObject.Find("Putbomb") == null && ItemManager.instance.bombCount > 0)
            {
                GameObject bomb = GetBombPooling();
                bomb.GetComponent<PutBomb>().PlayerBomb();
                ItemManager.instance.bombCount--;
                bombState = false;
                bomb.name = "Putbomb"; // 생성된 폭탄 오브젝트 이름 변경
            }
        }
    }

    public GameObject DrFetusBomb()
    {
        GameObject bomb = GetDrBombPooling();
        bomb.GetComponent<PutBomb>().PlayerBomb();
        return bomb;
    }
    #endregion

    #region PlayerAnim
    //이동 애니메이션
    void MoveAnim()
    {
        //가로입력방향 -> -1 몸통 애니메이션 뒤집기(왼쪽)
        if (moveInput.x < 0) { bodyRenderer.flipX = true; }
        //가로입력방향 -> 1 몸통 애니메이션 그대로(오른쪽)
        if (moveInput.x > 0) { bodyRenderer.flipX = false; }
        //세로입력 절댓값
        playerMoveAnim.SetFloat("Up&Down", Mathf.Abs(moveInput.y));
        //가로입력 절댓값
        playerMoveAnim.SetFloat("Left&Right", Mathf.Abs(moveInput.x));

        //위로가는 방향키를 눌렀을 때
        if (Input.GetKey(KeyCode.W))
        {
            //위로 보는 애니메이션 실행
            playerShotAnim.SetBool("UpLook", true);
            //다른 발사 키를 누르면
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //애니메이션 종료
                playerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("UpLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("UpLook", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerShotAnim.SetBool("DownLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("DownLook", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerShotAnim.SetBool("LeftLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("LeftLook", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerShotAnim.SetBool("RightLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("RightLook", false);
        }
    }

    //발사 애니메이션
    void ShotAnim()
    {
        //각 발사 방향키를 누르면
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //해당 방향 애니메이션 실행
            playerShotAnim.SetBool("playerLeftShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerLeftShot", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerShotAnim.SetBool("playerRightShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerRightShot", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerShotAnim.SetBool("playerUpShot", true);
            //위로 쏠 떄 눈물 레이어 낮게 바뀜
            Transform allChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
            for (int i = 0; i < allChildren.childCount; i++)
            {
                GameObject obj = allChildren.GetChild(i).gameObject;
                obj.GetComponent<SpriteRenderer>().sortingOrder = 101;
            }
        }
        else
        {
            playerShotAnim.SetBool("playerUpShot", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerShotAnim.SetBool("playerDownShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerDownShot", false);
        }
    }

    //피격 애니메이션
    public void Hit()
    {
        //머리 투명
        headRenderer.color = new Color(1, 1, 1, 0);
        //몸통 투명
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //머리에 씌워진 아이템 투명
        headItem.color = new Color(1, 1, 1, 0);
        //피격 애니메이션 실행
        playerAnim.SetTrigger("Hit");
        //피격 사운드 실행
        HitSound();
    }

    // 사망 애니메이션
    public void Dead()
    {
        //사망 사운드 실행
        DeadSound();

        //player head, player body 오브젝트 찾아서 끄기
        head.gameObject.SetActive(false);
        body.gameObject.SetActive(false);
        //사망 애니메이션 실행
        playerAnim.SetTrigger("Death");
    }
    #endregion

    #region GetItemAnim
    //아이템 획득 애니메이션
    public IEnumerator GetItemTime()
    {
        //원래 head, body,headitem 모습은 가려둠
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);
        //아이템 획득 애니메이션 실행
        getItem.SetTrigger("GetItem");
        //애니메이션 1초간 유지
        yield return new WaitForSeconds(1f);
        //플레이어 모습 보이게 함
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);

        //itemPosition 자식이 생기고
        if (itemPosition.childCount != 0)
        {
            Destroy(itemPosition.GetChild(0).gameObject);
        }
    }

    public IEnumerator GetTrinketItem()
    {
        GetitemSound();
        //원래 모습은 가려둠
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);
        //아이템 획득 애니메이션 실행
        getItem.SetTrigger("GetItem");
        //애니메이션 1초간 유지
        yield return new WaitForSeconds(1f);
        //플레이어 모습 보이게 함
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().KeepItem();
    }

    public IEnumerator GetActiveItem()
    {
        GetitemSound();
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().KeepItem();
    }

    public IEnumerator UseActiveItem()
    {
        Sprite activeSpr = ItemManager.instance.ActiveItem.GetComponent<SpriteRenderer>().sprite;
        useActiveItemImage.GetComponent<SpriteRenderer>().sprite = activeSpr;
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        useActiveItemImage.GetComponent<SpriteRenderer>().sprite = null;
    }
    #endregion

    #region Sound
    public void HitSound()
    {
        int randomIndex = Random.Range(0, hitClips.Length);

        audioSource.clip = hitClips[randomIndex];
        audioSource.Play();
    }
    public void DeadSound()
    {
        int randomIndex = Random.Range(0, deadClips.Length);

        audioSource.clip = deadClips[randomIndex];
        audioSource.Play();
    }

    public void GetitemSound()
    {
        audioSource.clip = getItemClip;
        audioSource.Play();
    }
    #endregion
}