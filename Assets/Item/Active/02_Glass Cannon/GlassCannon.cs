using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ActiveInfo
{
    float beforeTearSize;
    float beforeDamage;
    private GameObject tear;
    void Start()
    {
        SetActiveItem(2, 1);
        beforeTearSize = PlayerManager.instance.playerTearSize;
        beforeDamage = PlayerManager.instance.playerDamage;
    }

    public override void UseActive()
    {
        float shootHor = Input.GetAxis("Horizontal");
        float shootVer = Input.GetAxis("Vertical");
        PlayerManager.instance.playerTearSize *= 8f;
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.playerDamage *= 10f;
        if (shootHor == 0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, shootVer);
        }
        else
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(shootHor, shootVer);
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

