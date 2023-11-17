using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    /// <summary>
    /// MonoBehaviour을 상속받지 않는 클래스에서 코루틴을 사용하고 싶을 때 
    /// 쓰는 클래스
    /// </summary>

    IEnumerator enumerator = null;

    private void Coroutine(IEnumerator coro)
    {
        enumerator = coro;
        StartCoroutine(coro);
    }

    void Update()
    {
        if (enumerator != null)
        {
            if (enumerator.Current == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Stop()
    {
        StopCoroutine(enumerator.ToString());
        Destroy(gameObject);
    }

    public static CoroutineHandler Start_Coroutine(IEnumerator coro)
    {
        GameObject obj = new GameObject("CoroutineHandler");
        CoroutineHandler handler = obj.AddComponent<CoroutineHandler>();
        if (handler)
        {
            handler.Coroutine(coro);
        }
        return handler;
    }

}
