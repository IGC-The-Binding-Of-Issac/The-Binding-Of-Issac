using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoMove : MonoBehaviour
{
    //UD_Floor
    float initPositionY;
    public float distance;
    public float turningPoint;
    //UD_Floor & LR_Floor
    public bool turnSwitch;
    public float moveSpeed;

    private void Awake()
    {
        initPositionY = transform.position.y;
        turningPoint = initPositionY - distance;
    }
    void Update()
    {
        // 현재 씬이 01_Intro 일때
        if (SceneManager.GetActiveScene().name == "01_Intro")
        {
            // Enter키 눌렀을때
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("02_Game");
            }
        }
        upDown();
    }

    void upDown()
    {
        float currentPositionY = transform.position.y;

        if (currentPositionY >= initPositionY)
        {
            turnSwitch = false;
        }
        else if (currentPositionY <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
        }
    }
}
