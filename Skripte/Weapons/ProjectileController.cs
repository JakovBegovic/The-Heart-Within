using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public float timeOfFlight;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyBullet), timeOfFlight);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void DestroyBullet()
    {
        //Instantiate explosion
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            DestroyBullet();
        }
    }
}
