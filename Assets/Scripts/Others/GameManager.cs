using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static List<string> objectsToDestroy;
    public static string levelName { get; set; }
    public static int maxHealth { get; private set; }
    public static int health { get; private set; }
    public static int score { get; private set; }
    public static int lives { get; private set; }
    public static Vector2 lastCheckpoint { get; set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            StartGame();
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void SetItemEffect(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Score: { Setscore(); break; }
            case ItemType.Heal: { Sethealth(1); FindObjectOfType<AudioManager>().Play("ItemTake"); break; }
            case ItemType.MaxHealthUp: { SetMaxHealth(); FindObjectOfType<AudioManager>().Play("ItemTake"); break; }
            case ItemType.LvlUp: { Setlives(1); FindObjectOfType<AudioManager>().Play("ItemTake"); break; }
        }
    }

    public static void Setscore()
    {
        score++;
        if (score >= 100)
        {
            score = 0;
            Setlives(1);
        }
        FindObjectOfType<AudioManager>().Play("ScoreTake");
        LevelController.levelController.SetPlayerUIText();
    }
    
    public static void Sethealth(int hp)
    {
        health = health + hp;
        if (health > maxHealth) health = maxHealth;
        LevelController.levelController.SetPlayerUIText();
    }

    public static void Setlives(int Lives)
    {
        lives = lives + Lives;
        LevelController.levelController.SetPlayerUIText();
    }

    public static void SetMaxHealth()
    {
        maxHealth++;
        health = maxHealth;
        LevelController.levelController.SetPlayerUIText();
    }

    public static void StartLevel()
    {
        health = maxHealth;
        if (lastCheckpoint.x != 0 && lastCheckpoint.y != 0) FindObjectOfType<Player>().transform.position = lastCheckpoint;
        foreach (string name in objectsToDestroy)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null) Destroy(obj);
            else Debug.Log("Object not found: " + name);
        }
    }

    public static void StartGame()
    {
        objectsToDestroy = new List<string>();
        maxHealth = 3;
        health = maxHealth;
        score = 0;
        lives = 3;
    }

    public static void AddObjectsToDestroy(GameObject obj)
    {
        objectsToDestroy.Add(obj.name);
    }

    public static void ResetObjectsToDestroy()
    {
        objectsToDestroy = new List<string>();
    }
}
