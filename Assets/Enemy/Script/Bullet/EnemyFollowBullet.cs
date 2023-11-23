using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBullet : Enemy_Bullet
{
    /// <summary>
    /// Player 따라가는 총알
    /// </summary>
       
    private void Start()
    {
        /*
        ani             = GetComponent<Animator>();
        playerPosi      = GameObject.FindWithTag("Player").transform; 
        bulletDesti     = new Vector3(playerPosi.position.x, playerPosi.position.y, 0);

        waitForDest     = 0.5f;
        bulletSpeed     = 5f;
        */
    }

    private void Update()
    {
        if (enemyBulletIsBomb) 
        {
            transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime);
            bulletDestroy();
        }
  
    }
    void bulletDestroy()
    {
        if (Vector3.Distance(gameObject.transform.position, bulletDesti) <= 0.5f)
        {
            ani.SetTrigger("bulletDestroy");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            enemyBulletIsBomb = false;
        }
    }


    public void resetBullet()
    {
        EnemyPooling.Instance.returnBullet(this.gameObject);
    }

}
