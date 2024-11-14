using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMusic : MonoBehaviour
{
    public SingletonMusic _instance { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       // DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
