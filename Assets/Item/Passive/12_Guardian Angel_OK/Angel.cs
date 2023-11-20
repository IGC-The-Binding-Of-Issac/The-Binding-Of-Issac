using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(0, -1.68f, 0);

    private void Update()
    {
        MoveAngel();
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

    
}
