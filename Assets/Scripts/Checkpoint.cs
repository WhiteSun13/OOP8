using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool active;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && !active)
        {
            GameManager.lastCheckpoint = transform.position;
            FindObjectOfType<AudioManager>().Play("Checkpoint");
            GetComponent<Animator>().SetTrigger("Active");
            active = true;
        }
    }
}
