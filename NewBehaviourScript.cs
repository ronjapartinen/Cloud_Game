using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Color alphaColor;
    private float horizontal;
    private float fallTime;
    private float fadeOnTime;
    private float fade;
    private int speed = 8;
    private int jumpHeight = 15;
    private int fadeSpeed = 2;
    private bool fades = false;
    private string colName;
    private string fadeObject;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // start jump for the player
        rb.AddForce(transform.up * 15f, ForceMode2D.Impulse);
    }
    private void Move()
    {
        // set movement for the player to 
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        // Make player jump, for smoothness ForceMode2D.Impulse. Set fades on and cloud that player jumps from to fadeObject 
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        fades = true;
        fadeObject = colName;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // set collided objects name to colName & reset fallTime
        colName = col.gameObject.name;
        if (colName.StartsWith("clo")) { fallTime = 0;  }
    }
    private void Fade()
    {
        // fades, sets new color to cloud with fadeSpeed, add on fadeOnTime count 
        fadeOnTime += Time.deltaTime;
        alphaColor.a = 0;
        alphaColor = GameObject.Find(fadeObject).GetComponent<SpriteRenderer>().material.color;
        fade = alphaColor.a - (fadeSpeed * Time.deltaTime);
        alphaColor = new Color(alphaColor.r, alphaColor.g, alphaColor.b, fade);
        GameObject.Find(fadeObject).GetComponent<Renderer>().material.color = alphaColor;       
    }
    
    void Update()
    {
        Move();
        
        // Destroy object after it faded, reset fadeOnTime
        if (fadeOnTime >= 2)
        {
            Destroy(GameObject.Find(fadeObject));
            fadeOnTime = 0;
        }

        // if player is on the clouds edgecollider, jump
        if (rb.velocity.y == 0)
        {
            Jump();    
        }

        if (fades) { Fade(); }

        // if player is going down, add fallTime
        if (rb.velocity.y < 0) { fallTime += Time.deltaTime; }
        
        // if player hits the ground or has been falling for 3s, game over
        if (colName.StartsWith("ground") || fallTime >= 3 )
        {
            Time.timeScale = 0;
        }                                      
    }
}