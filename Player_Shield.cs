using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Shield : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color defColor;
    private bool shield = false;
    private int health = 10;
    private float shieldTime;
    private float healthTime;
    private string colName;
   
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defColor = sr.color;
    }
     
    public void Shield()
    {
        // set players color with shield
        sr.color = new Color(3, 0, 0);

        // destroy stawberry
        Destroy(GameObject.Find(colName));

        // put shield on and reset count
        shield = true;
        shieldTime = 0;
    }

    private void HealthDecrease()
    {
        // minus health and reset healthTime count
        health -= 5;
        healthTime = 0;

        // check health amount, game over when health is 0
        if (health <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        colName = col.gameObject.name;

        // if player hits strawberry put shield on
        if (colName.StartsWith("strawberry"))
        {
            Shield();
        }

        // if player hits bee without shield on, it will loose health. healthTime so it cannot loose health immeaditely again
        if (colName.StartsWith("bee") && shield == false && healthTime > 2)
        {
            HealthDecrease();
        }
    }
    void Update()
    {
        if(shield == true) { shieldTime += Time.deltaTime; }
        healthTime += Time.deltaTime;

        // set shield off and put normal color after a while from changing
        if (shieldTime >= 4 && shield == true)
        {
            shield = false;
            sr.color = defColor;
        }
    }
}



