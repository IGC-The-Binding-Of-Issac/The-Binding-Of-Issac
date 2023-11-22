using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStraightBullet : Enemy_Bullet
{
    /// <summary>
    /// 직진하는 총알
    /// </summary>
    void Start()
    {
        isCoru = true;
        ani         = GetComponent<Animator>();
        waitForDest = 0.5f;
        bulletSpeed = 5f;
    }

    public void resetBullet()
    {
        Debug.Log("이에ㅐㅇㅇ");
        EnemyPooling.Instance.returnBullet(this.gameObject);
    }

}
