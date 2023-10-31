using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearPoint : MonoBehaviour
{
    public GameObject overLapObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock") || 
            collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Fire"))
        {
            overLapObject = collision.gameObject;
        }
        else
        {
            overLapObject = null;
        }

    }

}
