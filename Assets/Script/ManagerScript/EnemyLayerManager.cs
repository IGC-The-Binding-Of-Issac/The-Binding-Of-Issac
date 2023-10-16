using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLayerManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
