using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearPoint : MonoBehaviour
{
    public GameObject overLapObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //벽,돌,똥,불에 눈물 발사지점이 충돌해있으면 overLapObject에 충돌한 오브젝트를 넣어줌
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
