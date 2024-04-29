using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCountController : MonoBehaviour
{
    private int ammoCountAK = 60;
    private int ammoCountShotgun = 4;
    private int ammoCountGlock = 50;

    public GunController AKController;
    public GunController ShotgunController;
    public GunController GlockController;

    public TextMeshProUGUI ammoCountAKText;
    public TextMeshProUGUI ammoCountShotgunText;
    public TextMeshProUGUI ammoCountGlockText;

    private void Start()
    {
        SetAmmoCountAK(ammoCountAK);
        SetAmmoCountShotgun(ammoCountShotgun);
        SetAmmoCountGlock(ammoCountGlock);
    }

    public void SetAmmoCountAK(int count)
    {
        ammoCountAK = count;
        ammoCountAKText.text = ammoCountAK + " bullets";
    }

    public void SetAmmoCountShotgun(int count)
    {
        ammoCountShotgun = count;
        ammoCountShotgunText.text = ammoCountShotgun + " shells";
    }

    public void SetAmmoCountGlock(int count)
    {
        ammoCountGlock = count;
        ammoCountGlockText.text = ammoCountGlock + " bullets";
    }

    public void ShotAK()
    {
        ammoCountAK--;
        ammoCountAKText.text = ammoCountAK + " bullets";
        //Display new ammo count
    }


    public void ShotShotgun()
    {
        ammoCountShotgun--;
        ammoCountShotgunText.text = ammoCountShotgun + " shells";
        //Display new ammo count
    }


    public void ShotGlock()
    {
        ammoCountGlock--;
        ammoCountGlockText.text = ammoCountGlock + " bullets";
        //Display new ammo count
    }

        public void BoughtAK(int count)
    {
        ammoCountAK += count;
        ammoCountAKText.text = ammoCountAK + " bullets";
        //Display new ammo count
    }


    public void BoughtShotgun(int count)
    {
        ammoCountShotgun += count;
        ammoCountShotgunText.text = ammoCountShotgun + " shells";
        //Display new ammo count
    }


    public void BoughtGlock(int count)
    {
        ammoCountGlock += count;
        ammoCountGlockText.text = ammoCountGlock + " bullets";
        //Display new ammo count
    }

}
