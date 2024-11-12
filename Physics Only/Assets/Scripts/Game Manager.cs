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

    [SerializeField] private GameObject Milk;

    [SerializeField] private float duration;

    private float timeElapsed;

    public bool LevelCompleted;

    private Color32 resultColor;
    private Color32 milkColor;
    private SpriteRenderer milkRenderer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelCompleted = false;
        milkRenderer = Milk.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log(resultColor);

        if (LevelCompleted && Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
    } 

    IEnumerator ChangeColor()
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            milkColor = Color32.Lerp(Color.white, resultColor, timeElapsed / duration);
            milkRenderer.color = milkColor;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        milkColor = resultColor;
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

    public void ChangeMilk(Color Goop, Color Dirt, Color Ketchup)
    {
        resultColor = (Goop + Dirt + Ketchup)/3;

        StartCoroutine(ChangeColor());
    }

    private void GameCompleted()
    {

    }
}
