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
    public int playerMaxHp = 6; // 최대 체력
    public float playerMoveSpeed = 5f; // 이동 속도
    public float playerTearSpeed = 6f; // 투사체 속도
    public float playerShotDelay = 0.5f; // 공격 딜레이
    public float playerDamage = 1f; // 데미지
    public float playerRange = 5f; // 사거리
    public float playerTearSize = 1f; //눈물 크기
    public float playerSize = 1f; //캐릭터 크기

    GameObject playerObj;
    public GameObject tearObj;
    bool CanGetDamage = true; // 데미지를 받을 수 있는지 확인.
    float hitDelay = .5f; // 피격 딜레이

    //delegate 선언 위치

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
                GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
            }
            else
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
        }            
    }

    //피격 딜레이
    IEnumerator HitDelay()
    {
        playerObj = GameManager.instance.playerObject;

        //피격 숫자만큼 딜레이
        yield return new WaitForSeconds(hitDelay);

        int countTime = 0;

        while(countTime < 14)
        {
            //countTIme%2 == 0이면 플레이어 모습이 보임
            if (countTime%2 == 0)
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            //countTIme%2 != 0이면 플레이어 모습이 안보임
            else
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
            countTime++;

            yield return new WaitForSeconds(0.1f);
        }
        //while문 실행 후 모습이 보임
        playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        
        //피격 판정 됨
        CanGetDamage = true;
    }
    public void ChgTearSize()
    {
        tearObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
    }

    public void ChgPlayerSize()
    {
        playerObj.transform.localScale = new Vector3(playerSize, playerSize, 0);
    }

}
