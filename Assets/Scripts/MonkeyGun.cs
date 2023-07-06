using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyGun : MonoBehaviour
{
    public float offset;
    private Vector3 difference;
    private Player player;
    private float rotZ;
    public float minRotZ = 10;
    public float maxRotZ = -45;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if ((rotZ <= minRotZ && rotZ >= maxRotZ) || (rotZ >= -(minRotZ + 180) && rotZ <= -(maxRotZ + 180)))
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        else if (difference.x < 0)
            transform.rotation = Quaternion.Euler(0f, 0f, -(maxRotZ + 180) + offset);
        else if (difference.x > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotZ + offset);
    }
}
