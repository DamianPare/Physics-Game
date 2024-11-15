using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip splash;
    [SerializeField] private AudioClip splat;
    [SerializeField] private AudioClip thud;
    [SerializeField] private AudioClip explosion;

    private Tagger[] AddOns;
    private List<GameObject> Yucky = new ();
    private List<GameObject> Dirty = new ();
    private List<GameObject> Ketchup = new();

    public float YuckContacted = 0;
    public float DirtContacted = 0;
    public float KetchupContacted = 0;

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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            source.PlayOneShot(thud);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // if touches gunk
        {
            YuckContacted++;
            GameManager.instance.RemoveEdibility();
            source.PlayOneShot(splat);
            int i = Random.Range(1, 14);

            while (Yucky[i].activeSelf == true)
            {
                i = Random.Range(1, 14);
            }

            if (Yucky[i].activeSelf == false)
            {
                Yucky[i].SetActive(true);
            }
        }

        if (collision.gameObject.layer == 8) // if touches dirt
        {
            DirtContacted++;
            GameManager.instance.RemoveEdibility();
            int i = Random.Range(1, 14);

            while (Dirty[i].activeSelf == true)
            {
                i = Random.Range(1, 14);
            }

            if (Dirty[i].activeSelf == false)
            {
                Dirty[i].SetActive(true);
            }
        }

        if (collision.gameObject.layer == 9) // if touches ketchup
        {
            KetchupContacted++;
            source.PlayOneShot(splat);
            GameManager.instance.RemoveEdibility();
            int i = Random.Range(1, 14);

            while (Ketchup[i].activeSelf == true)
            {
                i = Random.Range(1, 14);
            }

            if (Ketchup[i].activeSelf == false)
            {
                Ketchup[i].SetActive(true);
            }
        }

        if (collision.gameObject.layer == 10) // if touches milk
        {
            source.PlayOneShot(splash);
            CalculateColor();
            GameManager.instance.LevelFinished(KetchupContacted, YuckContacted, DirtContacted);
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
        float total = YuckContacted + DirtContacted + KetchupContacted;

        Color yResult = Color.white;
        Color dResult = Color.white;
        Color kResult = Color.white;

        if (YuckContacted > 0)
        {
            yResult = yuckColor * (YuckContacted / total);
        }

        if (DirtContacted > 0)
        {
            dResult = dirtColor * (DirtContacted / total);
        }

        if (KetchupContacted > 0)
        {
            kResult = chupColor * (KetchupContacted / total);
        }

        GameManager.instance.ChangeMilk(yResult, dResult, kResult);
    }

    public void Explosion(float power)
    {
        source.PlayOneShot(explosion, power);
    }
}
