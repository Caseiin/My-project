using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            var health = other.GetComponent<PlayerHealth>();
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
