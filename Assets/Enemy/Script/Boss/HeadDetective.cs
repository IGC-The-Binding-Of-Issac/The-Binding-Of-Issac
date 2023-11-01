using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetective : MonoBehaviour
{
    [SerializeField] SnakeManager parent;

    void Start()
    {
        parent = transform.parent.GetComponent<SnakeManager>();
    }

// Update is called once per frame
    void Update()
    {
        gameObject.transform.position = parent.snakeBody[0].transform.position;
    }

    //충돌처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) 
        {
            //벽이랑 충돌하면 리셋
            parent.stateReset();

        }
    }


}
