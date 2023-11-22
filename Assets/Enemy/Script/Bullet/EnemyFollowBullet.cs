using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBullet : Enemy_Bullet
{
    /// <summary>
    /// Player 따라가는 총알
    /// </summary>
    [SerializeField] Vector3 bulletDesti;
    [SerializeField] Transform playerPosi;


    bool isMoving = true;

    private void Start()
    {
        ani             = GetComponent<Animator>();
        playerPosi      = GameObject.FindWithTag("Player").transform; 
        bulletDesti     = new Vector3(playerPosi.position.x, playerPosi.position.y, 0);

        waitForDest     = 0.5f;
        bulletSpeed     = 5f;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime); 
            bulletDestroy(); 
        }
    }
    void bulletDestroy()
    {
        if (Vector3.Distance(gameObject.transform.position, bulletDesti) <= 0.5f)
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
        }
    }
}
