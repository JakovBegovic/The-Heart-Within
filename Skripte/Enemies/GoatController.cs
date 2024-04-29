using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GoatController : EnemyController
{
    // Movement
    public float distanceToStop;
    Vector3 targetPosition;
    Vector3 currentPosition;
    float distanceToPlayer;
    Vector3 vectorOfTravel;

    // Animation
    Vector3 forward;
    Animator animator;

    private void Start()
    {
        InheretedStart();

        vectorOfTravel = Player.transform.position - gameObject.transform.position;

        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        if (Player != null) // running towards player
        {
            targetPosition = Player.transform.position;
            currentPosition = gameObject.transform.position;


            distanceToPlayer = Vector3.Distance(currentPosition, targetPosition);

            if (distanceToPlayer > distanceToStop)
            {
                vectorOfTravel = targetPosition - currentPosition;
                vectorOfTravel.Normalize();

                //rb.MovePosition(currentPosition + movementSpeed * Time.fixedDeltaTime * vectorOfTravel);
                Animate();

            }
        }
    }

    private void Animate()
    {
        forward = transform.forward;

        animator.SetFloat("X_axis", Vector3.Cross(forward, vectorOfTravel).x);
        //[-1, +1] [player is up, player is down]
        //Debug.Log(Vector3.Cross(forward, vectorOfTravel).x);

        animator.SetFloat("Y_axis", Vector3.Cross(forward, vectorOfTravel).y);
        //[-1, +1] [player is left, player is right]
        //Debug.Log(Vector3.Cross(forward, vectorOfTravel).y);
    }

}
