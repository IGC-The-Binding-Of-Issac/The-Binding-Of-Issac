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
        // 초기화는 EnemPooling에서 진행
        /*
        ani         = GetComponent<Animator>();
        waitForDest = 0.5f;
        bulletSpeed = 5f;
        */

    }

    public void resetBullet()
    {
        EnemyPooling.Instance.returnBullet(this.gameObject);
    }

}
