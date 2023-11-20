using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ActiveInfo
{
    [Header("beforeStatement")]
    float beforeTearSize;

    void Awake()
    {
        SetActiveItem(2, 1);
        SetActiveString("유리 대포",
                        "조심해서 다루세요.",
                        "사용 시 캐릭터가 유리 대포를 머리 위로 들며" 
                      + "\n해당 방향으로 거대한 눈물을 발사한다.");
        beforeTearSize = PlayerManager.instance.playerTearSize;
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
            StartCoroutine(ShootCannon());
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }

    //아이템 사용 끝나면 원래 사이즈로 되돌려주기
    public override void afterActiveAttack()
    {
        PlayerManager.instance.playerTearSize = beforeTearSize;
        PlayerManager.instance.ChgTearSize();
    }

    private IEnumerator ShootCannon()
    {
        for (int l = 0; l < 2; l++)
        {
            float shootHor = Input.GetAxis("Horizontal");
            float shootVer = Input.GetAxis("Vertical");

            //Dr.Fetus 먹었을 때 폭탄 발사 사이즈
            if (ItemManager.instance.PassiveItems[16] && l == 0) PlayerManager.instance.playerTearSize *= 2.3f;
            else if (l == 0)
            {
                PlayerManager.instance.playerTearSize *= 8f;
            }
            PlayerManager.instance.ChgTearSize();

            if (shootHor == 0 && shootVer != 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, shootVer);
            }
            else if (shootHor != 0 && shootVer == 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(shootHor, 0);
            }
            else if (shootHor == 0 && shootVer == 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
            }
            yield return new WaitForSeconds(0.5f);
        }
        afterActiveAttack();
        yield return null;
    } 

    
}

