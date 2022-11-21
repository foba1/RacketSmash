using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManager : MonoBehaviour
{
    [Header("Health")]
    private int health;

    static CrazyManager instance;
    public static CrazyManager Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = 3;
    }

    public void FailToReceiveBall()
    {
        if (health > 1)
        {
            health--;
            UpdateHealth();
        }
        else
        {
            health = 0;
            GameOver();
        }
    }

    private void UpdateHealth()
    {

    }

    private void GameOver()
    {

    }
}
