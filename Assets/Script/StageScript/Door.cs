using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform movePosition;
    public GameObject roomInfo;
    public int doorDir = -1;
    public float doorDamage;

    [Header("Unity Setup")]
    public GameObject closeDoor;
    public GameObject openDoor;
    public bool doorKey; // 열쇠가 필요한문은 체크 해제 ( false )  열쇠가 필요없는 문은 체크 ( true )

    public void CheckedClear()
    {
        // door key : true :  usingKey / normal room / boss room
        if(roomInfo.GetComponent<Room>().isClear && doorKey)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void UsingKey()
    {
        doorKey = true;
    }

    public void OpenDoor()
    {
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
    }
    public void CloseDoor()
    {   
        closeDoor.SetActive(true);
        openDoor.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(roomInfo.GetComponent<Room>().isClear) // 방이 클리어된상태 일때
        {
            if(collision.gameObject.CompareTag("Player") && doorKey) // 문에 부딪힌 대상이 플레이어라면
            {
                if (Input.GetKey(KeyCode.W) && doorDir == 0)
                {
                    collision.transform.position = movePosition.transform.position; // 플레이어를 이동
                }
                else if (Input.GetKey(KeyCode.D) && doorDir == 1)
                {
                    collision.transform.position = movePosition.transform.position; // 플레이어를 이동
                }
                else if (Input.GetKey(KeyCode.S) && doorDir == 2)
                {
                    collision.transform.position = movePosition.transform.position; // 플레이어를 이동
                }
                else if (Input.GetKey(KeyCode.A) && doorDir == 3)
                {
                    collision.transform.position = movePosition.transform.position; // 플레이어를 이동
                }

                if(doorDamage != 0)
                {
                    PlayerManager.instance.GetDamage();
                }
            }

            // 열쇠를 사용해야하는 문일때
            else if(collision.gameObject.CompareTag("Player") && !doorKey && ItemManager.instance.keyCount > 0)
            {
                    // 해당방향으로 방향키를 한번더 누르면 키 사용.
                if (Input.GetKey(KeyCode.W) && doorDir == 0)
                {
                     UsingKey();
                    ItemManager.instance.keyCount--;
                    roomInfo.GetComponent<Room>().DoorSound(2);
                }
                else if (Input.GetKey(KeyCode.D) && doorDir == 1)
                {
                    UsingKey();
                    ItemManager.instance.keyCount--;
                    roomInfo.GetComponent<Room>().DoorSound(2);
                }
                else if (Input.GetKey(KeyCode.S) && doorDir == 2)
                {
                    UsingKey();
                    ItemManager.instance.keyCount--;
                    roomInfo.GetComponent<Room>().DoorSound(2);
                }
                else if (Input.GetKey(KeyCode.A) && doorDir == 3)
                {
                    UsingKey();
                    ItemManager.instance.keyCount--;
                    roomInfo.GetComponent<Room>().DoorSound(2);
                }
            }
        }
    }
}
