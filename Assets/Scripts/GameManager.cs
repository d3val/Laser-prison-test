using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game variables
    [Header("Game parameters")]
    [SerializeField] int playerLifes = 3;
    private int currentScore = 0;
    private int scoreMultiplier = 1;
    private bool isPlaying = true;
    private float rawScore;

    [Header("UI")]
    //UI variables
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(IncreaseDificulty(15, 2));
        StartCoroutine(IncreaseDificulty(40, 3));
        StartCoroutine(IncreaseDificulty(90, 5));
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        if (playerLifes <= 0)
            EndGame();

        rawScore += Time.deltaTime * scoreMultiplier;
        currentScore = (int)rawScore;
        scoreText.text = currentScore.ToString();
        Debug.Log(scoreMultiplier);
    }

    private void EndGame()
    {
        isPlaying = false;
    }

    private IEnumerator IncreaseDificulty(float time, int newScoreMultiplier)
    {
        yield return new WaitForSeconds(time);
        scoreMultiplier = newScoreMultiplier;
        multiplierText.text = "X"+newScoreMultiplier;
    }

    public void DecreaseLife()
    {
        playerLifes--;
    }
}
