using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int projectileDamage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().playerHealth -= projectileDamage;
            collision.gameObject.GetComponent<PlayerHealth>().bloodSplatter.Play();
        }

        DestroyProjectile();
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
