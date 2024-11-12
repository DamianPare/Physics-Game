using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private string Level1Name;
    [SerializeField] private string Level2Name;
    [SerializeField] private string Level3Name;

    private bool LevelCompleted;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelCompleted = false;
    }

    private void Update()
    {
        Debug.Log(LevelCompleted);

        if (LevelCompleted && Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        Debug.Log(Level1Name);
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == Level1Name)
        {
            SceneManager.LoadScene(Level2Name);
        }
        if (SceneManager.GetActiveScene().name == Level2Name)
        {
            SceneManager.LoadScene(Level3Name);
        }
        if (SceneManager.GetActiveScene().name == Level3Name)
        {
            GameCompleted();
        }
    }

    public void ChangeMilk(int Goop, int Dirt, int Ketchup)
    {
        LevelCompleted = true;
    }

    private void GameCompleted()
    {

    }
}
