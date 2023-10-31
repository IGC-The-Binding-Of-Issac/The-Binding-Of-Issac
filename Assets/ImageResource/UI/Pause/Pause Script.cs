using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseObj;
    bool OnScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnScreen)
            {
                PauseObj.SetActive(false);
                OnScreen = false;
            }else
            {
                PauseObj.SetActive(true);
                OnScreen = true;
            }
        }
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene("01_Intro");
        PauseObj.SetActive(false);
    }
    public void ResumeBtn()
    {
        PauseObj.SetActive(false);
    }
}
