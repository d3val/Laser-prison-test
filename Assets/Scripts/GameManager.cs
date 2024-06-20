using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Game variables
    [Header("Game parameters")]
    [SerializeField] int playerLifes = 3;
    private int currentScore = 0;
    private int scoreMultiplier = 1;
    private bool isPlaying = true;
    private float rawScore;
    [SerializeField] LaserManager laserManager;

    [Header("UI")]
    //UI variables
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] List<GameObject> lifeIcons;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI FinalScoreText;
    private int lifeIconsIndex = 0;

    // Class instance
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        Time.timeScale = 1;
    }

    private void Start()
    {
        StartCoroutine(IncreaseDificulty(15, 2, 2));
        StartCoroutine(IncreaseDificulty(40, 3, 3));
        StartCoroutine(IncreaseDificulty(90, 5, 6));
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        //When player loses her lifes, the game is over.
        if (playerLifes <= 0)
            EndGame();

        //Score calculation
        rawScore += Time.deltaTime * scoreMultiplier;
        currentScore = (int)rawScore;
        scoreText.text = currentScore.ToString();
    }

    //Sets some parameters to end the game
    private void EndGame()
    {
        isPlaying = false;
        GameOverPanel.SetActive(true);
        FinalScoreText.text = currentScore.ToString();
        Time.timeScale = 0;
    }

    //Used to increase the difficulty of the game
    private IEnumerator IncreaseDificulty(float time, int newScoreMultiplier, int lasersAdded)
    {
        yield return new WaitForSeconds(time);
        scoreMultiplier = newScoreMultiplier;
        multiplierText.text = "X" + newScoreMultiplier;
        laserManager.IncreaseLasersPerShoot(lasersAdded);
    }

    //Substacts one life.
    public void DecreaseLife()
    {
        if (lifeIconsIndex >= lifeIcons.Count)
        {
            Debug.LogError("lifeIconsIndex out of range");
            return;
        }

        playerLifes--;
        lifeIcons[lifeIconsIndex].SetActive(false);
        lifeIconsIndex++;
    }

    //Restarts the scene to its original state.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
