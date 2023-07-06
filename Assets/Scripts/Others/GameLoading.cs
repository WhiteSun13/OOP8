using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour
{
    public KeyCode _keyCode = KeyCode.Space;
    public GameObject zoneInfo, zoneNomber;
    private AsyncOperation async;

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(GameManager.levelName);
        zoneInfo.GetComponent<Text>().text = GameManager.levelName;
        zoneNomber.GetComponent<Text>().text = $"ZONE - {SceneManager.GetSceneByName(GameManager.levelName).buildIndex}";
        yield return true;
        async.allowSceneActivation = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyCode)) async.allowSceneActivation = true;
    }
}
