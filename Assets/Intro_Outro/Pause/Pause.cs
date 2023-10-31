using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool turnScreen;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        turnScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        PauseFunction();
    }
    void PauseFunction()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (turnScreen)
            {
                pausePanel.SetActive(true);
                turnScreen = false;
            }
            else
            {
                pausePanel.SetActive(false);
                turnScreen = true;
            }
        }
        
    }

    public void ResumeBtn()
    {
        pausePanel.SetActive(false);
    }
    public void ExitBtn()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene("01_Intro");
    }
}