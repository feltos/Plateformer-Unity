using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x,player.position.x,0.75f),Mathf.Lerp(transform.position.y,player.position.y,0.75f),-10.0f);
    }
    
}
