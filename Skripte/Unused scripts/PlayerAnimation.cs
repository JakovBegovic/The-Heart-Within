using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    Animator anim;
    Vector2 mouseDirection;
    float mouseAngle;
    float animDir;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        // vector of direction from camera to mouse
        // - transform.transition alters it so it is not from the camera but from the object
        // that this script belongs to

        mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg; 
        // angle between vector origin and destination

        if(mouseAngle > -45 && mouseAngle < 45)
        {
            animDir = 1; // EAST
        } else if(mouseAngle >= 45 && mouseAngle <= 135)
        {
            animDir = 2; // NORTH
        } else if(mouseAngle < -135 || mouseAngle > 135)
        {
            animDir = 3; // WEST
        } else if(mouseAngle >= -135 && mouseAngle <= -45)
        {
            animDir = 4; // SOUTH
        }


        anim.SetFloat("animDirection", animDir);

        ChangeAnim();
    }

    void ChangeAnim()
    {        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.Play("BT_player_walking_unarmed");
        }
        else
        {
            anim.Play("BT_player_idle_unarmed");
        }
    }
}
