using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedSmileProjectileController : MonoBehaviour
{
    private PlayerController PlayerController;

    Vector3 targetPosition;
    Vector3 currentPosition;
    Vector3 directionOfTravel;

    Vector2 playerDirection;
    float angleToPlayer;
    Quaternion directionOfRotation;

    public float speed;
    public int damage;
    public float timeOfFlight;
    //public GameObject efektUnistenja; on a lat er date

    void Start()
    {
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        targetPosition = PlayerController.transform.position;
        currentPosition = gameObject.transform.position;
        directionOfTravel = targetPosition - currentPosition;
        directionOfTravel.Normalize();

        RotateOnStart();

        Invoke(nameof(DestroyBullet), timeOfFlight);

    }

    void Update()
    {
        transform.Translate(directionOfTravel.x * speed * Time.deltaTime,
            directionOfTravel.y * speed * Time.deltaTime,
            directionOfTravel.z * speed * Time.deltaTime,
            Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Instantiate(efektUnistenja, transform.position, Quaternion.identity);
            PlayerController.TakeDamage(damage);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void RotateOnStart()
    {
        playerDirection = targetPosition - transform.position;
        // vector of direction from camera to mouse
        // - Gun.transition alters it so it is not from the camera but from the object
        // that this script belongs to

        angleToPlayer = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        // angle between vector origin and destination on degrees, <-180, 180]

        directionOfRotation = Quaternion.AngleAxis(angleToPlayer + 180, Vector3.forward);
        // .forward because we are rotating around the Z axis

        transform.rotation = directionOfRotation;
        // the way that Quaternion rotation is applied for 2 axis
    }
}
