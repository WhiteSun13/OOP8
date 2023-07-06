using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeadCheck : MonoBehaviour
{
    public GameObject deadCheck;
    void Update()
    {
        if (deadCheck == null) Destroy(gameObject);
    }
}
