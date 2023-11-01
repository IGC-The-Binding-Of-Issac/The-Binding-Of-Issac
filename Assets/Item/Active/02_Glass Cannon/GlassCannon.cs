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
        beforeTearSize = PlayerManager.instance.playerTearSize;
        beforeDamage = PlayerManager.instance.playerDamage;
    }

    public override void UseActive()
    {
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");
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

