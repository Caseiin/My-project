using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Ignore the enemy that shot it
        if(other.CompareTag("Enemy")|| other.CompareTag("Sensor")) return;

        if(other.CompareTag("Player")){
            var health = other.GetComponent<PlayerHealth>();
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
