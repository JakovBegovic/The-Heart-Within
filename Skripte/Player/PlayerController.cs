using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Movement
    Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 5;
    float inputVert;
    float inputHor;
    Vector2 moveVector;

    // Animation
    Animator anim;
    Vector2 mouseDirection;
    float mouseAngle;
    float animDir;
    [SerializeField] private SimpleFlash flashEffect;

    // Weaponry
    bool playerHasGun = true; // for now never changed
    public GameObject GunPosition;
    Vector3 gunPositionEast;
    Vector3 gunPositionNorth;
    Vector3 gunPositionWest;
    Vector3 gunPositionSouth;

    // Combat
    public float maxLifePoints;
    public float lifePoints;
    public float timeBetweenHits;
    private float timeOfNextHit;

    // Sound
    private AudioSource audioSource;


    void Start()
    {
        // Movement
        rb = GetComponent<Rigidbody2D>();

        // Animation
        anim = GetComponent<Animator>();

        // Weaponry
        //GunPosition.SetActive(false); for a later date
        gunPositionEast = new Vector3(0.142f, 0.11f, 0f);
        gunPositionNorth = new Vector3(0.096f, 0.179f, 0f);
        gunPositionWest = new Vector3(-0.142f, 0.11f, 0f);
        gunPositionSouth = new Vector3(-0.128f, -0.083f, 0f);

        // Combat
        lifePoints = maxLifePoints;
        timeOfNextHit = 0;

        // Sound
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void FixedUpdate()
    {        
        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);

    }

    private void HandleMovement()
    {

        inputVert = Input.GetAxisRaw("Vertical");
        inputHor = Input.GetAxisRaw("Horizontal");

        moveVector = new Vector2(inputHor, inputVert).normalized * movementSpeed;

    }


    void HandleAnimation()
    {
        if (playerHasGun)
        {
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // vector of direction from camera to mouse
            // - transform.transition alters it so it is not from the camera but from the object
            // that this script belongs to

            mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            // angle between vector origin and destination

            animDir = CalculateAnimDir(); 

        }
        else
        {
            if(inputVert == 1) 
            {
                animDir = 2; // NORTH
            } 
            else if(inputVert == -1) 
            { 
                animDir = 4; // SOUTH
            } 
            else if(inputHor == 1)
            {
                animDir = 1; // EAST
            }
            else if(inputHor == -1)
            {
                animDir = 3; // WEST
            }

        }
        
        anim.SetFloat("animDirection", animDir);

        ChangeAnim(); // change animation from idle to walking
    }

    int CalculateAnimDir()
    {

        if (mouseAngle > -45 && mouseAngle < 45)
        {
            GunPosition.transform.localPosition = gunPositionEast;

            return 1; // EAST
        }
        else if (mouseAngle >= 45 && mouseAngle <= 135)
        {
            GunPosition.transform.localPosition = gunPositionNorth;

            return 2; // NORTH
        }
        else if (mouseAngle < -135 || mouseAngle > 135)
        {
            GunPosition.transform.localPosition = gunPositionWest;

            return 3; // WEST
        }
        else if (mouseAngle >= -135 && mouseAngle <= -45)
        {
            GunPosition.transform.localPosition = gunPositionSouth;

            return 4; // SOUTH
        }
        else
        {
            return 0;
        }
    }

    void ChangeAnim()
    {
        if (playerHasGun)
        {
            if (inputHor != 0 || inputVert != 0)
            {
                anim.Play("BT_player_walking_armed");
            }
            else
            {
                anim.Play("BT_player_idle_armed");
            }
        } else
        {
            if (inputHor != 0 || inputVert != 0)
            {
                anim.Play("BT_player_walking_unarmed");
            }
            else
            {
                anim.Play("BT_player_idle_unarmed");
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("took damage");
        if(timeOfNextHit <= Time.time) // Immunity frames
        {
            audioSource.Play();
            flashEffect.Flash();

            //Debug.Log("took " + damage);
            timeOfNextHit = Time.time + timeBetweenHits;

            lifePoints -= damage;
            if(lifePoints <= 0)
            {
                SceneManager.LoadScene(3); // YouLoseMenu
                Destroy(gameObject);
            }
        }
        else
        {
            //Debug.Log("Hit evaded");
        }
        
    }
}
