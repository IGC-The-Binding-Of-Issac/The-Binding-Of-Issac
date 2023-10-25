using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeart : MonoBehaviour
{
    [SerializeField] GameObject HP1;
    [SerializeField] GameObject HP2;
    public void SetHeart(int hp)
    {
        switch(hp)
        {
            case 0:
                HP1.SetActive(false);
                HP2.SetActive(false);
                break;

            case 1:
                HP1.SetActive(true);
                HP2.SetActive(false);
                break;

            case 2:
                HP1.SetActive(false);
                HP2.SetActive(true);
                break;
        }
    }
}
