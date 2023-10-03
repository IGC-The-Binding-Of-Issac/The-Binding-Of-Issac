using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator PlayerMoveAnim;

    public Animator PlayerShotAnim;

    public Animator PlayerAnim;

    Rigidbody2D playerRB;

    public GameObject tearPrefab;

    GameObject tear;

    float tearSpeed;

    float moveSpeed;

    float shotDelay;

    private float lastshot;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 이부분 조금 수정해야할듯
        moveSpeed = PlayerManager.instance.playerMoveSpeed;
        tearSpeed = PlayerManager.instance.playerTearSpeed;
        shotDelay = PlayerManager.instance.playerShotDelay;

        Movement();

        MoveAnim();

        ShotAnim();

        InstallBomb();

    }

    //이동 기능
    void Movement()
    {
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

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
        playerRB.velocity = new Vector3(hori * moveSpeed, verti * moveSpeed, 0);
    }

    //발사 기능
    void Shoot(float x, float y)
    {
        //발사 기능 구현
        tear = Instantiate(tearPrefab, transform.position + Vector3.up * 0.4f, transform.rotation) as GameObject;
        tear.AddComponent<Rigidbody2D>().gravityScale = 0;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * tearSpeed : Mathf.Ceil(x) * tearSpeed,
            (y < 0) ? Mathf.Floor(y) * tearSpeed : Mathf.Ceil(y) * tearSpeed, 0);

        //총알이 대각으로 밀려서 발사되게 옆으로 힘을 줌
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            PlayerMoveAnim.SetBool("playerFrontWalk", true);
        }
        else
        {
            PlayerMoveAnim.SetBool("playerFrontWalk", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerMoveAnim.SetBool("playerLeftWalk", true);
        }
        else
        {
            PlayerMoveAnim.SetBool("playerLeftWalk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerMoveAnim.SetBool("playerRightWalk", true);
        }
        else
        {
            PlayerMoveAnim.SetBool("playerRightWalk", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            PlayerShotAnim.SetBool("UpLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("UpLook", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerShotAnim.SetBool("DownLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("DownLook", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerShotAnim.SetBool("LeftLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("LeftLook", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerShotAnim.SetBool("RightLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("RightLook", false);
        }
    }

    //발사 애니메이션
    void ShotAnim()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerShotAnim.SetBool("playerLeftShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerLeftShot", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerShotAnim.SetBool("playerRightShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerRightShot", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerShotAnim.SetBool("playerUpShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerUpShot", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerShotAnim.SetBool("playerDownShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerDownShot", false);
        }
    }

    //사망 애니메이션
    public void DieAnim() 
    {        
        PlayerAnim.SetTrigger("Death");
    }

    //피격 애니메이션
    public void HitAnim()
    {
        PlayerAnim.SetTrigger("Hit");
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