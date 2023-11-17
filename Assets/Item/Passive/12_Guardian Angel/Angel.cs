using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    [SerializeField]
    Transform destinationPosition;
    private Rigidbody2D rb;

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
        Vector3 nextPosition = Vector3.zero;
        Vector3 stop = Vector3.zero;
        if (moveX > 0) //오른쪽
        {
            nextPosition = new Vector3(1.83f, 0.2f, 0);
        }
        else if (moveX < 0) //왼쪽
        {
            nextPosition = new Vector3(-1.83f, 0.2f, 0);
            transform.localPosition = Vector3.SmoothDamp(transform.position, nextPosition, ref stop, 1f);
        }
        if (moveY > 0) //위
        {
            nextPosition = new Vector3(0, 2.18f, 0);
        }
        else if (moveY < 0) //아래
        {
            nextPosition = new Vector3(0, -1.68f, 0);
        }
        

    }

    private void AttackAngel()
    {
        float shootX = Input.GetAxisRaw("ShootHorizontal");
        float shootY = Input.GetAxisRaw("ShootVertical");
    }
}
