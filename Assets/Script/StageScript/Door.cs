using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform movePosition;
    public GameObject roomInfo;
    public int doorDir = -1;

    [Header("Unity Setup")]
    public GameObject closeDoor;
    public GameObject openDoor;
    public bool doorKey;

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
            }
        }
    }
}
