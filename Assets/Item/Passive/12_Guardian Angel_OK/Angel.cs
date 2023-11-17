using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 targetPosition = new Vector3(0, -1.68f, 0);
    private float lastShot;
    [SerializeField]
    Tear tearobj;
    public Transform shootPoint;
    public GameObject miniTear;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveAngel();
        AttackAngel();
    }

    private void MoveAngel()
    {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            Vector3 stop = Vector3.zero;
            Vector3 currentPosition = transform.localPosition;
            if (moveX > 0) //¿À¸¥ÂÊ
            {
                targetPosition = new Vector3(1.83f, 0.2f, 0);
            }
            else if (moveX < 0)
            {
                targetPosition = new Vector3(-1.83f, 0.2f, 0);
            }
            if (moveY > 0)
            {
                targetPosition = new Vector3(0, 2.18f, 0);
            }
            else if (moveY < 0)
            {
                targetPosition = new Vector3(0, -1.68f, 0);
            }
            transform.localPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref stop, 0.12f);
    }

    private void AttackAngel()
    {
        float shootX = Input.GetAxisRaw("ShootHorizontal");
        float shootY = Input.GetAxisRaw("ShootVertical");

        float shotDelay = PlayerManager.instance.playerShotDelay;
        if ((shootX != 0 || shootY != 0) && Time.time > lastShot + shotDelay)
        {
            if (shootX != 0 && shootY != 0)
            {
                shootX = 0;
            }
            else
            {
                Shoot(shootX, shootY);
            }
            lastShot = Time.time;
        }
    }
    
    private void Shoot(float shootX, float shootY)
    {
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        Vector3 firePoint = shootPoint.transform.position;
        miniTear = Instantiate(PlayerManager.instance.tearObj, firePoint, Quaternion.identity) as GameObject;
        miniTear.GetComponent<Transform>().localScale = new Vector3(0.55f, 0.55f, 0); 
        miniTear.GetComponent<Rigidbody2D>().velocity = new Vector3(shootX * tearSpeed, shootY * tearSpeed, 0);
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = miniTear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = miniTear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = miniTear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = miniTear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
    }
}
