using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    [SerializeField]
    GameObject[] DropItems; // 드랍아이템
    // 0 : 코인 , 1 : 하트 , 2 : 폭탄 , 3 : 열쇠

    [SerializeField]
    GameObject[] PassiveItems; // 패시브 아이템

    [SerializeField]
    GameObject[] TrinketItems; // 장신구 아이템

    [SerializeField]
    GameObject[] ActiveItems; // 액티브 아이템

    [SerializeField] private List<int> passive;
    [SerializeField] private List<int> trinket;
    [SerializeField] private List<int> active;

    [Header("Pooling")]
    [SerializeField] Transform dropItemPool_Transform;
    Stack<GameObject> coinPool = new Stack<GameObject>();
    Stack<GameObject> heartPool = new Stack<GameObject>();
    Stack<GameObject> bombPool = new Stack<GameObject>();
    Stack<GameObject> keyPool = new Stack<GameObject>();

    private void Start()
    {
        // 중복 드랍 방지용  초기화
        Itemduplication();
        SetPooling();
    }
    #region initialization
    void Itemduplication()
    {
        passive = new List<int>();
        trinket = new List<int>();
        active = new List<int>();
        for (int i = 0; i < PassiveItems.Length; i++)
            passive.Add(i);

        for (int i = 0; i < TrinketItems.Length; i++)
            trinket.Add(i);

        for (int i = 0; i < ActiveItems.Length-1; i++)
            active.Add(i);
    }

    void SetPooling()
    {
        // pool stack 초기화
        coinPool = new Stack<GameObject>();
        heartPool = new Stack<GameObject>();
        bombPool = new Stack<GameObject>();
        keyPool = new Stack<GameObject>();

        // 오브젝트 생성
        for(int i = 0; i < 40; i++)
        {
            CreateCoin();
            CreateHeart();
            CreateBomb();
            CreateKey();
        }
    }

    void CreateCoin()
    {
        GameObject coin = Instantiate(DropItems[0], dropItemPool_Transform.position, Quaternion.identity);
        coin.transform.SetParent(dropItemPool_Transform);
        coinPool.Push(coin);
        coin.SetActive(false);
    }

    void CreateHeart()
    {
        GameObject heart = Instantiate(DropItems[1], dropItemPool_Transform.position, Quaternion.identity);
        heart.transform.SetParent(dropItemPool_Transform);
        heartPool.Push(heart);
        heart.SetActive(false);
    }

    void CreateBomb()
    {
        GameObject bomb = Instantiate(DropItems[2], dropItemPool_Transform.position, Quaternion.identity);
        bomb.transform.SetParent(dropItemPool_Transform);
        bombPool.Push(bomb);
        bomb.SetActive(false);
    }

    void CreateKey()
    {
        GameObject key = Instantiate(DropItems[3], dropItemPool_Transform.position, Quaternion.identity);
        key.transform.SetParent(dropItemPool_Transform);
        keyPool.Push(key);
        key.SetActive(false);
    }
    #endregion

    #region pooling
    public GameObject GetDropItem(int index)
    {
        switch(index) 
        {
            #region 동전
            case 0:
                if(coinPool.Count == 0)
                {
                    CreateCoin();
                }
                GameObject coin = coinPool.Pop();
                coin.SetActive(true);
                coin.GetComponent<Coin>().SetCollisionDelay(true);
                return coin;
            #endregion
            #region 하트
            case 1:
                if (heartPool.Count == 0)
                {
                    CreateHeart();
                }
                GameObject heart = heartPool.Pop();
                heart.SetActive(true);
                heart.GetComponent<Heart>().SetCollisionDelay(true);
                return heart;
            #endregion
            #region 폭탄
            case 2:
                if (bombPool.Count == 0)
                {
                    CreateBomb();
                }
                GameObject bomb = bombPool.Pop();
                bomb.SetActive(true);
                bomb.GetComponent<DropBomb>().SetCollisionDelay(true);
                return bomb;
            #endregion
            #region 열쇠
            case 3:
                if (keyPool.Count == 0)
                {
                    CreateKey();
                }
                GameObject key = keyPool.Pop();
                key.SetActive(true);
                key.GetComponent<key>().SetCollisionDelay(true);
                return key;
            #endregion
        }
        return null;
    }
    
    public void Dropitem(Vector3 dropPosition, int itemCode)
    {
        GameObject dropItem;
        switch(itemCode)
        {
            case 0:
                dropItem = GetDropItem(itemCode); // 아이템 받아오기
                dropItem.transform.position = dropPosition;
                dropItem.GetComponent<Coin>().DropCoin();
                break;
            case 1:
                dropItem = GetDropItem(itemCode); // 아이템 받아오기
                dropItem.transform.position = dropPosition;
                dropItem.GetComponent<Heart>().DropHeart();
                break;
            case 2:
                dropItem = GetDropItem(itemCode); // 아이템 받아오기
                dropItem.transform.position = dropPosition;
                dropItem.GetComponent<DropBomb>().DropBomb_move();
                break;
            case 3:
                dropItem = GetDropItem(itemCode); // 아이템 받아오기
                dropItem.transform.position = dropPosition;
                dropItem.GetComponent<key>().DropKey();
                break;
            default:
                dropItem = GetDropItem(itemCode); // 아이템 받아오기
                dropItem.transform.position = dropPosition;
                dropItem.GetComponent<Coin>().DropCoin();
                break;
        }
    }

    public void ReturnDropItem(GameObject dropitem)
    {
        if (dropitem.GetComponent<Coin>() != null)
        {
            dropitem.GetComponent<Coin>().ResetObject();
            dropitem.SetActive(false);
            coinPool.Push(dropitem); 
        }
        else if (dropitem.GetComponent<Heart>() != null)
        {
            dropitem.GetComponent<Heart>().ResetObject();
            dropitem.SetActive(false);
            heartPool.Push(dropitem);
        }
        else if (dropitem.GetComponent<DropBomb>() != null)
        {
            dropitem.GetComponent<DropBomb>().ResetObject();
            dropitem.SetActive(false);
            bombPool.Push(dropitem);
        }
        else if (dropitem.GetComponent<key>() != null)
        {
            dropitem.GetComponent<key>().ResetObject();
            dropitem.SetActive(false);
            keyPool.Push(dropitem);
        }
        else
        {
            Destroy(dropitem);
        }
    }
    public void AllReturnDropItem()
    {
        for(int i = 0; i < dropItemPool_Transform.childCount; i++)
        {
            GameObject obj = dropItemPool_Transform.GetChild(i).gameObject;
            ReturnDropItem(obj);
        }
    }
    #endregion

    public GameObject DropTrinket()
    {
        GameObject obj;
        if (trinket.Count == 0)
        {
            if(passive.Count == 0)
            {
                if(active.Count == 0)
                {
                    obj = GetDropItem(3);
                    obj.GetComponent<key>().SetCollisionDelay(true);
                    return obj;
                }
                ShuffleList(ref active);
                obj = ActiveItems[active[0]];
                active.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref trinket);
        obj = TrinketItems[trinket[0]];
        trinket.RemoveAt(0);
        return obj;
    }

    public GameObject DropPassive()
    {
        GameObject obj;
        if (passive.Count == 0)
        {
            if (active.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = GetDropItem(3);
                    obj.GetComponent<key>().SetCollisionDelay(true);
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref active);
            obj = ActiveItems[active[0]];
            active.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref passive);
        obj = PassiveItems[passive[0]];
        passive.RemoveAt(0);
        return obj;
    }

    public GameObject DropActive()
    {
        GameObject obj;
        if (active.Count == 0)
        {
            if (passive.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = GetDropItem(3);
                    obj.GetComponent<key>().SetCollisionDelay(true);
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }
            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }
        ShuffleList(ref active);
        obj = ActiveItems[active[0]];
        active.RemoveAt(0);
        return obj;
    }

    public GameObject OpenGoldChest()
    {
        int rd = Random.Range(0, 10000);
        int n = rd % 100;

        if (0 <= n && n <= 49)
            return DropTrinket();

        else if (50 <= n && n <= 75)
            return DropPassive();

        else
            return DropActive();
    }

    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }

    public GameObject ActiveChange(int itemCode)
    {
        return ActiveItems[itemCode];
    }
    private void ShuffleList(ref List<int> list)
    {
        int rd1, rd2;
        int tmp;

        for(int i = 0; i < list.Count; i++)
        {
            rd1 = Random.Range(0, list.Count);
            rd2 = Random.Range(0, list.Count);

            tmp = list[rd1];
            list[rd1] = list[rd2];
            list[rd2] = tmp;
        }
    }
   
    public Sprite GetStuffImage(int index)
    {
        return PassiveItems[index].GetComponent<SpriteRenderer>().sprite;
    }



    #region archive
    public GameObject ObjectBreak() // 오브젝트 부쉈을때
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // 랜텀 아이템 반환 ( 열쇠 제외 )
    }

    public GameObject OpenNormalChest(int rd)
    {
        //return GetDropItem(rd);
        return DropItems[rd];
    }

    #endregion
}