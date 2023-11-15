using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IemSpawnEffect : MonoBehaviour
{
    Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
