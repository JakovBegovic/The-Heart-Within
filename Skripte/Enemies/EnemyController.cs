using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{

    // Combat
    [HideInInspector] public Transform Player;
    public int lifePoints;
    public int collisionDamageDealt;
    public float timeBetweenCollisionAttacks;
    protected float timeOfNextAttack;
    // public GameObject deathParticles; on a later date
    bool isHit = false;

    // Movement
    [HideInInspector] public Rigidbody2D rb;
    public float movementSpeed;

    // Animation
    [SerializeField] private SimpleFlash flashEffect;

    // GUI
    protected SoulsCollectedController soulsCollectedController;
    public int numberOfSoulsHeld = 2;

    // Sound
    private AudioSource audioSource;


    void Start()
    {
        InheretedStart();
    }

    public void InheretedStart()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        timeOfNextAttack = 0;

        soulsCollectedController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SoulsCollectedController>();
    }
    
    public void TakeDamage(int damage)
    {
        if(!isHit)
        {
            isHit = true;

            audioSource.Play();

            flashEffect.Flash();
            lifePoints -= damage;

            if (lifePoints < 0)
            {
                GiveSouls();

                Destroy(gameObject);
            }

            isHit = false;
        }
        
    }

    public virtual void GiveSouls()
    {
        //Debug.Log("Gave " + numberOfSoulsHeld + " souls");
        soulsCollectedController.AddSouls(numberOfSoulsHeld);
    }

    void OnTriggerStay2D(Collider2D collision) // inflicting collision damage
    {
        if (collision.CompareTag("Player") && (timeOfNextAttack <= Time.time))
        {
            //Debug.Log("Attacked player");
            timeOfNextAttack = Time.time + timeBetweenCollisionAttacks;

            collision.gameObject.GetComponent<PlayerController>().TakeDamage(collisionDamageDealt);
        }

    }

}
