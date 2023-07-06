using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Panel;
    public static bool isPaused { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isPaused)
            {
                isPaused = true;
                FindObjectOfType<AudioManager>().Stop("Main");
                FindObjectOfType<AudioManager>().Play("Pause");
                Time.timeScale = 0f;
                Panel.SetActive(true);
            }
            else
            {
                isPaused = false;
                FindObjectOfType<AudioManager>().Stop("Pause");
                FindObjectOfType<AudioManager>().Play("Main");
                Time.timeScale = 1f;
                Panel.SetActive(false);
            }
            
        }
    }
}
