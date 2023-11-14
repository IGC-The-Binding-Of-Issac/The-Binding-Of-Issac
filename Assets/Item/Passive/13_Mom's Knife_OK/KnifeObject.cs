using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeObject : MonoBehaviour
{
    public PlayerController playerCtr;

    private Rigidbody2D rb;
    private float triggerStay;
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
        if (x > 0)
        {
            shootForce += Time.deltaTime;
        }
        if (x == 0 && shootForce != 0 && !canReturn)
        {
            rb.velocity = new Vector3(shootForce, 0, 0);
            canReturn = true;
        }
    }
    private void RangeCheck()
    {
        Vector3 knifePosition = this.gameObject.transform.position;
        Vector3 playerPosition = GameManager.instance.playerObject.transform.position;
        float between = Vector3.Distance(knifePosition, playerPosition);
        if (between >= PlayerManager.instance.playerRange && canReturn)
        {
            rb.velocity = new Vector3(-shootForce, 0, 0);
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
