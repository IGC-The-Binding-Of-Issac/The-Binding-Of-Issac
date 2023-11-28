
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
    Vector2 shotInput;
    public float tearY;

    [Header("PlayerState")]
    public GameObject HeadItem;
    public GameObject CheckedObject;
    public GameObject tear;
    public GameObject putBomb;
    public GameObject DrBomb;
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
    GameObject tearPoolChild;
    GameObject DrBombChild;
    GameObject putBombChild;
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
        ShotAnim();
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
            GameObject DrbombObj = Instantiate(DrBomb, bombPointTransform.position, Quaternion.identity);
            DrBombPool.Push(DrbombObj);
            DrbombObj.transform.SetParent(DrBombPointTransform.transform);
            DrbombObj.gameObject.SetActive(false);
        }
        DrBombChild = DrBombPool.Pop();
    }
    public GameObject GetDrBombPooling()
    {
        if (DrBombPool.Count == 0)
        {
            GameObject DrbombObj = Instantiate(DrBombChild, bombPointTransform.position, Quaternion.identity);
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
            GameObject bombObj = Instantiate(putBomb, bombPointTransform.position, Quaternion.identity);
            putBombPool.Push(bombObj);
            bombObj.transform.SetParent(bombPointTransform.transform);
            bombObj.gameObject.SetActive(false);
        }
        putBombChild = putBombPool.Pop();
    }
    public GameObject GetBombPooling()
    {
        if (putBombPool.Count == 0)
        {
            GameObject bombObj = Instantiate(putBombChild, bombPointTransform.position, Quaternion.identity);
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
        bombObj.GetComponent<BoxCollider2D>().isTrigger = true;
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
            SetSFXObject(tearObj);
            tearObj.transform.SetParent(tearPoint.transform);
            tearObj.gameObject.SetActive(false);
        }
        tearPoolChild = tearPool.Pop();
    }
    public GameObject GetTearPooling()
    {
        if (tearPool.Count == 0)
        {
            GameObject tearObj = Instantiate(tearPoolChild, tearPointTransform.position, Quaternion.identity);
            tearPool.Push(tearObj);
            SetSFXObject(tearObj);
            tearObj.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
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
        float shootHor = Input.GetAxis("ShootHorizontal");
        //세로 발사 키 입력
        float shootVer = Input.GetAxis("ShootVertical");

        shotInput = shootHor * Vector2.right + shootVer * Vector2.up;
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
                Shoot(shootHor, shootVer);
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
        if (ItemManager.instance.PassiveItems[9] && !ItemManager.instance.PassiveItems[16])
        {
            if (x > 0) //오른쪽
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (x < 0) //왼쪽
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(180, 0, 90);
            }
            if (y > 0) //위
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (y < 0) //아래
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        CheckedObject = null;
        if (y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //눈물이 이동속도의 영향을 받음
        //해당 이동키를 누르면 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //해당 방향으로 힘을 줌
            rigid_bullet.AddForce(moveInput * 2.5f, ForceMode2D.Impulse);
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

        //16번 패시브 아이템 먹으면
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
        if (ItemManager.instance.PassiveItems[9] && !ItemManager.instance.PassiveItems[16])
        {
            if (x > 0) //오른쪽
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (x < 0) //왼쪽
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(180, 0, 90);
            }
            if (y > 0) //위
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (y < 0) //아래
            {
                DefaultTearObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        CheckedObject = null;
        if (y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //눈물이 이동속도의 영향을 받음
        //해당 이동키를 누르면 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //해당 방향으로 힘을 줌
            rigid_bullet.AddForce(moveInput * 2.5f, ForceMode2D.Impulse);
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
        if (moveInput.x < 0) { bodyRenderer.flipX = true; headRenderer.flipX = true; }
        //가로입력방향 -> 1 몸통 애니메이션 그대로(오른쪽)
        if (moveInput.x > 0) { bodyRenderer.flipX = false; headRenderer.flipX = false; }
        //세로입력 절댓값
        playerMoveAnim.SetFloat("Up&Down", Mathf.Abs(moveInput.y));
        //가로입력 절댓값
        playerMoveAnim.SetFloat("Left&Right", Mathf.Abs(moveInput.x));
        playerShotAnim.SetFloat("Left&RightLook", Mathf.Abs(moveInput.x));

        if (moveInput.y > 0)
        {
            playerShotAnim.SetBool("UpLook", true); // 위쪽 방향
        }
        else
        {
            playerShotAnim.SetBool("UpLook", false);
        }
        if (moveInput.y < 0)
        {
            playerShotAnim.SetBool("DownLook", true); // 아래쪽 방향
        }
        else
        {
            playerShotAnim.SetBool("DownLook", false);
        }
    }

    //발사 애니메이션
    void ShotAnim()
    {
        if (shotInput.x < 0) { headRenderer.flipX = true; }
        if (shotInput.x > 0) { headRenderer.flipX = false; }
        playerShotAnim.SetFloat("Left&RightShot", Mathf.Abs(shotInput.x));

        if (shotInput.y > 0)
        {
            playerShotAnim.SetBool("UpShot", true); // 위쪽 방향
        }
        else
        {
            playerShotAnim.SetBool("UpShot", false);
        }
        if (shotInput.y < 0)
        {
            playerShotAnim.SetBool("DownShot", true); // 아래쪽 방향
        }
        else
        {
            playerShotAnim.SetBool("DownShot", false);
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

    void SetSFXObject(GameObject obj)
    {
        if(obj.GetComponent<AudioSource>() != null)
        {
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());
        }
    }
    #endregion
}