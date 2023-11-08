using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    public Transform body;
    public Transform head;
    public Transform useActiveItemImage;

    [Header("Sprite")]
    public Sprite defaultTearImg;
    SpriteRenderer bodyRenderer;
    SpriteRenderer headRenderer;
    SpriteRenderer headItem;
    Rigidbody2D playerRB;

    [Header("Function")]
    private float lastshot;
    Vector2 moveInput;

    float shootHor;
    float shootVer;
    public GameObject tear;

    [Header("Unity Setup")]
    public TearPoint tearPoint;

    [Header("State")]
    public GameObject CheckedObject;
    public bool nailActivated; // 대못 아이템을 사용했을 때
    public bool canUseActive = true; //액티브 아이템 개갈김을 방지하기 위한
    public bool canChangeItem = false; //액티브 아이템 변경 과부하를 위한

    
    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] hitClips;
    public AudioClip[] deadClips;
    public AudioClip getItemClip;
    public AudioClip useItemClip;
    public AudioClip ShootClip;

    [Header("equipment")]
    public GameObject HeadItem;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        headItem = GameManager.instance.playerObject.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>();
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = defaultTearImg;

        canUseActive = true; // 액티브 아이템 개갈김을 방지하기 위한
        canChangeItem = true; // 액티브 아이템 변경 과부하를 위한
        nailActivated = false;
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
                        if (active.activeItemCode == 1 && ItemManager.instance.coinCount <= 0) return;
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

    //이동 기능
    void Movement()
    {
        float moveSpeed = PlayerManager.instance.playerMoveSpeed;
        float shotDelay = PlayerManager.instance.playerShotDelay;

        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        moveInput = hori * Vector2.right + verti * Vector2.up;
        //대각 이동속도 1 넘기지 않기
        if(moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }
        shootHor = Input.GetAxis("ShootHorizontal");
        shootVer = Input.GetAxis("ShootVertical");

        //총알 발사 딜레이
        if ((shootHor != 0 || shootVer != 0) && Time.time > lastshot + shotDelay)
        {
            if (shootHor != 0 && shootVer != 0)
            {
                //대각 발사 X
                shootHor = 0;
            }
            Shoot(shootHor, shootVer);
            lastshot = Time.time;
        }

        //플레이어 움직임
        playerRB.velocity = moveInput * moveSpeed;
    }

//발사 기능
public void Shoot(float x, float y)
    {
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        Vector3 firePoint = tearPoint.transform.position;
        //발사 기능 구현
        //게임 중 눈물 생성 눈물 프리펩, 발사 시작위치, 회전
        ShootSound();

        //칼 먹었을 때
        if (ItemManager.instance.PassiveItems[13] && !ItemManager.instance.PassiveItems[16])
        {
            Vector3 knifeRotate = new Vector3(0, 0, 0);
            if (x == -1) knifeRotate = new Vector3(180, 0, 90);
            else if (x == 1) knifeRotate = new Vector3(0, 0, 270);
            else if (y == 1) knifeRotate = new Vector3(0, 180, 0);
            else if (y == -1) knifeRotate = new Vector3(0, 0, 180);
            tear = Instantiate(PlayerManager.instance.tearObj, firePoint, Quaternion.Euler(knifeRotate.x, knifeRotate.y, knifeRotate.z)) as GameObject;
        }
        //나머지
        else
        {
        tear = Instantiate(PlayerManager.instance.tearObj, firePoint, transform.rotation) as GameObject;
        } 
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);
        CheckedObject = null;
        
        if(y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //총알이 대각으로 밀려서 발사되게 옆으로 힘을 줌
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
            //위로 발사할 땐 눈물 레이어 높이기
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
        if (ItemManager.instance.PassiveItems[2])
        {
            for (int i = 0; i < 3; i++)
                MutantShoot(x, y);
        }
    }
    public void MutantShoot(float x, float y)
    {
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        Vector3 firePoint = tearPoint.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        //발사 기능 구현
        //게임 중 눈물 생성 눈물 프리펩, 발사 시작위치, 회전
        tear = Instantiate(PlayerManager.instance.tearObj, firePoint, transform.rotation) as GameObject;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);


        CheckedObject = null;
        if (y != 1) // 위 공격이 아닐때
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //총알이 대각으로 밀려서 발사되게 옆으로 힘을 줌
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
            //위로 발사할 땐 눈물 레이어 높이기
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
    }

    //이동 애니메이션
    void MoveAnim()
    {
        if (moveInput.x < 0) { bodyRenderer.flipX = true; }
        if (moveInput.x > 0) { bodyRenderer.flipX = false; }
        playerMoveAnim.SetFloat("Up&Down", Mathf.Abs(moveInput.y));
        playerMoveAnim.SetFloat("Left&Right", Mathf.Abs(moveInput.x));
        if (Input.GetKey(KeyCode.W))
        {
            playerShotAnim.SetBool("UpLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
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
            if(tear != null)
                tear.GetComponent<SpriteRenderer>().sortingOrder = 102;
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
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);
        playerAnim.SetTrigger("Hit");

        HitSound();
    }

    // 사망 애니메이션
    public void Dead()
    {
        DeadSound();

        //player head, player body 오브젝트 찾아서 끄기
        head.gameObject.SetActive(false);
        body.gameObject.SetActive(false);
        playerAnim.SetTrigger("Death");
    }

    //아이템 획득 애니메이션
    public IEnumerator GetItemTime()
    {
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
        UseItemSound();
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

    //폭탄 설치 기능
    void InstallBomb()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // 필드에 설치된 폭탄이없을때 && 보유중인 폭탄 개수가 1개 이상일때
            if(GameObject.Find("Putbomb") == null && ItemManager.instance.bombCount > 0)
            {
                ItemManager.instance.bombCount--;
                GameObject bomb = Instantiate(ItemManager.instance.bombPrefab, transform.position, Quaternion.identity) as GameObject;
                bomb.name = "Putbomb"; // 생성된 폭탄 오브젝트 이름 변경
            }
        }
    }

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

    public void UseItemSound()
    {
        audioSource.clip = useItemClip;
        audioSource.Play();
    }

    public void ShootSound()
    {
        audioSource.clip = ShootClip;
        audioSource.Play();
    }
    #endregion
}