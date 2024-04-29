using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleGunsController : MonoBehaviour
{
    int totalGuns;
    public int currentGunIndex;

    public GameObject[] guns;
    public GameObject gunHolder;
    public GameObject currentGun;

    private float timeOfNextSwap;
    private float timeBetweenSwaps;

    // Start is called before the first frame update
    void Start()
    {
        totalGuns = gunHolder.transform.childCount;
        guns = new GameObject[totalGuns];

        for (int i = 0; i < totalGuns; i++)
        {
            guns[i] = gunHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);  
        }

        guns[0].SetActive(true);

        currentGun = guns[0];
        currentGunIndex = 0;


        timeOfNextSwap = 0;
        timeBetweenSwaps = 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1) && timeOfNextSwap <= Time.time)
        {
            timeOfNextSwap = Time.time + timeBetweenSwaps;

            SetCurrentGunInactive();
            currentGunIndex++;
            currentGunIndex %= totalGuns;
            SetCurrentGunActive();
            currentGun = guns[currentGunIndex];
        }
        
    }

    public void SetCurrentGunActive()
    {
        guns[currentGunIndex].SetActive(true);
    }


    public void SetCurrentGunInactive()
    {
        guns[currentGunIndex].SetActive(false);
    }
}
