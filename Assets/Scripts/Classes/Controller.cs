using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    // Hold our Pawn
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Set our Pawn's Controller to Me
        pawn.controller = this;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Process Input (Child Classes *Must* Override)
    public abstract void ProcessInputs();

}
