using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Credits (Score)
    public int credits;

    // Awake event before Start can run
    private void Awake()
    {
        if (instance == null)
        {
            // This is the instance
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
