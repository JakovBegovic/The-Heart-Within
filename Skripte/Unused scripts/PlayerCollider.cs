using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    bool playerHasGun = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && !playerHasGun)
        {
            Destroy(collision.gameObject);
            playerHasGun = true;
            Debug.Log("I have a gun!");
        }

    }

}
