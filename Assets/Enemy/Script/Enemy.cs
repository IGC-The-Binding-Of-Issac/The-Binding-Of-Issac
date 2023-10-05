using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    protected string dieParameter;

    [Header("Enemy State")]
    public bool playerInRoom;
    [SerializeField] protected float hp;
    protected float sight;
    protected float searchDelay;
    protected float moveSpeed;
    protected float bulletSpeed;
    protected float attaackSpeed;
    protected float waitforSecond;

    protected Transform playerPos; //범위 내 플레이어 위치
    // Move 하위 스크립트에서 구현
    public virtual void Move() { }

    // 초기화
   
    //플레이어 search
    protected bool PlayerSearch()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 sightSize = new Vector2(x, y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sightSize, sight); //시작 위치 , 범위

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                playerPos = colliders[i].transform;
                return true;
            }
        }
        return false;
    }

    //collider검사
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어랑 부딪히면 플레이어의 hp감소
            PlayerManager.instance.GetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tears"))
        {
            Color oriColor = gameObject.GetComponent<SpriteRenderer>().color;
            StartCoroutine(Hit(oriColor));
        }
    }

    IEnumerator Hit(Color oriColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = oriColor;
    }

    // 데미지
    public void GetDamage(float damage) //Tears 에서 사용 (player대미지 만큼 피 깎음)
    {
        hp -= damage;
        if (IsDead())
        {
            DeadAction(dieParameter);
        }
    }

    // hp검사
    bool IsDead()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }

    void DeadAction(string ani)
    {
        gameObject.GetComponent<Animator>().SetBool(ani, true);
        Destroy(gameObject , waitforSecond);
    }


}
