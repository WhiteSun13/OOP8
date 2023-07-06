using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPanel : MonoBehaviour
{
    private Animator anim;
    public bool noLoading = false;
    public bool resetPlayerStartPosition = false;
    public string level;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnFadeComplete()
    {
        if (resetPlayerStartPosition)
        {
            GameManager.lastCheckpoint = new Vector2(0, 0);
            GameManager.ResetObjectsToDestroy();
        }
        if (noLoading) SceneManager.LoadScene(level);
        else
        {
            GameManager.levelName = level;
            SceneManager.LoadScene("Loading");
        }
    }
}
