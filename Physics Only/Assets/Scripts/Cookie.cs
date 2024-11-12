using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public Tagger[] AddOns;
    public List<GameObject> Yucky = new ();
    public List<GameObject> Dirty = new ();
    public List<GameObject> Ketchup = new();

    private int YuckContacted = 0;
    private int DirtContacted = 0;
    private int KetchupContacted = 0;


    private void Awake()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // if touches gunk
        {
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
            int i = Random.Range(1, 11);
            Ketchup[i].SetActive(true);
            KetchupContacted++;
        }

        if (collision.gameObject.layer == 10) // if touches milk
        {
            GameManager.instance.ChangeMilk(YuckContacted, DirtContacted, KetchupContacted);
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
}
