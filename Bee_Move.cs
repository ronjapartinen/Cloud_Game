using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bee_Move : MonoBehaviour
{
    private float scale = 10f;
    private float speed = 1f;
    private float startY = 20; 
    
    void Start()
    {
        // set starting positon to bee
        startY = transform.position.y;
    }

    void Update()
    {
        //set new position to bee to go up and down
        float newY = startY + scale * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(transform.position.x, newY); 
    }
}
