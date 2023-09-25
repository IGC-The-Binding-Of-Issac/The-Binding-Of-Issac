using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearController : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    // Start is called before the first frame update

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 위치
        playerPosition = playerController.transform.position;
        //총알 위치
        tearPosition = this.transform.position;
        //둘 사이의 거리
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);

        //둘 사이의 거리가 플레이어 사거리보다 커지면
        if (betweenDistance >= GameController.instance.playerRange)
        {
            //눈물 터지는 애니메이션 실행
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }

    public void StopTear()
    {
        //총알 오브젝트 속도를 zero로 만듬
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void DestoryTear()
    {
        //총알 파괴
        Destroy(gameObject);
    }
}