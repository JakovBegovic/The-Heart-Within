using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCntroller : MonoBehaviour
{
    //public Text healthText;
    public Image healthBar;

    public GameObject player;
    private PlayerController playerController;

    float health, maxHealth;

    float lerpSpeed;
    Color healthColor;

    Color blueColor;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        maxHealth = playerController.maxLifePoints;
        health = playerController.lifePoints;

        blueColor = new Color(0, 168, 255);
    }


    void Update()
    {
        health = playerController.lifePoints;

        //healthText.text = "Soul power " + health + "%";

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    void ColorChanger()
    {
        healthColor = Color.Lerp(Color.grey, blueColor, (health / maxHealth));

        healthBar.color = healthColor;
    }
}
