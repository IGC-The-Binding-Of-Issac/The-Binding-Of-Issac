using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OopsEffect : MonoBehaviour
{
    bool canDamage = true;
    private void Start()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0);
        StartCoroutine(flooring());
    }

    //Oops 생성 시 점점 스케일이 커졌다가 다시 줄어드는 방식, Coroutine이 끝나면 Destroy
    private IEnumerator flooring()
    {
        while (gameObject.transform.localScale.x <= 1.5f)
        {
            gameObject.transform.localScale += new Vector3(0.09f, 0.09f, 0);
            yield return new WaitForSeconds(0.0375f);
        }
        yield return new WaitForSeconds(1.5f);
        while (gameObject.transform.localScale.x >= 0.3f)
        {
            gameObject.transform.localScale -= new Vector3(0.09f, 0.09f, 0);
            yield return new WaitForSeconds(0.0375f);
        }
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TEnemy"))
        {
            if (canDamage) //0.5초마다 데미지 4 부여
            {
            collision.gameObject.GetComponent<TEnemy>().GetDamage(4);
            canDamage = false;
            Invoke("canGiveDamage", 0.5f);
            }
        }
        else if (collision.gameObject.layer == 25) //Larry.Jr 한테만 별개로 적용
        {
            if (canDamage)
            {
                //인수를 사용하는 메소드가 BombDamage라서 일단 이거 사용
                collision.gameObject.GetComponentInParent<SnakeManager>().getBombDamage(8);
                canDamage = false;
                Invoke("canGiveDamage", 0.5f);
            }
        }
    }

    void canGiveDamage()
    {
        canDamage = true;
    }
}
