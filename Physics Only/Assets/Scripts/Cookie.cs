using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip splash;
    [SerializeField] private AudioClip thud;
    [SerializeField] private AudioClip explosion;

    private Tagger[] AddOns;
    private List<GameObject> Yucky = new ();
    private List<GameObject> Dirty = new ();
    private List<GameObject> Ketchup = new();

    public int YuckContacted = 0;
    public int DirtContacted = 0;
    public int KetchupContacted = 0;

    public Color yuckColor;
    public Color dirtColor;
    public Color chupColor;

    public static Cookie instance;

    private void Awake()
    {
        instance = this;

        AddOns = GetComponentsInChildren<Tagger>();
        foreach (Tagger mess in AddOns)
        {
            switch (mess.myTag)
            {
                case "Goop":
                    this.Yucky.Add(mess.gameObject);
                    break;
                case "Dust":
                    this.Dirty.Add(mess.gameObject);
                    break;
                case "Chup":
                    this.Ketchup.Add(mess.gameObject);
                    break;
                default:
                    Debug.LogError("Unhandled child tag: " + mess.myTag);
                    break;
            }
        }
    }

    private void Start()
    {
        ToggleOff();
    }

    private void Update()
    {
        if(GameManager.instance.LevelCompleted)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // if touches gunk
        {
            source.PlayOneShot(splash);
            int i = Random.Range(1, 11);
            Yucky[i].SetActive(true);
            YuckContacted++;
        }

        if (collision.gameObject.layer == 8) // if touches dirt
        {
            int i = Random.Range(1, 11);
            Dirty[i].SetActive(true);
            DirtContacted++;
        }

        if (collision.gameObject.layer == 9) // if touches ketchup
        {
            source.PlayOneShot(splash);
            int i = Random.Range(1, 11);
            Ketchup[i].SetActive(true);
            KetchupContacted++;
        }

        if (collision.gameObject.layer == 10) // if touches milk
        {
            source.PlayOneShot(splash);
            CalculateColor();
        }

        if (collision.gameObject.layer == 11) // if touches milk
        {
            source.PlayOneShot(thud);
        }
    }

    private void ToggleOff()
    {
        foreach (GameObject goop in Yucky)
        {
            goop.SetActive(false);
        }
        foreach (GameObject dust in Dirty)
        {
            dust.SetActive(false);
        }
        foreach (GameObject chup in Ketchup)
        {
            chup.SetActive(false);
        }
    }

    private void CalculateColor()
    {
        yuckColor *= YuckContacted;
        dirtColor *= DirtContacted;
        chupColor *= KetchupContacted;
        GameManager.instance.ChangeMilk(yuckColor, dirtColor, chupColor);
    }

    public void Explosion(float power)
    {
        source.PlayOneShot(explosion, power);
    }
}
