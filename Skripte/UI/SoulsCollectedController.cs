using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCollectedController : MonoBehaviour
{
    int soulsCollected = 0;

    public TextMeshProUGUI demonSoulText;


    private void Start()
    {
        demonSoulText.text = "Souls collected: " + soulsCollected;
    }

    public void AddSouls(int numberOfSouls)
    {
        soulsCollected += numberOfSouls;
        demonSoulText.text = "Souls collected: " + soulsCollected;
    }

    public bool TakeSouls(int numberOfSouls)
    {
        if(soulsCollected >= numberOfSouls) 
        { 
            soulsCollected -= numberOfSouls;
            demonSoulText.text = "Souls collected: " + soulsCollected;

            return true; // if the player had enough souls collected
        }
        else
        {
            return false; // if the player does not have enough souls collected
        }
        
    }
}
