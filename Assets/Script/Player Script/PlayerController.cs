using System.Collections;
using System.Collections.Generic;
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

    [Header("Sprite")]
    public Sprite defaultTearImg;
    SpriteRenderer bodyRenderer;
    SpriteRenderer headRenderer;
    Rigidbody2D playerRB;

    [Header("Function")]
    private float lastshot;
    Vector2 moveInput;

    float shootHor;
    float shootVer;
    public GameObject tear;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = defaultTearImg;
    }
    
    void Update()
    {
        MoveAnim();
        ShotAnim();
        InstallBomb();
    }
    void FixedUpdate()
    {
        Movement();
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
        Vector3 tearPoint = gameObject.transform.GetChild(5).transform.position;
        //발사 기능 구현
        //게임 중 눈물 생성 눈물 프리펩, 발사 시작위치, 회전
        tear = Instantiate(PlayerManager.instance.tearObj, tearPoint, transform.rotation) as GameObject;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);
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
        if (Input.GetKeyDown(KeyCode.A))
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
        playerAnim.SetTrigger("Hit");
    }

    // 사망 애니메이션
    public void Dead()
    {
        //player head, player body 오브젝트 찾아서 끄기
        head.gameObject.SetActive(false);
        body.gameObject.SetActive(false);
        playerAnim.SetTrigger("Death");

        // 사망애니메이션 이후 사망아웃트로 씬으로 이동 작성
    }

    //아이템 획득 애니메이션
    public IEnumerator GetItemTime()
    {
        //원래 모습은 가려둠
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //아이템 획득 애니메이션 실행
        getItem.SetTrigger("GetItem");
        //애니메이션 1초간 유지
        yield return new WaitForSeconds(1f);
        //플레이어 모습 보이게 함
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);

        //itemPosition 자식이 생기고
        if (itemPosition.childCount != 0)
        {
            Destroy(itemPosition.GetChild(0).gameObject);
        }
    }

    public IEnumerator GetTrinketItem()
    {
        //원래 모습은 가려둠
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //아이템 획득 애니메이션 실행
        getItem.SetTrigger("GetItem");
        //애니메이션 1초간 유지
        yield return new WaitForSeconds(1f);
        //플레이어 모습 보이게 함
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().KeepItem();
    }

    public IEnumerator GetActiveItem()
    {
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);

        ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().KeepItem();
    }

    public IEnumerator UseActiveItem()
    {
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(0.4f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
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


}