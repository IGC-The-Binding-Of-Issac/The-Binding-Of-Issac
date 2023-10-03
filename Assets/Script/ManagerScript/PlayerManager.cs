using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Player Stat")]
    public int playerHp = 6; // 현재 체력
    public int playerMaxHp = 6; // 최대체력
    public float playerMoveSpeed = 5f; // 이동속도
    public float playerTearSpeed = 6f; // 투사체속도
    public float playerShotDelay = 0.5f; // 공격딜레이
    public float playerDamage = 1f; // 데미지
    public float playerRange = 5f; // 사거리

    GameObject playerObj;
    bool CanGetDamage = true; // 데미지를 받을 수 있는지 확인.
    float hitDelay = .5f; // 피격 딜레이

    public void Start()
    {

    }
    public void GetDamage()
    {
        if(CanGetDamage)
        {
            playerHp--;
            CanGetDamage = false;
            StartCoroutine(HitDelay());
            if(playerHp <= 0) // 데미지를 받았을때 HP가 0이하가 되면 사망함수 실행.
            {
                Dead();
            }else
                Hit();
        }            
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);

        int countTime = 0;

        while(countTime < 10)
        {
            if (countTime%2 == 0)
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
            countTime++;

            yield return new WaitForSeconds(0.1f);
        }
        playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        CanGetDamage = true;
    }        

    void Hit()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        playerObj.GetComponent<PlayerController>().HitAnim();

        //playerObj.transform.GetChild(0).gameObject.SetActive(false);
        //playerObj.transform.GetChild(1).gameObject.SetActive(false);
    }

    // 사망 함수
    void Dead()
    {
        //player head, player body 오브젝트 찾아서 끄기
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.GetChild(0).gameObject.SetActive(false);
        playerObj.transform.GetChild(1).gameObject.SetActive(false);
        // 사망애니메이션 실행
        playerObj.GetComponent<PlayerController>().DieAnim();
        // 사망애니메이션 이후 사망아웃트로 씬으로 이동 작성
    }
    
    void GetItemAnim()
    {
        //아이템 먹는 애니메이션
    }
}
