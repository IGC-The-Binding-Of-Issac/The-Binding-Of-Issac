using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TammyHead : ActiveInfo
{
    float beforeDamage;
    private GameObject activeTear;
    void Start()
    {
        SetActiveItem(3, 1);
        beforeDamage = PlayerManager.instance.playerDamage;
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 25f;
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, -1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, -1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 0);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, -1);
        activeTear = GameObject.Find("Tear(Clone)");
    }

    public override void CheckedItem()
    {
        if (activeTear == null) PlayerManager.instance.playerDamage = beforeDamage;
    }
}
