using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Teleport : MonoBehaviour
{
    public Transform pointToTeleport;
    private Player player;
    private void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.W))
        {
            player.transform.position = pointToTeleport.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player = null;
    }
}
