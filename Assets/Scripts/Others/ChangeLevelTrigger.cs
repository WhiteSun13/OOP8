using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelTrigger : MonoBehaviour
{
    public bool gameOver;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            if (gameOver) LevelController.levelController.GameOver();
            else LevelController.levelController.ChangeLevel();
        }
    }
}
