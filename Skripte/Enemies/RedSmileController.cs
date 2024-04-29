using UnityEngine;

public class RedSmileController : EnemyController
{
    // Movement
    public float distanceToStop;
    public float distanceToStart;
    float distanceToPlayer;
    bool canMove;
    Vector3 targetPosition;
    Vector3 currentPosition;
    Vector3 vectorOfTravel;

    // Combat
    public Transform ShootPoint;
    public GameObject Projectile;

    float timeOfNextShot;
    public float timeBetweenShots;

    // Animation
    Animator animator;

    void Start()
    {
        InheretedStart();

        canMove = false;

        timeOfNextShot = 0;

        animator = GetComponent<Animator>();

    }


    void Update()
    {
        targetPosition = Player.transform.position;
        currentPosition = gameObject.transform.position;
        
        vectorOfTravel = targetPosition - currentPosition;
        vectorOfTravel.Normalize();

        Animate();

        distanceToPlayer = Vector3.Distance(currentPosition, targetPosition);


        if(canMove)
        {
            // if player is closer than distance to stop than stop moving
            canMove = !(distanceToPlayer < distanceToStop);
            //animator.Play("BT_RedSmile_Levitating");
            animator.SetBool("isMoving", true);

        }
        else
        {
           // if player is farther away tha distance to start mving start moving
            canMove = distanceToPlayer >= distanceToStart;
            //animator.Play("BT_RedSmile_Idle");
            animator.SetBool("isMoving", false);
        }

        TriggerShooting();
    }


    private void FixedUpdate()
    {
        if (Player != null && canMove) // running towards player
        {
            //rb.MovePosition(currentPosition + movementSpeed * Time.fixedDeltaTime * vectorOfTravel);
        }
    }

    public void TriggerShooting()
    {
        if (timeOfNextShot <= Time.time)
        {
            timeOfNextShot = Time.time + timeBetweenShots;

            animator.SetTrigger("Shoot");
        }
    }
    
    public void InstantiateShot()
    { // called at the end of shoot animation
        if (Player != null)
        {
            Vector2 directionOfShooting = Player.position - ShootPoint.position;

            float angleOfShooting = Mathf.Atan2(directionOfShooting.y, directionOfShooting.x) * Mathf.Rad2Deg;
            Quaternion rotationOfShootPoint = Quaternion.AngleAxis(angleOfShooting, Vector3.right);
            ShootPoint.rotation = rotationOfShootPoint;

            
            Instantiate(Projectile, ShootPoint.position, ShootPoint.rotation);
        }
    }

    private void Animate()
    {
        //Debug.Log(Vector3.Cross(transform.forward, vectorOfTravel).y > 0);
        //[-1, +1] [player is left, player is right]
        if (Vector3.Cross(transform.forward, vectorOfTravel).y > 0)
        {
            animator.SetFloat("Y_axis", 1);
        }
        else
        {
            animator.SetFloat("Y_axis", -1);
        }
    }
}
