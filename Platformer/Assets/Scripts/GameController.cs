using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score;
    public Text scoreText;


    
    
    
    public static GameController instance;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }else{
            Destroy(gameObject);
        }
        
    }

    
    public void GetCoin()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(1);
    }
}
