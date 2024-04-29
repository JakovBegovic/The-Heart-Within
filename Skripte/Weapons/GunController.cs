using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Rotation
    Vector2 mouseDirection;
    float mouseAngle;
    Quaternion gunDirZ;
    Quaternion gunDirX;

    // Sorting layer
    public GameObject GunSprite;
    public Animator anim; // for getting animDir parameter

    // Bullet firing
    public GameObject Projectile;
    public Transform ShootPoint;
    public float timeBetweenBullets;
    public int bulletsAvailable;
    bool hasBullets;
    float timeOfShooting;
    //public GameObject ShootingParticles; TODO
    public AmmoCountController ammoCountController;
    private ShootSoundController shotSoundController;

    // Shotgun specific
    public bool isShotgun = false;
    Quaternion bulletAngle;


    public bool isAK = false;
    public bool isGlock = false;


    void Start()
    {
        shotSoundController = GetComponent<ShootSoundController>();

        hasBullets = bulletsAvailable != 0;

        if (isAK)
        {
            ammoCountController.SetAmmoCountAK(bulletsAvailable);
        } else if(isGlock)
        {
            ammoCountController.SetAmmoCountGlock(bulletsAvailable);
        } else if (isShotgun)
        {
            ammoCountController.SetAmmoCountShotgun(bulletsAvailable);

        }
    }

    void Update()
    {
        hasBullets = bulletsAvailable != 0;

        HandleRotation();
        HandleLayering();
        HandleShooting();

    }
    
    void HandleRotation()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // vector of direction from camera to mouse
        // - Gun.transition alters it so it is not from the camera but from the object
        // that this script belongs to

        mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        // angle between vector origin and destination on degrees, <-180, 180]

        gunDirZ = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
        // .forward because we are rotating around the Z axis

        if (mouseAngle > 90 || mouseAngle < -90)
        {
            gunDirX = Quaternion.AngleAxis(-180, Vector3.right);
        }
        else
        {
            gunDirX = Quaternion.AngleAxis(0, Vector3.right);
        }

        transform.rotation = gunDirZ * gunDirX;
        // the way that Quaternion rotation is applied for 2 axis
    }

    void HandleLayering()
    {
        switch (anim.GetFloat("animDirection"))
        {
            case 1: // EAST
                SortingLayerDown();
                break;

            case 2: // NORTH
                SortingLayerDown();
                break;

            case 3: // WEST
                SortingLayerDown();
                break;

            case 4: // SOUTH
                SortingLayerUp();
                break;

        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= timeOfShooting && hasBullets)
            {
                shotSoundController.PlayShot();

                bulletsAvailable--;
                hasBullets = bulletsAvailable != 0;

                timeOfShooting = Time.time + timeBetweenBullets;

                if (isShotgun)
                {
                    ammoCountController.ShotShotgun();
                    for (int i=0; i<7; i++)
                    {
                        bulletAngle = Quaternion.Euler(0, 0, Random.Range(-9.0f, 9.0f));
                        Instantiate(Projectile, ShootPoint.position, transform.rotation * bulletAngle);
                    }

                }
                else if(isAK)
                {
                    ammoCountController.ShotAK();
                    Instantiate(Projectile, ShootPoint.position, transform.rotation);
                }
                else
                {
                    ammoCountController.ShotGlock();
                    Instantiate(Projectile, ShootPoint.position, transform.rotation);
                }
                //Instantiate(ShootingParticles, ShootPoint.position, transform.rotation);
            }
        }
    }

    public void SortingLayerDown()
    {

        GunSprite.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Weapon equipped down");
        
    }

    public void SortingLayerUp()
    {
        GunSprite.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Weapon equipped up");
    }
}
