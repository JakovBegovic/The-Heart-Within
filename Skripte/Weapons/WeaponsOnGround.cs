using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsOnGround : MonoBehaviour
{
    private Collider2D gunOnGroundCollider;
    private Collider2D playerCollider;
    private SoulsCollectedController soulsCollectedController;
    public GunController gunController;
    public int cost;
    public int bulletNumberPerBuy;

    private float timeOfNextPossiblePurchase;
    private float timeBetweenPurchases;

    public bool isAK;
    public bool isGlock;
    public bool isShotgun;

    public AmmoCountController ammoCountController;



    private void Start()
    {
        gunOnGroundCollider = GetComponent<Collider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        soulsCollectedController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SoulsCollectedController>();

        timeOfNextPossiblePurchase = 0;
        timeBetweenPurchases = 0.5f;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gunOnGroundCollider.IsTouching(playerCollider) && timeOfNextPossiblePurchase <= Time.time)
        {
            timeOfNextPossiblePurchase = Time.time + timeBetweenPurchases;

            if (soulsCollectedController.TakeSouls(cost))
            {
                gunController.bulletsAvailable += bulletNumberPerBuy;
                if (isAK)
                {
                    ammoCountController.BoughtAK(bulletNumberPerBuy);
                }
                else if (isGlock)
                {
                    ammoCountController.BoughtGlock(bulletNumberPerBuy);
                }
                else
                {
                    ammoCountController.BoughtShotgun(bulletNumberPerBuy);
                }

            }
            else
            {
                Debug.Log("Not enough souls");
            }
        }
    }

}
