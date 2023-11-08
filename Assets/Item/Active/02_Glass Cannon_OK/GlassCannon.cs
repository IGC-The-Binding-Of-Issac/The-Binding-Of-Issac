using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ActiveInfo
{
    float beforeTearSize;
    float beforeDamage;
    private GameObject tear;
    void Awake()
    {
        SetActiveItem(2, 1);
        SetActiveString("유리 대포",
                        "조심해서 다루세요.",
                        "사용 시 캐릭터가 유리 대포를 머리 위로 들며" +
                        "\n해당 방향으로 엄청나게 거대한 눈물을 발사한다." +
                        "\n공격력은 현재 공격력 * 10이다.");
        beforeTearSize = PlayerManager.instance.playerTearSize;
        beforeDamage = PlayerManager.instance.playerDamage;
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
            float shootHor = Input.GetAxis("Horizontal");
            float shootVer = Input.GetAxis("Vertical");
            PlayerManager.instance.playerTearSize *= 8f;
            PlayerManager.instance.ChgTearSize();
            PlayerManager.instance.playerDamage *= 10f;
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
            tear = GameObject.Find("Tear(Clone)");
            afterActiveAttack();
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public override void afterActiveAttack()
    {
        PlayerManager.instance.playerTearSize = beforeTearSize;
        PlayerManager.instance.ChgTearSize();
    }

    public override void CheckedItem()
    {
        if (tear == null)
        {
            PlayerManager.instance.playerDamage = beforeDamage;
        }
    }
}

