using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D.Animation;

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

    public bool CanGetDamage = true; // 데미지를 받을 수 있는지 확인.
    public int CanBlockDamage = 0; // Holy Mantle 습득 시 데미지를 3회 방어해준다.
    public int deathCount = 0;
    float hitDelay = .5f; // 피격 딜레이

    [Header("unity setup")]
    public GameObject tearObj;
    GameObject playerObj;

    [Header("Player Sprite")]
    SpriteRenderer playerHead;
    SpriteRenderer playerBody;
    SpriteRenderer headItem;

    [Header("Player OutFit")]
    public SpriteLibraryAsset[] head;
    public SpriteLibraryAsset[] body;
    public SpriteLibraryAsset[] tear;

    public void Start()
    {
        ItemManager.instance.bombPrefab.transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<AudioSource>();
        PlayerInitialization();
    }
    void PlayerInitialization()
    {
        playerHp = 6; // 현재 체력
        playerMaxHp = 6; // 최대 체력
        playerMoveSpeed = 5f; // 이동 속도
        playerTearSpeed = 6f; // 투사체 속도
        playerShotDelay = 0.5f; // 공격 딜레이
        playerDamage = 1f; // 데미지
        playerRange = 5f; // 사거리
        playerTearSize = 1f; //눈물 크기
        playerSize = 1f; //캐릭터 크기
        CanBlockDamage = 0;
        CanGetDamage = true;
        hitDelay = 0.5f; // 피격 딜레이
    }
    public void CheckedShotDelay()
    {
        if(playerShotDelay < 0.05)
        {
            playerShotDelay = 0.05f;
        }
    }

    public void CheckedDamage()
    {
        if (playerDamage < 0.3f)
        {
            playerDamage = 0.3f;
        }
    }
    public void CheckedStatus()
    {
        //체력 체크
        if (playerHp <= 0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
            Invoke("GameOver", 0.7f);
        }
        else if (playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //이동속도 최소값
        if (playerMoveSpeed < 1f)
        {
            playerMoveSpeed = 1f;
        }
        //눈물 속도 최소값
        if (playerTearSpeed < 0.5f)
        {
            playerTearSpeed = 0.5f;
        }
        //공격 속도 최소값
        if (playerShotDelay < 0.05f)
        {
            playerShotDelay = 0.05f;
        }
        //데미지 최소값
        if (playerDamage < 0.3f)
        {
            playerDamage = 0.3f;
        }
        //사거리 최소값
        if (playerRange < 0.5f)
        {
            playerRange = 0.5f;
        }
    }
    #region SkinChange
    public void SetHeadSkin(int index)
    {
        //머리 외형을 바꾸는 함수
        ChangeHead(head[index]);
    }
    public void SetBodySkin(int index) 
    { 
        //몸통 외형을 바꾸는 함수
        ChangeBody(body[index]);
    }
    public void SetTearSkin(int index)
    {
        //눈물 외형을 바꾸는 함수
        ChangeTear(tear[index]);
    }
    public void ChangeHead(SpriteLibraryAsset head)
    {
        //SpriteLibraryAsset안에 있는 head를 플레이어 스프라이트에 넣어줌 
        GameManager.instance.playerObject.GetComponent<PlayerController>().head.GetComponent<SpriteLibrary>().spriteLibraryAsset = head;
    }

    public void ChangeBody(SpriteLibraryAsset body)
    {
        //SpriteLibraryAsset안에 있는 body를 플레이어 스프라이트에 넣어줌
        GameManager.instance.playerObject.GetComponent<PlayerController>().body.GetComponent<SpriteLibrary>().spriteLibraryAsset = body;
    }

    public void ChangeTear(SpriteLibraryAsset tear)
    {
        //SpriteLibraryAsset안에 있는 tear를 플레이어 스프라이트에 넣어줌
        Transform allChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
        for (int i = 0; i < allChildren.childCount; i++)
        {
            GameObject obj = allChildren.GetChild(i).gameObject;
            obj.GetComponent<SpriteLibrary>().spriteLibraryAsset = tear;
        }
        //tearObj.GetComponent<SpriteLibrary>().spriteLibraryAsset = tear;
    }
    #endregion
    //delegate 선언 위치


    public void CheckedPlayerHP()
    {
        if (playerHp<=0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
            Invoke("GameOver", 0.7f);
        }
        else if (playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
    }

    public void GetDamage()
    {
        UIManager.instance.SetPlayerCurrentHP();
        if (ItemManager.instance.PassiveItems[6] && CanGetDamage && CanBlockDamage > 0) //홀리 맨틀 먹었을 때
        {
            StartCoroutine(HitDelay());
            CanGetDamage = false;
            CanBlockDamage--;
        }
        else if (ItemManager.instance.PassiveItems[14] && CanGetDamage && CanBlockDamage == 0) // Dead Cat 먹었을 때
        {
            playerHp--;
            CanGetDamage = false;
            if (playerHp > 0)
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
            else //체력이 0일 때
            {
            int[] liveOrDeath = { 0, 1 }; 
            int randomNum = (UnityEngine.Random.Range(0, 100000) % 2);
            if (liveOrDeath[randomNum] == 0) 
                 {
                    playerHp = 2; //최대 체력만큼 채워줌
                    StartCoroutine(HitDelay());
                    GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
                 }
            else
                 {
                    GameManager.instance.playerObject.GetComponent<PlayerController>().Dead(); //그냥 죽음
                    Invoke("GameOver", 0.7f);
                 }
            }
        }
        else if (ItemManager.instance.PassiveItems[15] && CanGetDamage && CanBlockDamage == 0) // Guppy's tail 먹었을 때
        {
            playerHp--;
            CanGetDamage = false;
            if (playerHp > 0)
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
            else if (playerHp <= 0 && deathCount >= 1)
            {
                playerHp = playerMaxHp;
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
                deathCount--;
            }
            else
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Dead(); //그냥 죽음
                Invoke("GameOver", 0.7f);
            }
        }
        else if (CanGetDamage && CanBlockDamage == 0)
        {
            playerHp--;
            CanGetDamage = false;
            if(playerHp <= 0) // 데미지를 받았을때 HP가 0이하가 되면 사망함수 실행.
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
                Invoke("GameOver", 0.7f);
            }
            else
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
        }

    }

    void GameOver()
    {
        Invoke("GameOver", 0.7f);
        UIManager.instance.GameOver();
    }

    //피격 딜레이
    public IEnumerator HitDelay()
    {
        playerObj = GameManager.instance.playerObject;
        playerHead = playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerBody = playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        headItem = playerObj.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>();
        //피격 숫자만큼 딜레이
        yield return new WaitForSeconds(hitDelay);

        int countTime = 0;

        //피격 후 깜빡이는 효과
        while(countTime < 14)
        {
            //countTIme%2 == 0이면 플레이어 모습이 보임
            if (countTime%2 == 0)
            {
                playerHead.color = new Color(1, 1, 1, 1);
                playerBody.color = new Color(1, 1, 1, 1);
                headItem.color = new Color(1, 1, 1, 1);
            }
            //countTIme%2 != 0이면 플레이어 모습이 안보임
            else
            {
                playerHead.color = new Color(1, 1, 1, 0);
                playerBody.color = new Color(1, 1, 1, 0);
                headItem.color = new Color(1, 1, 1, 0);
            }
            countTime++;
            //0.1초에 한번씩 깜빡임
            yield return new WaitForSeconds(0.1f);
        }
        //while문 실행 후 모습이 보임
        playerHead.color = new Color(1, 1, 1, 1);
        playerBody.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);

        //피격 판정 됨
        CanGetDamage = true;
    }
    public void ChgTearSize()
    {
        //눈물 사이즈 변경
        Transform tearChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
        Transform DrbombChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().DrBombPointTransform;
        for (int i = 0; i < tearChildren.childCount; i++)
        {
            GameObject tearObj = tearChildren.GetChild(i).gameObject;
            tearObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
        }
        for (int i = 0; i < DrbombChildren.childCount; i++)
        {
            GameObject bombObj = DrbombChildren.GetChild(i).gameObject;
            bombObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
        }
    }

    public void ChgPlayerSize()
    {
        //플레이어 스케일 변경
        playerObj = GameManager.instance.playerObject;
        playerObj.transform.localScale = new Vector3(playerSize, playerSize, 0);
    }
}
