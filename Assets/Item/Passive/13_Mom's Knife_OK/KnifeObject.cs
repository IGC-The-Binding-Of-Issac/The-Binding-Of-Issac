using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeObject : MonoBehaviour
{

    [SerializeField]
    public Transform startPosition;
    private Rigidbody2D rb;
    [SerializeField]
    float shootForceX;
    [SerializeField]
    float shootForceY;

    public bool canShoot = true;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ShootKnife();
        if(canShoot) transform.position = startPosition.position;
    }
  
    private void ShootKnifeX(float x, float distance)
    {
        if (x != 0 && canShoot)
        {
            if (x > 0) shootForceX += Time.deltaTime;

            else if (x < 0) shootForceX -= Time.deltaTime;

            if (shootForceX >= 1f) shootForceX = 1f;

            else if (shootForceX <= -1f) shootForceX = -1f;
        }
        if (x == 0 && canShoot && shootForceX != 0)
        {
            if (shootForceX > 0 && x == 0)
            {
                rb.AddForce(new Vector3(shootForceX, 0, 0) * shootForceX * 7f, ForceMode2D.Impulse);
            }
            else if (shootForceX < 0 && x == 0)
            {
                rb.AddForce(new Vector3(shootForceX, 0, 0) * -shootForceX * 7f, ForceMode2D.Impulse);
            }
            canShoot = false;
        }
        if ((!canShoot && rb.velocity.x <= 1.5f && shootForceX > 0) || (!canShoot && rb.velocity.x >= -1.5f && shootForceX < 0))
        {
            rb.velocity = Vector3.zero;
            Vector3 velo = Vector3.zero;
            Vector3 currentPosition = gameObject.transform.position;
            transform.position = Vector3.SmoothDamp(currentPosition, startPosition.position, ref velo, 0.07f);
        }
        if ((!canShoot && distance <= 0.5f && rb.velocity.x <= 0.1f && shootForceX > 0) || (!canShoot && distance <= 0.5f && rb.velocity.x >= -0.1f && shootForceX < 0))
        {
            transform.position = startPosition.position;
            canShoot = true;
            shootForceX = 0;
        }
    }
    
    private void ShootKnifeY(float y, float distance)
    {
        if (y != 0 && canShoot)
        {
            if (y > 0) shootForceY += Time.deltaTime;
            else if (y < 0) shootForceY -= Time.deltaTime;

            if (shootForceY >= 1f) shootForceY = 1f;
            else if (shootForceY <= -1f) shootForceY = -1f;
        }
        if (y == 0 && canShoot && shootForceY != 0)
        {
            if (shootForceY > 0 && y == 0)
            {
                rb.AddForce(new Vector3(0, shootForceY, 0) * shootForceY * 7f, ForceMode2D.Impulse);
            }
            else if (shootForceY < 0 && y == 0)
            {
                rb.AddForce(new Vector3(0, shootForceY, 0) * -shootForceY * 7f, ForceMode2D.Impulse);
            }
            canShoot = false;
        }
        if ((!canShoot && rb.velocity.y <= 1.5f && shootForceY > 0) || (!canShoot && rb.velocity.y >= -1.5f && shootForceY < 0))
        {
            rb.velocity = Vector3.zero;
            Vector3 velo = Vector3.zero;
            Vector3 currentPosition = gameObject.transform.position;
            transform.position = Vector3.SmoothDamp(currentPosition, startPosition.position, ref velo, 0.07f);
        }
        if ((!canShoot && distance <= 0.5f && rb.velocity.y <= 0.1f && shootForceY > 0) || (!canShoot && distance <= 0.5f && rb.velocity.y >= -0.1f && shootForceY < 0))
        {
            transform.position = startPosition.position;
            canShoot = true;
            shootForceY = 0;
        }
    }
        
    
    private void ShootKnife()
    {
        float x = Input.GetAxisRaw("ShootHorizontal");
        float y = Input.GetAxisRaw("ShootVertical");
        float distance = Vector3.Distance(startPosition.position, transform.position);
        ShootKnifeX(x, distance);
        ShootKnifeY(y, distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }
}
