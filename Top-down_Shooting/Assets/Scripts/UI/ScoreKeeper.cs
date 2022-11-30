using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int score { get; private set; }
    float lastEnemyKillTime;
    int streakCount;
    float streakExpiryTime = 1;
    public static int haveCoin { get; set; }
    int coin = 10;
    public static int howManyKillEnemy { get; private set; }

    private void Start()
    {
        Enemy.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
        haveCoin = PlayerPrefs.GetInt("PlayerCoin");
    }
    
    void OnEnemyKilled()
    {
        howManyKillEnemy++;
        haveCoin += coin;
        PlayerPrefs.SetInt("PlayerCoin", haveCoin);

        if (Time.time < lastEnemyKillTime + streakExpiryTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }

        lastEnemyKillTime = Time.time;

        score += 5 + (int)Mathf.Pow(2, streakCount);
        PlayerPrefs.SetInt("EnemyKilled", howManyKillEnemy);
    }

    void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }
}
