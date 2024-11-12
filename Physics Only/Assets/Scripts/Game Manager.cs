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

    private void Awake()
    {
        instance = this;
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().name == Level1Name)
        {
            SceneManager.LoadScene("Level2Name");
        }
        if (SceneManager.GetActiveScene().name == Level2Name)
        {
            SceneManager.LoadScene("Level3Name");
        }
        if (SceneManager.GetActiveScene().name == Level3Name)
        {
            GameCompleted();
        }
    }

    public void ChangeMilk(int Goop, int Dirt, int Ketchup)
    {

    }

    private void GameCompleted()
    {

    }
}
