using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodmotherController : EnemyController
{
    // Movement
    public float distanceToStop;
    Vector3 vectorOfTravel;
    public float timeBetweenBirths;
    float timeOfNextBirth;
    Vector3 targetPosition;
    Vector3 currentPosition;

    // Birthing
    public GameObject birthedEnemy;

    // Animation
    Vector3 forward;
    Animator animator;


    private void Start()
    {
        InheretedStart();

        timeOfNextBirth = 0;

        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        targetPosition = Player.transform.position;
        currentPosition = gameObject.transform.position;

        vectorOfTravel = targetPosition - currentPosition;
        vectorOfTravel.Normalize();

        Animate();
    }

    private void FixedUpdate()
    {
        if (Player != null) // running towards player
        {
            float distance = Vector3.Distance(currentPosition, targetPosition);

            if (distance > distanceToStop && (timeOfNextBirth > Time.time))
            {
                //rb.MovePosition(currentPosition + movementSpeed * Time.fixedDeltaTime * vectorOfTravel);
            } 
            else if(timeOfNextBirth <= Time.time)
            {
                animator.SetTrigger("Birth");
                GiveBirth();
            }
        }
    }

    public void GiveBirth()
    {

        if(Player != null)
        {
            //Debug.Log("Gave birth");
            timeOfNextBirth = Time.time + timeBetweenBirths;

            Instantiate(birthedEnemy, transform.position, transform.rotation);

        }
    }


    private void Animate()
    {
        //[-1, +1] [player is left, player is right]
        if (Vector3.Cross(transform.forward, vectorOfTravel).y > 0)
        {
            animator.SetFloat("Y_Axis", 1);
        }
        else
        {
            animator.SetFloat("Y_Axis", -1);
        }

        //Debug.Log(Vector3.Cross(forward, vectorOfTravel).y);
    }
}
