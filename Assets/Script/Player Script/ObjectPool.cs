using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private GameObject poolingObjects;
    private Queue<Tear> poolTear = new Queue<Tear>();
    private void Awake()
    {
        instance = this;
        Initialize(10);
    }
    private Tear CreateTear()
    {
        var newObj = Instantiate(poolingObjects).GetComponent<Tear>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private void Initialize(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            poolTear.Enqueue(CreateTear());
        }
    }
    //public static Tear GetTear()
    //{
    //    if (instance)
    //    {

    //    }
    //    else
    //    {

    //    }
    //}
}
