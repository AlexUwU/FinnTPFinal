using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    public float bonusDamage = 5f;   // cuánto suma SOLO en esta partida

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.runDamageBonus += bonusDamage;
            // opcional: efecto, sonido, etc.
            Destroy(gameObject);
        }
    }
}

