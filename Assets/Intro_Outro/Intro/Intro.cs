using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    void Update()
    {
        // 현재 씬이 01_Intro 일때
        if(SceneManager.GetActiveScene().name == "01_Intro")
        {
            // Enter키 눌렀을때
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("02_Game");
            }
        }
    }
}
