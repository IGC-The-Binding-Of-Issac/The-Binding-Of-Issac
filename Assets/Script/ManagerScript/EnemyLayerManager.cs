using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLayerManager : MonoBehaviour
{
    void FixedUpdate()
    {

        if (GameManager.instance.playerObject.transform.position.y > gameObject.transform.position.y)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 110;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
        }
    }
}
