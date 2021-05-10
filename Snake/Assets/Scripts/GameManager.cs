using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action onScoreChanged;
    public PlayerController pc;
    public int score;
    public int maxScore = 25;
    public TextMeshProUGUI scoreUI;

    #region Singleton
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    // Add score and invoke all the subscribed events
    public void AddScore(int amount)
    {
        score += amount;
        scoreUI.text = score.ToString();
        onScoreChanged?.Invoke();
        if (score >= maxScore) pc.Die();
    }

    // Exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
