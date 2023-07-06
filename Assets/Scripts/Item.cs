using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score,
    Heal,
    MaxHealthUp,
    LvlUp
}
public class Item : MonoBehaviour
{
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            GameManager.SetItemEffect(itemType);
            GameManager.AddObjectsToDestroy(gameObject);
            Destroy(gameObject);
        }
    }
}
