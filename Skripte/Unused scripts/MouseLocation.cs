using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Camera-main - za glavnu kameru koju koristimo 
                                                                                                  // vraca odnos izmedju pozicije kamere i pozicije misa 
                                                                                                  // transform.position - pozicija objekta kome je ova skripta pripisana, oduzimamo tu vrijednost da bi dobili vektor od misa do oruzja 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // kut izmedju oruzja i misa igraca 
        //Quaternion rotacija = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        // prvi argument kut, drugi argument oko koje osi se okrećemo, Vector3.forward rotacija oko Z osi 
        // kut - 90 jer je sprite nacrtan sa kutom od 90 stupnjeva (uspravno) 


        Debug.Log(angle);

    }
}
