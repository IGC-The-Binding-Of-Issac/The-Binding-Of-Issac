using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeObject : MonoBehaviour
{
    public PlayerController playerCtr;

    private Rigidbody2D rb;
    private float triggerStay;
    private Vector3 startPosition;
    private bool canShoot = true;
    private bool canReturn;
    [SerializeField]
    float shootForce;
    private void Start()
    {
        playerCtr = GameManager.instance.playerObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ShootKnife();
        RangeCheck();
    }

    private void ShootKnife()
    {
        float x = Input.GetAxisRaw("ShootHorizontal");
        float y = Input.GetAxisRaw("ShootVertical");
        if (x > 0 && canShoot)
        {
            shootForce += Time.deltaTime;
        }
        if (x == 0 && shootForce != 0 && canShoot)
        {
            startPosition = gameObject.transform.position;
            Debug.Log(startPosition);
            rb.AddForce(new Vector3(shootForce, 0, 0) * shootForce * 2.5f, ForceMode2D.Impulse);
            canShoot = false;
        }
    }
    private void RangeCheck()
    {
        if(!canShoot)
        {
            Vector3 knifePosition = gameObject.transform.position;
            float distance = Vector3.Distance(knifePosition, startPosition);
            if (!canShoot && rb.velocity.x <= 0.5f)
            {
                canReturn = true;
            }
            if (canReturn)
            {
                rb.AddForce(new Vector3(-shootForce, 0, 0) * shootForce * 0.2f, ForceMode2D.Force);
            }
            if (canReturn && distance <= 0.01f)
            {
                rb.velocity = Vector3.zero;
                canReturn = false;
                canShoot = true;
                shootForce = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        triggerStay += Time.deltaTime;
        if (triggerStay > 0.15f)
        {
            //똥에 박으면
            if (collision.gameObject.CompareTag("Object_Poop"))
            {
                for (int l = 0; l < 4; l++)
                {
                collision.GetComponent<Poop>().GetDamage();
                }
            }
            //황금 똥에 박으면
            else if (collision.gameObject.name == "Golden Poop(Clone)")
            {
                for (int l = 0; l < 4; l++)
                {
                collision.GetComponent<GoldenPoop>().GetDamage();
                }
            }
            //불에 박으면
            else if (collision.gameObject.CompareTag("Object_Fire"))
            {
                for (int l = 0; l < 4; l++)
                {
                collision.GetComponent<FirePlace>().GetDamage();
                }
            }
            //적과 박으면
            else if (collision.gameObject.CompareTag("Enemy"))
            {
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);
            }
            triggerStay = 0;
        }
        
    }
}
