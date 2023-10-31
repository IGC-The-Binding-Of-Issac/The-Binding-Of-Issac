using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tearPosition : MonoBehaviour
{
    public bool turnOn;

    private void Update()
    {
        Debug.Log(turnOn);
    }
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object_Rock") || collision.gameObject.CompareTag("Wall")
            || collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Fire"))
        {
            turnOn = false;
        }
        else
        {
            turnOn = true;
        }
    }
}
