using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int lives = 20;
    public int money = 100;

    public Text moneyText;
    public Text livesText;

    public void LoseLife(int l = 1)
    {
        lives -= 1;
        if(lives <= 0)
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
    }
}
