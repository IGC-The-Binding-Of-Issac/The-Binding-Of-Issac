using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tear : MonoBehaviour
{
    PlayerController playerController;
    Animator tearBoomAnim;
    Rigidbody2D tearRB;
    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;
    float playerTearSize;

    AudioSource audioSource;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip boomSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        tearRB = GetComponent<Rigidbody2D>();
        //플레이어 발사 시작위치
        playerPosition = playerController.transform.position;

        audioSource.volume = SoundManager.instance.GetSFXVolume(); // 볼륨 설정
        ShootSound(); // shoot 사운드 실행
    }

    void Update()
    {
        TearRange();
    }

    void TearRange()
    {
        //총알 위치
        tearPosition = this.transform.position;
        //둘 사이의 거리
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);

        //해당 아이템을 먹으면
        if (ItemManager.instance.PassiveItems[9])
        {
            //눈물 y값이 플레이어 y값보다 낮아지면 눈물 터트림
            if(tearPosition.y <= playerPosition.y)
                BoomTear();
        }
        //둘 사이의 거리가 플레이어 사거리보다 커지면
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            //눈물 터트림
            BoomTear();
        }
    }

    //눈물이 터지는 애니메이션이 실행되면서 애니메이션 위치는 고정
    //TearBoom 애니메이션 클립에 추가함
    public void StopTear()
    {
        BoomSound(); // tear 터지는 사운드
        //눈물 속도 0
        tearRB.velocity = Vector2.zero;
        //눈물 중력 0
        tearRB.gravityScale = 0;
    }

    public void BoomTear()
    {
        //눈물 터지는 애니메이션 실행
        tearBoomAnim.SetTrigger("BoomTear");
    }
    public void TearSize()
    {
        //플레이어 스탯 눈물 사이즈를 가져옴
        playerTearSize = PlayerManager.instance.playerTearSize;
        //현재 눈물에 플레이어 스탯 사이즈 적용
        gameObject.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
    }

    //눈물 애니메이션 클립 이벤트로 마지막에 추가 되어있음
    public void DestoryTear()
    {
        //총알 파괴
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject tmp = GameManager.instance.playerObject.GetComponent<PlayerController>().CheckedObject;
        
        //벽에 박으면 총알 터트리기
        if (tmp != collision.gameObject)
        {

            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
            }

            //똥에 박으면
            else if (collision.gameObject.CompareTag("Object_Poop"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.GetComponent<Poop>().GetDamage();
            }
            //황금 똥에 박으면
            else if (collision.gameObject.name == "Golden Poop(Clone)")
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.GetComponent<GoldenPoop>().GetDamage(); 
            }
            //불에 박으면
            else if (collision.gameObject.CompareTag("Object_Fire"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.GetComponent<FirePlace>().GetDamage();
            }
            //적과 박으면
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);
                //Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
                //총알 방향
                Vector2 direction = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
                //넉백
                StartCoroutine(collision.gameObject.GetComponent<Enemy>().knockBack());
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 200);
            }

            //boss와 충돌하면
            else if (collision.gameObject.CompareTag("Boss"))
            {
                BoomTear();
            }
        }
    }

    #region Sound
    void BoomSound()
    {
        audioSource.clip = boomSound;
        audioSource.Play();
    }

    void ShootSound()
    {
        audioSource.clip = shootSound;
        audioSource.Play();
    }
    #endregion
}