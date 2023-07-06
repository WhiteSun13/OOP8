using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{
    public GameObject loadPanel;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) anim.SetTrigger("SkipIntro");
    }
    public void PlayGame()
    {
        GameManager.StartGame();
        loadPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
