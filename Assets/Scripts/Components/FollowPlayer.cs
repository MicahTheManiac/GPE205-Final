using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTf;
    public Vector3 offset = new Vector3(0, 20, -15);


    // Update is called once per frame
    void Update()
    {
        if (playerTf != null)
        {
            transform.position = playerTf.position + offset;
        }
    }
}
