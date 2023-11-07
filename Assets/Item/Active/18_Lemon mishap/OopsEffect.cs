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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canDamage)
            {
            collision.gameObject.GetComponent<Enemy>().GetDamage(8);
            canDamage = false;
            Invoke("canGiveDamage", 0.5f);
            }
        }
        else if (collision.gameObject.layer == 25)
        {
            if (canDamage)
            {
                //�μ��� ����ϴ� �޼ҵ尡 BombDamage�� �ϴ� �̰� ���
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