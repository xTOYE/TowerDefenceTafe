using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    #region Singleton
    public static ScoreManager Instance = null;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public int lives = 20;
    public int money = 100;
    public int current = 1;
    public int highest = 1;

    public Text moneyText;
    public Text livesText;
    public Text currentText;
    public Text highestText;

    public void LoseLife(int l = 1)
    {
        //how to lose a life
        lives -= 1;
        //checking amount of lives
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        moneyText.text = "Money: $" + money.ToString();
        livesText.text = "Lives: " + lives.ToString();
        currentText.text = "Current: " + current.ToString();
        //highestText.text = "Highest: " + highest.ToString();
    }
}
