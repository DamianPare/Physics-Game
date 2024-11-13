using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private string Level1Name;
    [SerializeField] private string Level2Name;
    [SerializeField] private string Level3Name;

    [SerializeField] private GameObject Milk;
    [SerializeField] private GameObject completedScreen;
    [SerializeField] private TMP_Text Ketchup;
    [SerializeField] private TMP_Text Relish;
    [SerializeField] private TMP_Text Dirt;

    [SerializeField] private GameObject lostScreen;

    [SerializeField] private float duration;

    private float timeElapsed;
    private int Edibility;

    public bool LevelCompleted;
    public bool LevelFailed;

    private Color32 resultColor;
    private Color32 milkColor;
    private SpriteRenderer milkRenderer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Edibility = 100;
        LevelCompleted = false;
        LevelFailed = false;
        milkRenderer = Milk.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Edibility <= 0)
        {
            LevelFailed = true;
            EndGame();
        }

        if (LevelCompleted && Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
    } 

    IEnumerator ChangeColor()
    {
        float timeElapsed = 0;
        resultColor.a = 255;

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
        
        Debug.Log("Goop: " + Goop + "|  Dirt: " + Dirt + "|  Ketchup: " + Ketchup);
        resultColor = (Goop + Dirt + Ketchup)/3;
        StartCoroutine(ChangeColor());
        Debug.Log(resultColor);
    }

    private void GameCompleted()
    {
        //completedScreen.SetActive(true);
    }

    public void LevelFinished(float ketch, float goop, float dirt)
    {
        LevelCompleted = true;
        Ketchup.text = ketch.ToString();
        Relish.text = goop.ToString();
        Dirt.text = dirt.ToString();
        completedScreen.SetActive(true);
    }

    public void RemoveEdibility()
    {
        //Edibility -= 10;
        Debug.Log(Edibility);
    }

    private void EndGame()
    {
        //lostScreen.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
