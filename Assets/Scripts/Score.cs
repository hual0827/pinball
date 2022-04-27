using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    int score;
    int highScore;

    void Awake()
    {
        instance = this;
    }

    public int ReadScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void ResetScore()
    {
        score = 0;
    }
    public int ReadHighScore()
    {
        return highScore;
    }
    public void SetHighScore(int hScore)
    {
        highScore = hScore;
    }
}
